using AutoMapper;
using BikeRental.Application.Contracts;
using BikeRental.Application.Contracts.Renters;
using BikeRental.Domain;
using BikeRental.Domain.Models;

namespace BikeRental.Application.Services;

/// <summary>
/// Служба приложения для работы с арендаторами
/// </summary>
/// <param name="renterRepository">Репозиторий арендаторов</param>
/// <param name="mapper">Объект маппинга</param>
public class RenterService(IRepository<Renter, int> renterRepository, IMapper mapper)
    : IApplicationService<RenterDto, RenterCreateUpdateDto, int>
{
    /// <summary>
    /// Создаёт арендатора
    /// </summary>
    /// <param name="dto">DTO для создания и обновления арендатора</param>
    /// <returns>DTO арендатора</returns>
    public async Task<RenterDto> Create(RenterCreateUpdateDto dto)
    {
        var entity = mapper.Map<Renter>(dto);

        var created = await renterRepository.Create(entity);
        return mapper.Map<RenterDto>(created);
    }

    /// <summary>
    /// Возвращает арендатора по идентификатору
    /// </summary>
    /// <param name="dtoId">Идентификатор арендатора</param>
    /// <returns>DTO арендатора</returns>
    public async Task<RenterDto?> Get(int dtoId)
    {
        var entity = await renterRepository.Read(dtoId)
            ?? throw new KeyNotFoundException($"Арендатор с идентификатором {dtoId} не найден");

        return mapper.Map<RenterDto>(entity);
    }

    /// <summary>
    /// Возвращает список арендаторов
    /// </summary>
    /// <returns>Список DTO арендаторов</returns>
    public async Task<IList<RenterDto>> GetAll()
    {
        var items = await renterRepository.ReadAll();
        return [.. items.Select(mapper.Map<RenterDto>)];
    }

    /// <summary>
    /// Обновляет арендатора по идентификатору
    /// </summary>
    /// <param name="dto">DTO для создания и обновления арендатора</param>
    /// <param name="dtoId">Идентификатор арендатора</param>
    /// <returns>DTO арендатора</returns>
    public async Task<RenterDto> Update(RenterCreateUpdateDto dto, int dtoId)
    {
        var entity = await renterRepository.Read(dtoId)
            ?? throw new KeyNotFoundException($"Арендатор с идентификатором {dtoId} не найден");

        mapper.Map(dto, entity);

        var updated = await renterRepository.Update(entity);
        return mapper.Map<RenterDto>(updated);
    }

    /// <summary>
    /// Удаляет арендатора по идентификатору
    /// </summary>
    /// <param name="dtoId">Идентификатор арендатора</param>
    /// <returns>true если удаление выполнено иначе false</returns>
    public Task<bool> Delete(int dtoId) => renterRepository.Delete(dtoId);
}