namespace BikeRental.Generator.Kafka.Host.Configuration;

/// <summary>
/// Настройки Kafka продюсера
/// </summary>
public class KafkaProducerOptions
{
    /// <summary>
    /// Название секции конфигурации
    /// </summary>
    public const string SectionName = "KafkaProducer";

    /// <summary>
    /// Название топика для отправки сообщений
    /// </summary>
    public string Topic { get; set; } = "rental-records";

    /// <summary>
    /// Количество попыток переподключения к брокеру
    /// </summary>
    public int RetryCount { get; set; } = 5;

    /// <summary>
    /// Базовая задержка между попытками в секундах
    /// </summary>
    public int RetryBaseDelaySeconds { get; set; } = 2;
}
