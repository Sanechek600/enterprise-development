namespace BikeRental.Application.Contracts.RentalRecords;

/// <summary>
/// DTO для создания и обновления записи аренды
/// </summary>
/// <param name="RenterId">Идентификатор арендатора</param>
/// <param name="BikeId">Идентификатор велосипеда</param>
/// <param name="StartTime">Время начала аренды</param>
/// <param name="Duration">Продолжительность аренды</param>
public sealed record RentalRecordCreateUpdateDto(
    int RenterId,
    int BikeId,
    DateTime StartTime,
    TimeSpan Duration
);