using BikeRental.Domain.Enums;

namespace BikeRental.Application.Contracts.BikeModels;

/// <summary>
/// DTO для получения модели велосипеда
/// </summary>
/// <param name="Id">Уникальный идентификатор модели</param>
/// <param name="Type">Тип велосипеда</param>
/// <param name="WheelSize">Размер колёс в дюймах</param>
/// <param name="MaxRiderWeight">Предельно допустимый вес пассажира в килограммах</param>
/// <param name="BikeWeight">Вес велосипеда в килограммах</param>
/// <param name="BrakeType">Тип тормозов</param>
/// <param name="Year">Модельный год выпуска</param>
/// <param name="HourlyPrice">Цена аренды за один час</param>
public sealed record BikeModelDto(
    int Id,
    BikeType Type,
    int WheelSize,
    double MaxRiderWeight,
    double? BikeWeight,
    string? BrakeType,
    int? Year,
    decimal HourlyPrice
);