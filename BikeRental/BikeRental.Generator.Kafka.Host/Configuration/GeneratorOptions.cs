namespace BikeRental.Generator.Kafka.Host.Configuration;

/// <summary>
/// Настройки генератора контрактов
/// </summary>
public class GeneratorOptions
{
    /// <summary>
    /// Название секции конфигурации
    /// </summary>
    public const string SectionName = "Generator";

    /// <summary>
    /// Интервал генерации сообщений в секундах
    /// </summary>
    public int IntervalSeconds { get; set; } = 5;

    /// <summary>
    /// Количество сообщений генерируемых за один цикл
    /// </summary>
    public int BatchSize { get; set; } = 5;

    /// <summary>
    /// Максимальный Id арендатора (генерируется от 1 до MaxRenterId)
    /// </summary>
    public int MaxRenterId { get; set; } = 20;

    /// <summary>
    /// Максимальный Id велосипеда (генерируется от 1 до MaxBikeId)
    /// </summary>
    public int MaxBikeId { get; set; } = 20;

    /// <summary>
    /// Минимальная продолжительность аренды в часах
    /// </summary>
    public int MinDurationHours { get; set; } = 1;

    /// <summary>
    /// Максимальная продолжительность аренды в часах
    /// </summary>
    public int MaxDurationHours { get; set; } = 8;

    /// <summary>
    /// Диапазон дней для генерации даты начала (+-DaysRange от текущей даты)
    /// </summary>
    public int DaysRange { get; set; } = 30;
}
