using BikeRental.Generator.Kafka.Host.Configuration;
using BikeRental.Generator.Kafka.Host.Generation;
using BikeRental.Generator.Kafka.Host.Producing;
using Microsoft.Extensions.Options;

namespace BikeRental.Generator.Kafka.Host.Workers;

/// <summary>
/// Фоновый сервис для генерации и отправки записей аренды в Kafka
/// </summary>
public class GeneratorWorker(
    RentalRecordGenerator generator,
    RentalRecordProducer producer,
    ILogger<GeneratorWorker> logger,
    IOptions<GeneratorOptions> options) : BackgroundService
{
    private readonly GeneratorOptions _options = options.Value;
    private int _messageCounter;

    /// <summary>
    /// Запускает генерацию и отправку сообщений с заданным интервалом
    /// </summary>
    /// <param name="stoppingToken">Токен отмены остановки сервиса</param>
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        logger.LogInformation(
            "Запуск генератора сообщений. Интервал: {Interval} сек, BatchSize: {BatchSize}, MaxRenterId: {MaxRenterId}, MaxBikeId: {MaxBikeId}",
            _options.IntervalSeconds,
            _options.BatchSize,
            _options.MaxRenterId,
            _options.MaxBikeId);

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                var dtos = generator.Generate(_options.BatchSize);

                logger.LogInformation("Сгенерировано записей аренды: {Count}", dtos.Count);

                foreach (var dto in dtos)
                {
                    stoppingToken.ThrowIfCancellationRequested();

                    var key = Interlocked.Increment(ref _messageCounter);

                    logger.LogInformation(
                        "Сгенерирована запись аренды #{Key}: RenterId={RenterId}, BikeId={BikeId}, StartTime={StartTime}, Duration={Duration}",
                        key,
                        dto.RenterId,
                        dto.BikeId,
                        dto.StartTime,
                        dto.Duration);

                    await producer.ProduceAsync(key, dto, stoppingToken);

                    logger.LogInformation("Запись аренды #{Key} успешно отправлена в Kafka", key);
                }
            }
            catch (OperationCanceledException) when (stoppingToken.IsCancellationRequested)
            {
                logger.LogInformation("Генерация и отправка сообщений отменены");
                break;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Ошибка при генерации или отправке записи аренды");
            }

            try
            {
                await Task.Delay(TimeSpan.FromSeconds(_options.IntervalSeconds), stoppingToken);
            }
            catch (OperationCanceledException) when (stoppingToken.IsCancellationRequested)
            {
                logger.LogInformation("Ожидание следующего цикла отменено");
                break;
            }
        }

        logger.LogInformation("Генератор контрактов остановлен");
    }
}