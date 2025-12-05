using BikeRental.Application.Contracts.BikeModels;

namespace BikeRental.Application.Contracts.Bikes;

/// <summary>
/// Интерфейс службы приложения для работы с велосипедами
/// </summary>
public interface IBikeService : IApplicationService<BikeDto, BikeCreateUpdateDto, int>
{
    /// <summary>
    /// Возвращает модель велосипеда по идентификатору велосипеда
    /// </summary>
    /// <param name="bikeId">Идентификатор велосипеда</param>
    /// <returns>DTO модели велосипеда</returns>
    public Task<BikeModelDto> GetBikeModel(int bikeId);
}