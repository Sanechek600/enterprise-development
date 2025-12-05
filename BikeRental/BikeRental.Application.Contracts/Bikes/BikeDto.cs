namespace BikeRental.Application.Contracts.Bikes;

/// <summary>
/// DTO для получения велосипеда
/// </summary>
/// <param name="Id">Уникальный идентификатор велосипеда</param>
/// <param name="SerialNumber">Серийный номер велосипеда</param>
/// <param name="Color">Цвет велосипеда</param>
public sealed record BikeDto(
    int Id,
    string SerialNumber,
    string? Color
);