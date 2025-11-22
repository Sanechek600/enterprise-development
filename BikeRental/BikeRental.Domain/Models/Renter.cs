namespace BikeRental.Domain.Models;

/// <summary>
/// Арендатор велосипеда
/// </summary>
public class Renter
{
    /// <summary>
    /// Уникальный идентификатор арендатора
    /// </summary>
    public required int Id { get; set; }

    /// <summary>
    /// ФИО арендатора
    /// </summary>
    public required string FullName { get; set; }

    /// <summary>
    /// Телефон арендатора
    /// </summary>
    public required string Phone { get; set; }

    /// <summary>
    /// Список записей аренды этого арендатора
    /// </summary>
    public List<RentalRecord>? RentalRecords { get; set; }
}