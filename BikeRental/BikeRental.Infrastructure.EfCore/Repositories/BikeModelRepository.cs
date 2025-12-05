using BikeRental.Domain;
using BikeRental.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace BikeRental.Infrastructure.EfCore.Repositories;

/// <summary>
/// Репозиторий EF Core для CRUD операций над сущностью <see cref="BikeModel"/>
/// </summary>
/// <param name="db">Контекст БД</param>
public sealed class BikeModelRepository(BikeRentalDbContext db) : IRepository<BikeModel, int>
{
    /// <summary>
    /// Создаёт модель велосипеда и сохраняет изменения в БД
    /// </summary>
    /// <param name="entity">Новая модель велосипеда</param>
    /// <returns>Созданная модель велосипеда с заполненным Id</returns>
    public async Task<BikeModel> Create(BikeModel entity)
    {
        db.BikeModels.Add(entity);
        await db.SaveChangesAsync();
        return entity;
    }

    /// <summary>
    /// Получает модель велосипеда по идентификатору
    /// Подтягивает связанные сущности велосипеды, записи аренды и арендаторы
    /// </summary>
    /// <param name="entityId">Идентификатор модели велосипеда</param>
    /// <returns>Модель велосипеда или null если не найдена</returns>
    public async Task<BikeModel?> Read(int entityId)
    {
        return await db.BikeModels
            .AsNoTracking()
            .Include(x => x.Bikes!)
                .ThenInclude(b => b.RentalRecords!)
                    .ThenInclude(r => r.Renter)
            .FirstOrDefaultAsync(x => x.Id == entityId);
    }

    /// <summary>
    /// Получает все модели велосипеда
    /// Подтягивает связанные сущности велосипеды, записи аренды и арендаторы
    /// </summary>
    /// <returns>Список моделей велосипеда</returns>
    public async Task<IList<BikeModel>> ReadAll()
    {
        return await db.BikeModels
            .AsNoTracking()
            .Include(x => x.Bikes!)
                .ThenInclude(b => b.RentalRecords!)
                    .ThenInclude(r => r.Renter)
            .ToListAsync();
    }

    /// <summary>
    /// Обновляет модель велосипеда и сохраняет изменения в БД
    /// </summary>
    /// <param name="entity">Отредактированная модель велосипеда</param>
    /// <returns>Обновлённая модель велосипеда</returns>
    public async Task<BikeModel> Update(BikeModel entity)
    {
        db.BikeModels.Update(entity);
        await db.SaveChangesAsync();
        return entity;
    }

    /// <summary>
    /// Удаляет модель велосипеда по идентификатору
    /// </summary>
    /// <param name="entityId">Идентификатор модели велосипеда</param>
    /// <returns>true если удаление выполнено иначе false</returns>
    public async Task<bool> Delete(int entityId)
    {
        var entity = await db.BikeModels.FirstOrDefaultAsync(x => x.Id == entityId);
        if (entity is null) return false;

        db.BikeModels.Remove(entity);
        await db.SaveChangesAsync();
        return true;
    }
}