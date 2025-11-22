namespace BikeRental.Domain.Models;

/// <summary>
/// Велосипед с указанием модели и серийного номера
/// </summary>
public class Bike
{
    /// <summary>
    /// Уникальный идентификатор велосипеда
    /// </summary>
    public required int Id { get; set; }

    /// <summary>
    /// Серийный номер велосипеда
    /// </summary>
    public required string SerialNumber { get; set; }

    /// <summary>
    /// Цвет велосипеда
    /// </summary>
    public string? Color { get; set; }

    /// <summary>
    /// Идентификатор модели велосипеда
    /// </summary>
    public required int ModelId { get; set; }

    /// <summary>
    /// Модель велосипеда
    /// </summary>
    public BikeModel? Model { get; set; }

    /// <summary>
    /// Список записей аренды для этого велосипеда
    /// </summary>
    public List<RentalRecord>? RentalRecords { get; set; }
}