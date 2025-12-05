namespace BikeRental.Application.Contracts.Analytics;

/// <summary>
/// DTO для статистики по длительности аренды
/// </summary>
/// <param name="MinHours">Минимальное время аренды в часах</param>
/// <param name="MaxHours">Максимальное время аренды в часах</param>
/// <param name="AvgHours">Среднее время аренды в часах</param>
public sealed record RentalDurationStatsDto(
    double MinHours,
    double MaxHours,
    double AvgHours
);