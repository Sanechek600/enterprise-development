namespace BikeRental.Domain.Models;

/// <summary>
/// Запись о выдаче велосипеда фиксирующая время начала и длительность аренды
/// </summary>
public class RentalRecord
{
    /// <summary>
    /// Уникальный идентификатор записи аренды
    /// </summary>
    public required int Id { get; set; }

    /// <summary>
    /// Идентификатор арендатора
    /// </summary>
    public required int RenterId { get; set; }

    /// <summary>
    /// Арендатор
    /// </summary>
    public Renter? Renter { get; set; }

    /// <summary>
    /// Идентификатор велосипеда
    /// </summary>
    public required int BikeId { get; set; }

    /// <summary>
    /// Велосипед
    /// </summary>
    public Bike? Bike { get; set; }

    /// <summary>
    /// Время начала аренды
    /// </summary>
    public required DateTime StartTime { get; set; }

    /// <summary>
    /// Продолжительность аренды в часах
    /// </summary>
    public int? DurationHours { get; set; }
}