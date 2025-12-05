using AutoMapper;
using BikeRental.Application.Contracts.BikeModels;
using BikeRental.Application.Contracts.Bikes;
using BikeRental.Domain;
using BikeRental.Domain.Models;

namespace BikeRental.Application.Services;

/// <summary>
/// Служба приложения для работы с велосипедами
/// </summary>
/// <param name="bikeRepository">Репозиторий велосипедов</param>
/// <param name="modelRepository">Репозиторий моделей велосипедов</param>
/// <param name="mapper">Объект маппинга</param>
public class BikeService(IRepository<Bike, int> bikeRepository, IRepository<BikeModel, int> modelRepository, IMapper mapper)
    : IBikeService
{
    /// <summary>
    /// Создаёт велосипед
    /// </summary>
    /// <param name="dto">DTO для создания и обновления велосипеда</param>
    /// <returns>DTO велосипеда</returns>
    public async Task<BikeDto> Create(BikeCreateUpdateDto dto)
    {
        _ = await modelRepository.Read(dto.ModelId)
            ?? throw new KeyNotFoundException($"Модель велосипеда с идентификатором {dto.ModelId} не найдена");

        var entity = mapper.Map<Bike>(dto);

        var created = await bikeRepository.Create(entity);
        return mapper.Map<BikeDto>(created);
    }

    /// <summary>
    /// Возвращает велосипед по идентификатору
    /// </summary>
    /// <param name="dtoId">Идентификатор велосипеда</param>
    /// <returns>DTO велосипеда</returns>
    public async Task<BikeDto?> Get(int dtoId)
    {
        var entity = await bikeRepository.Read(dtoId)
            ?? throw new KeyNotFoundException($"Велосипед с идентификатором {dtoId} не найден");

        return mapper.Map<BikeDto>(entity);
    }

    /// <summary>
    /// Возвращает список велосипедов
    /// </summary>
    /// <returns>Список DTO велосипедов</returns>
    public async Task<IList<BikeDto>> GetAll()
    {
        var items = await bikeRepository.ReadAll();
        return [.. items.Select(mapper.Map<BikeDto>)];
    }

    /// <summary>
    /// Обновляет велосипед по идентификатору
    /// </summary>
    /// <param name="dto">DTO для создания и обновления велосипеда</param>
    /// <param name="dtoId">Идентификатор велосипеда</param>
    /// <returns>DTO велосипеда</returns>
    public async Task<BikeDto> Update(BikeCreateUpdateDto dto, int dtoId)
    {
        var entity = await bikeRepository.Read(dtoId)
            ?? throw new KeyNotFoundException($"Велосипед с идентификатором {dtoId} не найден");

        _ = await modelRepository.Read(dto.ModelId)
            ?? throw new KeyNotFoundException($"Модель велосипеда с идентификатором {dto.ModelId} не найдена");

        mapper.Map(dto, entity);

        var updated = await bikeRepository.Update(entity);
        return mapper.Map<BikeDto>(updated);
    }

    /// <summary>
    /// Удаляет велосипед по идентификатору
    /// </summary>
    /// <param name="dtoId">Идентификатор велосипеда</param>
    /// <returns>true если удаление выполнено иначе false</returns>
    public Task<bool> Delete(int dtoId) => bikeRepository.Delete(dtoId);

    /// <summary>
    /// Возвращает модель велосипеда по идентификатору велосипеда
    /// </summary>
    /// <param name="bikeId">Идентификатор велосипеда</param>
    /// <returns>DTO модели велосипеда</returns>
    public async Task<BikeModelDto> GetBikeModel(int bikeId)
    {
        var bike = await bikeRepository.Read(bikeId)
            ?? throw new KeyNotFoundException($"Велосипед с идентификатором {bikeId} не найден");

        var model = await modelRepository.Read(bike.ModelId)
            ?? throw new KeyNotFoundException($"Модель велосипеда с идентификатором {bike.ModelId} не найдена");

        return mapper.Map<BikeModelDto>(model);
    }
}