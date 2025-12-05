using BikeRental.Application.Contracts.BikeModels;

namespace BikeRental.Application.Contracts.Analytics;

/// <summary>
/// DTO для выдачи модели велосипеда с прибылью от аренды
/// </summary>
/// <param name="Model">Модель велосипеда</param>
/// <param name="Profit">Прибыль от аренды</param>
public sealed record BikeModelProfitDto(
    BikeModelDto Model,
    decimal Profit
);