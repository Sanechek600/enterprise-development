using BikeRental.Application.Contracts.BikeModels;

namespace BikeRental.Application.Contracts.Analytics;

/// <summary>
/// DTO для выдачи модели велосипеда с суммарной длительностью аренды
/// </summary>
/// <param name="Model">Модель велосипеда</param>
/// <param name="TotalHours">Суммарное время аренды в часах</param>
public sealed record BikeModelTotalDurationDto(
    BikeModelDto Model,
    double TotalHours
);