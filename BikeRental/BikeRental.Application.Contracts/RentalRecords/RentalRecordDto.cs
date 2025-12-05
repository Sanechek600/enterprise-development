namespace BikeRental.Application.Contracts.RentalRecords;

/// <summary>
/// DTO для получения записи аренды
/// </summary>
/// <param name="Id">Уникальный идентификатор записи аренды</param>
/// <param name="RenterId">Идентификатор арендатора</param>
/// <param name="BikeId">Идентификатор велосипеда</param>
/// <param name="StartTime">Время начала аренды</param>
/// <param name="Duration">Продолжительность аренды</param>
public sealed record RentalRecordDto(
    int Id,
    int RenterId,
    int BikeId,
    DateTime StartTime,
    TimeSpan Duration
);