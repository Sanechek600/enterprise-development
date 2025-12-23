using BikeRental.Application.Contracts.RentalRecords;
using BikeRental.Generator.Kafka.Host.Configuration;
using Confluent.Kafka;
using Microsoft.Extensions.Options;
using Polly;
using Polly.Retry;

namespace BikeRental.Generator.Kafka.Host.Producing;

/// <summary>
/// Продюсер записей аренды в Kafka
/// </summary>
/// <param name="producer">Kafka producer</param>
/// <param name="logger">Логгер продюсера</param>
/// <param name="options">Настройки Kafka producer</param>
public class RentalRecordProducer(
    IProducer<int, RentalRecordCreateUpdateDto> producer,
    ILogger<RentalRecordProducer> logger,
    IOptions<KafkaProducerOptions> options) : IDisposable
{
    private readonly KafkaProducerOptions _options = options.Value;

    private readonly AsyncRetryPolicy _retryPolicy = Policy
        .Handle<KafkaException>()
        .Or<ProduceException<int, RentalRecordCreateUpdateDto>>()
        .WaitAndRetryAsync(
            options.Value.RetryCount,
            attempt => GetRetryDelay(options.Value, attempt),
            (exception, delay, attempt, _) =>
            {
                logger.LogWarning(
                    exception,
                    "Ошибка при отправке сообщения в Kafka. Попытка {Attempt} из {MaxAttempts}. Следующая попытка через {Delay} секунд",
                    attempt,
                    options.Value.RetryCount,
                    delay.TotalSeconds);
            });

    /// <summary>
    /// Отправляет сообщение о записи аренды в Kafka
    /// </summary>
    /// <param name="key">Ключ сообщения</param>
    /// <param name="dto">DTO записи аренды</param>
    /// <param name="cancellationToken">Токен отмены</param>
    public async Task ProduceAsync(int key, RentalRecordCreateUpdateDto dto, CancellationToken cancellationToken = default)
    {
        var message = new Message<int, RentalRecordCreateUpdateDto>
        {
            Key = key,
            Value = dto
        };

        try
        {
            await _retryPolicy.ExecuteAsync(async () =>
            {
                cancellationToken.ThrowIfCancellationRequested();

                var result = await producer.ProduceAsync(_options.Topic, message, cancellationToken);

                logger.LogInformation(
                    "Сообщение отправлено в Kafka. Topic={Topic}, Partition={Partition}, Offset={Offset}, Key={Key}",
                    result.Topic,
                    result.Partition.Value,
                    result.Offset.Value,
                    key);
            });
        }
        catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
        {
            logger.LogInformation("Отправка сообщения в Kafka отменена. Key={Key}, RenterId={RenterId}, BikeId={BikeId}", key, dto.RenterId, dto.BikeId);

            throw;
        }
        catch (ProduceException<int, RentalRecordCreateUpdateDto> ex)
        {
            logger.LogError(ex, "Ошибка Kafka при отправке сообщения. Topic={Topic}, Key={Key}, Reason={Reason}", _options.Topic, key, ex.Error.Reason);

            throw;
        }
        catch (KafkaException ex)
        {
            logger.LogError(ex, "Ошибка Kafka при отправке сообщения. Topic={Topic}, Key={Key}", _options.Topic, key);

            throw;
        }
    }

    private static TimeSpan GetRetryDelay(KafkaProducerOptions options, int attempt)
    {
        var baseDelay = Math.Max(1, options.RetryBaseDelaySeconds);
        var seconds = baseDelay * Math.Pow(2, Math.Max(0, attempt - 1));
        var cappedSeconds = Math.Min(seconds, 60);
        return TimeSpan.FromSeconds(cappedSeconds);
    }

    public void Dispose()
    {
        producer.Flush(TimeSpan.FromSeconds(5));
        producer.Dispose();
        GC.SuppressFinalize(this);
    }
}