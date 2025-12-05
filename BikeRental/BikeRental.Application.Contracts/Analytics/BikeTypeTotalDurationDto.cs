using BikeRental.Domain.Enums;

namespace BikeRental.Application.Contracts.Analytics;

/// <summary>
/// DTO для выдачи суммарного времени аренды по типу велосипеда
/// </summary>
/// <param name="Type">Тип велосипеда</param>
/// <param name="TotalHours">Суммарное время аренды в часах</param>
public sealed record BikeTypeTotalDurationDto(
    BikeType Type,
    double TotalHours
);