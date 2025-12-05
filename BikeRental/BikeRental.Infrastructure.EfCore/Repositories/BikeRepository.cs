using BikeRental.Domain;
using BikeRental.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace BikeRental.Infrastructure.EfCore.Repositories;

/// <summary>
/// Репозиторий EF Core для CRUD операций над сущностью <see cref="Bike"/>
/// </summary>
/// <param name="db">Контекст БД</param>
public sealed class BikeRepository(BikeRentalDbContext db) : IRepository<Bike, int>
{
    /// <summary>
    /// Создаёт велосипед и сохраняет изменения в БД
    /// </summary>
    /// <param name="entity">Новый велосипед</param>
    /// <returns>Созданный велосипед с заполненным Id</returns>
    public async Task<Bike> Create(Bike entity)
    {
        db.Bikes.Add(entity);
        await db.SaveChangesAsync();
        return entity;
    }

    /// <summary>
    /// Получает велосипед по идентификатору
    /// Подтягивает связанные сущности модель велосипеда, записи аренды и арендаторы
    /// </summary>
    /// <param name="entityId">Идентификатор велосипеда</param>
    /// <returns>Велосипед или null если не найден</returns>
    public async Task<Bike?> Read(int entityId)
    {
        return await db.Bikes
            .Include(x => x.Model)
            .Include(x => x.RentalRecords!)
                .ThenInclude(r => r.Renter)
            .FirstOrDefaultAsync(x => x.Id == entityId);
    }

    /// <summary>
    /// Получает все велосипеды
    /// Подтягивает связанные сущности модель велосипеда, записи аренды и арендаторы
    /// </summary>
    /// <returns>Список велосипедов</returns>
    public async Task<IList<Bike>> ReadAll()
    {
        return await db.Bikes
            .AsNoTracking()
            .Include(x => x.Model)
            .Include(x => x.RentalRecords!)
                .ThenInclude(r => r.Renter)
            .ToListAsync();
    }

    /// <summary>
    /// Обновляет велосипед и сохраняет изменения в БД
    /// </summary>
    /// <param name="entity">Отредактированный велосипед</param>
    /// <returns>Обновлённый велосипед</returns>
    public async Task<Bike> Update(Bike entity)
    {
        db.Bikes.Update(entity);
        await db.SaveChangesAsync();
        return entity;
    }

    /// <summary>
    /// Удаляет велосипед по идентификатору
    /// </summary>
    /// <param name="entityId">Идентификатор велосипеда</param>
    /// <returns>true если удаление выполнено иначе false</returns>
    public async Task<bool> Delete(int entityId)
    {
        var entity = await db.Bikes.FirstOrDefaultAsync(x => x.Id == entityId);
        if (entity is null) return false;

        db.Bikes.Remove(entity);
        await db.SaveChangesAsync();
        return true;
    }
}