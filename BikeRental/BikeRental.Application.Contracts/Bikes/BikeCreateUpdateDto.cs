namespace BikeRental.Application.Contracts.Bikes;

/// <summary>
/// DTO для создания и обновления велосипеда
/// </summary>
/// <param name="SerialNumber">Серийный номер велосипеда</param>
/// <param name="Color">Цвет велосипеда</param>
/// <param name="ModelId">Идентификатор модели велосипеда</param>
public sealed record BikeCreateUpdateDto(
    string SerialNumber,
    string? Color,
    int ModelId
);