using BikeRental.Domain;
using BikeRental.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace BikeRental.Infrastructure.EfCore.Repositories;

/// <summary>
/// Репозиторий EF Core для CRUD операций над сущностью <see cref="RentalRecord"/>
/// </summary>
/// <param name="db">Контекст БД</param>
public sealed class RentalRecordRepository(BikeRentalDbContext db) : IRepository<RentalRecord, int>
{
    /// <summary>
    /// Создаёт запись аренды и сохраняет изменения в БД
    /// </summary>
    /// <param name="entity">Новая запись аренды</param>
    /// <returns>Созданная запись с заполненным Id</returns>
    public async Task<RentalRecord> Create(RentalRecord entity)
    {
        db.RentalRecords.Add(entity);
        await db.SaveChangesAsync();
        return entity;
    }

    /// <summary>
    /// Получает запись аренды по идентификатору
    /// Подтягивает связанные сущности арендатора, велосипед и модель велосипеда
    /// </summary>
    /// <param name="entityId">Идентификатор записи аренды</param>
    /// <returns>Запись аренды или null если не найдена</returns>
    public async Task<RentalRecord?> Read(int entityId)
    {
        return await db.RentalRecords
            .AsNoTracking()
            .Include(x => x.Renter)
            .Include(x => x.Bike)
                .ThenInclude(b => b!.Model)
            .FirstOrDefaultAsync(x => x.Id == entityId);
    }

    /// <summary>
    /// Получает все записи аренды
    /// Подтягивает связанные сущности арендатора, велосипед и модель велосипеда
    /// </summary>
    /// <returns>Список записей аренды</returns>
    public async Task<IList<RentalRecord>> ReadAll()
    {
        return await db.RentalRecords
            .AsNoTracking()
            .Include(x => x.Renter)
            .Include(x => x.Bike)
                .ThenInclude(b => b!.Model)
            .OrderByDescending(x => x.StartTime)
            .ToListAsync();
    }

    /// <summary>
    /// Обновляет запись аренды и сохраняет изменения в БД
    /// </summary>
    /// <param name="entity">Отредактированная запись аренды</param>
    /// <returns>Обновлённая запись</returns>
    public async Task<RentalRecord> Update(RentalRecord entity)
    {
        db.RentalRecords.Update(entity);
        await db.SaveChangesAsync();
        return entity;
    }

    /// <summary>
    /// Удаляет запись аренды по идентификатору
    /// </summary>
    /// <param name="entityId">Идентификатор записи аренды</param>
    /// <returns>true если удаление выполнено иначе false</returns>
    public async Task<bool> Delete(int entityId)
    {
        var entity = await db.RentalRecords.FirstOrDefaultAsync(x => x.Id == entityId);
        if (entity is null) return false;

        db.RentalRecords.Remove(entity);
        await db.SaveChangesAsync();
        return true;
    }
}