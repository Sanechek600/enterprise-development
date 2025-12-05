using BikeRental.Domain.Enums;

namespace BikeRental.Application.Contracts.BikeModels;

/// <summary>
/// DTO для создания и обновления модели велосипеда
/// </summary>
/// <param name="Type">Тип велосипеда</param>
/// <param name="WheelSize">Размер колёс в дюймах</param>
/// <param name="MaxRiderWeight">Предельно допустимый вес пассажира в килограммах</param>
/// <param name="BikeWeight">Вес велосипеда в килограммах</param>
/// <param name="BrakeType">Тип тормозов</param>
/// <param name="Year">Модельный год выпуска</param>
/// <param name="HourlyPrice">Цена аренды за один час</param>
public sealed record BikeModelCreateUpdateDto(
    BikeType Type,
    int WheelSize,
    double MaxRiderWeight,
    double? BikeWeight,
    string? BrakeType,
    int? Year,
    decimal HourlyPrice
);