using AutoMapper;
using BikeRental.Application.Contracts;
using BikeRental.Application.Contracts.BikeModels;
using BikeRental.Domain;
using BikeRental.Domain.Models;

namespace BikeRental.Application.Services;

/// <summary>
/// Служба приложения для работы с моделями велосипедов
/// </summary>
/// <param name="bikeModelRepository">Репозиторий моделей велосипедов</param>
/// <param name="mapper">Объект маппинга</param>
public class BikeModelService(IRepository<BikeModel, int> bikeModelRepository, IMapper mapper)
    : IApplicationService<BikeModelDto, BikeModelCreateUpdateDto, int>
{
    /// <summary>
    /// Создаёт модель велосипеда
    /// </summary>
    /// <param name="dto">DTO для создания и обновления модели велосипеда</param>
    /// <returns>DTO модели велосипеда</returns>
    public async Task<BikeModelDto> Create(BikeModelCreateUpdateDto dto)
    {
        var entity = mapper.Map<BikeModel>(dto);

        var created = await bikeModelRepository.Create(entity);
        return mapper.Map<BikeModelDto>(created);
    }

    /// <summary>
    /// Возвращает модель велосипеда по идентификатору
    /// </summary>
    /// <param name="dtoId">Идентификатор модели велосипеда</param>
    /// <returns>DTO модели велосипеда</returns>
    public async Task<BikeModelDto?> Get(int dtoId)
    {
        var entity = await bikeModelRepository.Read(dtoId)
            ?? throw new KeyNotFoundException($"Модель велосипеда с идентификатором {dtoId} не найдена");

        return mapper.Map<BikeModelDto>(entity);
    }

    /// <summary>
    /// Возвращает список моделей велосипедов
    /// </summary>
    /// <returns>Список DTO моделей велосипедов</returns>
    public async Task<IList<BikeModelDto>> GetAll()
    {
        var items = await bikeModelRepository.ReadAll();
        return [.. items.Select(mapper.Map<BikeModelDto>)];
    }

    /// <summary>
    /// Обновляет модель велосипеда по идентификатору
    /// </summary>
    /// <param name="dto">DTO для создания и обновления модели велосипеда</param>
    /// <param name="dtoId">Идентификатор модели велосипеда</param>
    /// <returns>DTO модели велосипеда</returns>
    public async Task<BikeModelDto> Update(BikeModelCreateUpdateDto dto, int dtoId)
    {
        var entity = await bikeModelRepository.Read(dtoId)
            ?? throw new KeyNotFoundException($"Модель велосипеда с идентификатором {dtoId} не найдена");

        mapper.Map(dto, entity);

        var updated = await bikeModelRepository.Update(entity);
        return mapper.Map<BikeModelDto>(updated);
    }

    /// <summary>
    /// Удаляет модель велосипеда по идентификатору
    /// </summary>
    /// <param name="dtoId">Идентификатор модели велосипеда</param>
    /// <returns>true если удаление выполнено иначе false</returns>
    public Task<bool> Delete(int dtoId) => bikeModelRepository.Delete(dtoId);
}