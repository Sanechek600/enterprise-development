namespace BikeRental.Infrastructure.Kafka.Configuration;

/// <summary>
/// Настройки Kafka для консьюмера
/// </summary>
public class KafkaConsumerOptions
{
    /// <summary>
    /// Название секции конфигурации
    /// </summary>
    public const string SectionName = "KafkaConsumer";

    /// <summary>
    /// Название топика для чтения сообщений
    /// </summary>
    public string Topic { get; set; } = "rental-records";

    /// <summary>
    /// Идентификатор группы консьюмеров
    /// </summary>
    public string GroupId { get; set; } = "api-host-consumer";

    /// <summary>
    /// Количество попыток переподключения к брокеру
    /// </summary>
    public int RetryCount { get; set; } = 5;

    /// <summary>
    /// Базовая задержка между попытками в секундах
    /// </summary>
    public int RetryBaseDelaySeconds { get; set; } = 2;
}
