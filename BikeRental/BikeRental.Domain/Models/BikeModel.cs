using BikeRental.Domain.Enums;

namespace BikeRental.Domain.Models;

/// <summary>
/// Модель велосипеда содержащая технические характеристики и цену аренды в час
/// </summary>
public class BikeModel
{
    /// <summary>
    /// Уникальный идентификатор модели
    /// </summary>
    public required int Id { get; set; }

    /// <summary>
    /// Тип велосипеда
    /// </summary>
    public required BikeType Type { get; set; }

    /// <summary>
    /// Размер колес в дюймах
    /// </summary>
    public required int WheelSize { get; set; }

    /// <summary>
    /// Предельно допустимый вес пассажира в килограммах
    /// </summary>
    public required int MaxRiderWeight { get; set; }

    /// <summary>
    /// Вес велосипеда в килограммах
    /// </summary>
    public double? BikeWeight { get; set; }

    /// <summary>
    /// Тип тормозов
    /// </summary>
    public string? BrakeType { get; set; }

    /// <summary>
    /// Модельный год выпуска
    /// </summary>
    public int? Year { get; set; }

    /// <summary>
    /// Цена аренды за один час
    /// </summary>
    public required decimal HourlyPrice { get; set; }

    /// <summary>
    /// Список велосипедов данной модели
    /// </summary>
    public List<Bike>? Bikes { get; set; }
}