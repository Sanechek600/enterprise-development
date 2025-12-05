using BikeRental.Domain;
using BikeRental.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace BikeRental.Infrastructure.EfCore.Repositories;

/// <summary>
/// Репозиторий EF Core для CRUD операций над сущностью <see cref="Renter"/>
/// </summary>
/// <param name="db">Контекст БД</param>
public sealed class RenterRepository(BikeRentalDbContext db) : IRepository<Renter, int>
{
    /// <summary>
    /// Создаёт арендатора и сохраняет изменения в БД
    /// </summary>
    /// <param name="entity">Новый арендатор</param>
    /// <returns>Созданный арендатор с заполненным Id</returns>
    public async Task<Renter> Create(Renter entity)
    {
        db.Renters.Add(entity);
        await db.SaveChangesAsync();
        return entity;
    }

    /// <summary>
    /// Получает арендатора по идентификатору
    /// Подтягивает связанные сущности записи аренды, велосипеды и модели велосипеда
    /// </summary>
    /// <param name="entityId">Идентификатор арендатора</param>
    /// <returns>Арендатор или null если не найден</returns>
    public async Task<Renter?> Read(int entityId)
    {
        return await db.Renters
            .AsNoTracking()
            .Include(x => x.RentalRecords!)
                .ThenInclude(r => r.Bike)
                    .ThenInclude(b => b!.Model)
            .FirstOrDefaultAsync(x => x.Id == entityId);
    }

    /// <summary>
    /// Получает всех арендаторов
    /// Подтягивает связанные сущности записи аренды, велосипеды и модели велосипеда
    /// </summary>
    /// <returns>Список арендаторов</returns>
    public async Task<IList<Renter>> ReadAll()
    {
        return await db.Renters
            .AsNoTracking()
            .Include(x => x.RentalRecords!)
                .ThenInclude(r => r.Bike)
                    .ThenInclude(b => b!.Model)
            .ToListAsync();
    }

    /// <summary>
    /// Обновляет арендатора и сохраняет изменения в БД
    /// </summary>
    /// <param name="entity">Отредактированный арендатор</param>
    /// <returns>Обновлённый арендатор</returns>
    public async Task<Renter> Update(Renter entity)
    {
        db.Renters.Update(entity);
        await db.SaveChangesAsync();
        return entity;
    }

    /// <summary>
    /// Удаляет арендатора по идентификатору
    /// </summary>
    /// <param name="entityId">Идентификатор арендатора</param>
    /// <returns>true если удаление выполнено иначе false</returns>
    public async Task<bool> Delete(int entityId)
    {
        var entity = await db.Renters.FirstOrDefaultAsync(x => x.Id == entityId);
        if (entity is null) return false;

        db.Renters.Remove(entity);
        await db.SaveChangesAsync();
        return true;
    }
}