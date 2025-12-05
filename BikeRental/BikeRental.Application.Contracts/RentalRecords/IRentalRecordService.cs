namespace BikeRental.Application.Contracts.RentalRecords;

/// <summary>
/// Интерфейс службы приложения для получения записей аренды
/// </summary>
public interface IRentalRecordService
{
    /// <summary>
    /// Возвращает запись аренды по идентификатору арендатора
    /// </summary>
    /// <param name="renterId">Идентификатор арендатора</param>
    /// <returns>DTO записи аренды</returns>
    public Task<RentalRecordDto> GetRentalRecordByRenterId(int renterId);

    /// <summary>
    /// Возвращает запись аренды по идентификатору велосипеда
    /// </summary>
    /// <param name="bikeId">Идентификатор велосипеда</param>
    /// <returns>DTO записи аренды</returns>
    public Task<RentalRecordDto> GetRentalRecordByBikeId(int bikeId);
}