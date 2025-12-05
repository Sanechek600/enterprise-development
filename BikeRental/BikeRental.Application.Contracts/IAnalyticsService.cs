using BikeRental.Application.Contracts.Analytics;
using BikeRental.Application.Contracts.Bikes;

namespace BikeRental.Application.Contracts;

/// <summary>
/// Интерфейс аналитической службы приложения
/// Предоставляет агрегированные выборки и метрики по арендам и моделям велосипедов
/// </summary>
public interface IAnalyticsService
{
    /// <summary>
    /// Возвращает список спортивных велосипедов
    /// </summary>
    /// <returns>Список велосипедов</returns>
    public Task<IList<BikeDto>> GetAllSportBikes();

    /// <summary>
    /// Возвращает топ 5 моделей велосипедов по прибыли от аренды
    /// </summary>
    /// <returns>Список моделей с рассчитанной прибылью</returns>
    public Task<IList<BikeModelProfitDto>> GetTop5ModelsByProfit();

    /// <summary>
    /// Возвращает топ 5 моделей велосипедов по суммарной длительности аренды
    /// </summary>
    /// <returns>Список моделей с суммарным временем аренды</returns>
    public Task<IList<BikeModelTotalDurationDto>> GetTop5ModelsByRentalDuration();

    /// <summary>
    /// Возвращает минимальное, максимальное и среднее время аренды
    /// </summary>
    /// <returns>Статистика по длительности аренды</returns>
    public Task<RentalDurationStatsDto> GetMinMaxAvgRentalTime();

    /// <summary>
    /// Возвращает суммарное время аренды велосипедов по типам
    /// </summary>
    /// <returns>Список типов велосипедов с суммарным временем аренды</returns>
    public Task<IList<BikeTypeTotalDurationDto>> GetSumRentalTimeByBikeType();

    /// <summary>
    /// Возвращает клиентов бравших велосипеды на прокат больше всего раз
    /// </summary>
    /// <returns>Список клиентов с максимальным количеством аренд</returns>
    public Task<IList<RenterTopByRentCountDto>> GetTopRentersByRentCount();
}