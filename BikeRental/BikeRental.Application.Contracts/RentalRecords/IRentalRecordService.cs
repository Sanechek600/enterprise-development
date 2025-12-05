namespace BikeRental.Application.Contracts.RentalRecords;

/// <summary>
/// Интерфейс службы приложения для работы с записями аренды
/// </summary>
public interface IRentalRecordService : IApplicationService<RentalRecordDto, RentalRecordCreateUpdateDto, int>
{
    /// <summary>
    /// Возвращает список записей аренды арендатора
    /// </summary>
    /// <param name="renterId">Идентификатор арендатора</param>
    /// <returns>Список DTO записей аренды</returns>
    public Task<IList<RentalRecordDto>> GetRentalRecordsByRenterId(int renterId);

    /// <summary>
    /// Возвращает список записей аренды велосипеда
    /// </summary>
    /// <param name="bikeId">Идентификатор велосипеда</param>
    /// <returns>Список DTO записей аренды</returns>
    public Task<IList<RentalRecordDto>> GetRentalRecordsByBikeId(int bikeId);
}