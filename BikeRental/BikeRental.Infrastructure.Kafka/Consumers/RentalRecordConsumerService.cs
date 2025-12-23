using BikeRental.Application.Contracts.RentalRecords;
using BikeRental.Infrastructure.Kafka.Configuration;
using Confluent.Kafka;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Polly;
using Polly.Retry;

namespace BikeRental.Infrastructure.Kafka.Consumers;

/// <summary>
/// Фоновый сервис для потребления сообщений о записях аренды из Kafka
/// </summary>
/// <param name="scopeFactory">Фабрика скоупов DI</param>
/// <param name="logger">Логгер сервиса</param>
/// <param name="options">Настройки Kafka consumer</param>
/// <param name="consumer">Kafka consumer</param>
public class RentalRecordConsumerService(
    IServiceScopeFactory scopeFactory,
    ILogger<RentalRecordConsumerService> logger,
    IOptions<KafkaConsumerOptions> options,
    IConsumer<int, RentalRecordCreateUpdateDto> consumer)
    : BackgroundService
{
    private readonly KafkaConsumerOptions _options = options.Value;

    private readonly AsyncRetryPolicy _subscribeRetryPolicy = Policy
        .Handle<KafkaException>()
        .Or<ConsumeException>()
        .WaitAndRetryAsync(
            options.Value.RetryCount,
            attempt => GetRetryDelay(options.Value, attempt),
            (exception, delay, attempt, _) =>
            {
                logger.LogWarning(
                    exception,
                    "Ошибка при подписке на Kafka. Попытка {Attempt} из {MaxAttempts}. Следующая попытка через {Delay} секунд",
                    attempt,
                    options.Value.RetryCount,
                    delay.TotalSeconds);
            });

    /// <summary>
    /// Запускает цикл чтения сообщений из Kafka и обработки записей аренды
    /// </summary>
    /// <param name="stoppingToken">Токен отмены остановки сервиса</param>
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        logger.LogInformation("Запуск консьюмера Kafka для топика {Topic}", _options.Topic);

        try
        {
            await _subscribeRetryPolicy.ExecuteAsync(() =>
            {
                consumer.Subscribe(_options.Topic);
                logger.LogInformation("Успешно подписан на топик {Topic}", _options.Topic);
                return Task.CompletedTask;
            });

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    var consumeResult = consumer.Consume(TimeSpan.FromSeconds(1));

                    if (consumeResult?.Message?.Value is null)
                        continue;

                    var dto = consumeResult.Message.Value;

                    logger.LogInformation(
                        "Получено сообщение с ключом {Key}: RenterId={RenterId}, BikeId={BikeId}",
                        consumeResult.Message.Key,
                        dto.RenterId,
                        dto.BikeId);

                    await ProcessMessageAsync(dto, stoppingToken);

                    consumer.Commit(consumeResult);
                }
                catch (ConsumeException ex)
                {
                    logger.LogError(ex, "Ошибка при получении сообщения из Kafka");
                }
                catch (OperationCanceledException) when (stoppingToken.IsCancellationRequested)
                {
                    logger.LogInformation("Получен запрос на остановку консьюмера Kafka");
                    break;
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Неожиданная ошибка при обработке сообщения");
                }
            }

            if (stoppingToken.IsCancellationRequested)
                logger.LogInformation("Остановка консьюмера Kafka по токену отмены");
        }
        catch (OperationCanceledException) when (stoppingToken.IsCancellationRequested)
        {
            logger.LogInformation("Выполнение консьюмера Kafka отменено");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Непредвиденная ошибка консьюмера Kafka");
        }
        finally
        {
            try
            {
                consumer.Close();
            }
            catch (Exception ex)
            {
                logger.LogWarning(ex, "Ошибка при закрытии Kafka consumer");
            }

            logger.LogInformation("Консьюмер Kafka остановлен");
        }
    }

    /// <summary>
    /// Обрабатывает сообщение и создаёт запись аренды через сервис приложения
    /// </summary>
    /// <param name="dto">DTO для создания записи аренды</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>true если сообщение обработано и может быть закоммичено</returns>
    private async Task ProcessMessageAsync(RentalRecordCreateUpdateDto dto, CancellationToken cancellationToken)
    {
        if (cancellationToken.IsCancellationRequested)
        {
            logger.LogInformation("Обработка сообщения отменена до выполнения. RenterId={RenterId}, BikeId={BikeId}", dto.RenterId, dto.BikeId);
            return;
        }

        using var scope = scopeFactory.CreateScope();
        var rentalRecordService = scope.ServiceProvider.GetRequiredService<IRentalRecordService>();

        try
        {
            var result = await rentalRecordService.Create(dto);

            logger.LogInformation("Запись аренды успешно создана: Id={Id}, RenterId={RenterId}, BikeId={BikeId}", result.Id, result.RenterId, result.BikeId);
        }
        catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
        {
            logger.LogInformation("Обработка сообщения отменена во время выполнения. RenterId={RenterId}, BikeId={BikeId}", dto.RenterId, dto.BikeId);
            return;
        }
        catch (KeyNotFoundException ex)
        {
            logger.LogWarning("Ошибка валидации при создании записи аренды: {Message}. RenterId={RenterId}, BikeId={BikeId}", ex.Message, dto.RenterId, dto.BikeId);
        }
    }

    private static TimeSpan GetRetryDelay(KafkaConsumerOptions options, int attempt)
    {
        var baseDelay = Math.Max(1, options.RetryBaseDelaySeconds);
        var seconds = baseDelay * Math.Pow(2, Math.Max(0, attempt - 1));
        var cappedSeconds = Math.Min(seconds, 60);
        return TimeSpan.FromSeconds(cappedSeconds);
    }

    public override void Dispose()
    {
        consumer.Unsubscribe();
        GC.SuppressFinalize(this);
        base.Dispose();
    }
}