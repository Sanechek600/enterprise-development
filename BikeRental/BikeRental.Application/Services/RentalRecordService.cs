using AutoMapper;
using BikeRental.Application.Contracts;
using BikeRental.Application.Contracts.RentalRecords;
using BikeRental.Domain;
using BikeRental.Domain.Models;

namespace BikeRental.Application.Services;

/// <summary>
/// Служба приложения для работы с записями аренды
/// </summary>
/// <param name="rentalRecordRepository">Репозиторий записей аренды</param>
/// <param name="renterRepository">Репозиторий арендаторов</param>
/// <param name="bikeRepository">Репозиторий велосипедов</param>
/// <param name="mapper">Объект маппинга</param>
public class RentalRecordService(
    IRepository<RentalRecord, int> rentalRecordRepository,
    IRepository<Renter, int> renterRepository,
    IRepository<Bike, int> bikeRepository,
    IMapper mapper)
    : IRentalRecordService, IApplicationService<RentalRecordDto, RentalRecordCreateUpdateDto, int>
{
    /// <summary>
    /// Создаёт запись аренды
    /// </summary>
    /// <param name="dto">DTO для создания и обновления записи аренды</param>
    /// <returns>DTO записи аренды</returns>
    public async Task<RentalRecordDto> Create(RentalRecordCreateUpdateDto dto)
    {
        _ = await renterRepository.Read(dto.RenterId)
            ?? throw new KeyNotFoundException($"Арендатор с идентификатором {dto.RenterId} не найден");

        _ = await bikeRepository.Read(dto.BikeId)
            ?? throw new KeyNotFoundException($"Велосипед с идентификатором {dto.BikeId} не найден");

        var entity = mapper.Map<RentalRecord>(dto);

        var created = await rentalRecordRepository.Create(entity);
        return mapper.Map<RentalRecordDto>(created);
    }

    /// <summary>
    /// Возвращает запись аренды по идентификатору
    /// </summary>
    /// <param name="dtoId">Идентификатор записи аренды</param>
    /// <returns>DTO записи аренды</returns>
    public async Task<RentalRecordDto?> Get(int dtoId)
    {
        var entity = await rentalRecordRepository.Read(dtoId)
            ?? throw new KeyNotFoundException($"Запись аренды с идентификатором {dtoId} не найдена");

        return mapper.Map<RentalRecordDto>(entity);
    }

    /// <summary>
    /// Возвращает список записей аренды
    /// </summary>
    /// <returns>Список DTO записей аренды</returns>
    public async Task<IList<RentalRecordDto>> GetAll()
    {
        var items = await rentalRecordRepository.ReadAll();
        return [.. items.Select(mapper.Map<RentalRecordDto>)];
    }

    /// <summary>
    /// Обновляет запись аренды по идентификатору
    /// </summary>
    /// <param name="dto">DTO для создания и обновления записи аренды</param>
    /// <param name="dtoId">Идентификатор записи аренды</param>
    /// <returns>DTO записи аренды</returns>
    public async Task<RentalRecordDto> Update(RentalRecordCreateUpdateDto dto, int dtoId)
    {
        var entity = await rentalRecordRepository.Read(dtoId)
            ?? throw new KeyNotFoundException($"Запись аренды с идентификатором {dtoId} не найдена");

        _ = await renterRepository.Read(dto.RenterId)
            ?? throw new KeyNotFoundException($"Арендатор с идентификатором {dto.RenterId} не найден");

        _ = await bikeRepository.Read(dto.BikeId)
            ?? throw new KeyNotFoundException($"Велосипед с идентификатором {dto.BikeId} не найден");

        mapper.Map(dto, entity);

        var updated = await rentalRecordRepository.Update(entity);
        return mapper.Map<RentalRecordDto>(updated);
    }

    /// <summary>
    /// Удаляет запись аренды по идентификатору
    /// </summary>
    /// <param name="dtoId">Идентификатор записи аренды</param>
    /// <returns>true если удаление выполнено иначе false</returns>
    public Task<bool> Delete(int dtoId) => rentalRecordRepository.Delete(dtoId);

    /// <summary>
    /// Возвращает запись аренды по идентификатору арендатора
    /// </summary>
    /// <param name="renterId">Идентификатор арендатора</param>
    /// <returns>DTO записи аренды</returns>
    public async Task<RentalRecordDto> GetRentalRecordByRenterId(int renterId)
    {
        _ = await renterRepository.Read(renterId)
            ?? throw new KeyNotFoundException($"Арендатор с идентификатором {renterId} не найден");

        var record = (await rentalRecordRepository.ReadAll())
            .OrderByDescending(r => r.StartTime)
            .FirstOrDefault(r => r.RenterId == renterId)
            ?? throw new KeyNotFoundException($"Запись аренды для арендатора с идентификатором {renterId} не найдена");

        return mapper.Map<RentalRecordDto>(record);
    }

    /// <summary>
    /// Возвращает запись аренды по идентификатору велосипеда
    /// </summary>
    /// <param name="bikeId">Идентификатор велосипеда</param>
    /// <returns>DTO записи аренды</returns>
    public async Task<RentalRecordDto> GetRentalRecordByBikeId(int bikeId)
    {
        _ = await bikeRepository.Read(bikeId)
            ?? throw new KeyNotFoundException($"Велосипед с идентификатором {bikeId} не найден");

        var record = (await rentalRecordRepository.ReadAll())
            .OrderByDescending(r => r.StartTime)
            .FirstOrDefault(r => r.BikeId == bikeId)
            ?? throw new KeyNotFoundException($"Запись аренды для велосипеда с идентификатором {bikeId} не найдена");

        return mapper.Map<RentalRecordDto>(record);
    }
}