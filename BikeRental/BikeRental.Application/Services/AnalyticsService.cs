using AutoMapper;
using BikeRental.Application.Contracts;
using BikeRental.Application.Contracts.Analytics;
using BikeRental.Application.Contracts.BikeModels;
using BikeRental.Application.Contracts.Bikes;
using BikeRental.Application.Contracts.Renters;
using BikeRental.Domain;
using BikeRental.Domain.Enums;
using BikeRental.Domain.Models;

namespace BikeRental.Application.Services;

/// <summary>
/// Служба приложения для выполнения аналитических запросов
/// </summary>
/// <param name="bikeRepository">Репозиторий велосипедов</param>
/// <param name="bikeModelRepository">Репозиторий моделей велосипедов</param>
/// <param name="renterRepository">Репозиторий арендаторов</param>
/// <param name="rentalRecordRepository">Репозиторий записей аренды</param>
/// <param name="mapper">Объект маппинга</param>
public sealed class AnalyticsService(
    IRepository<Bike, int> bikeRepository,
    IRepository<BikeModel, int> bikeModelRepository,
    IRepository<Renter, int> renterRepository,
    IRepository<RentalRecord, int> rentalRecordRepository,
    IMapper mapper) : IAnalyticsService
{
    /// <summary>
    /// Возвращает список спортивных велосипедов
    /// </summary>
    /// <returns>Список велосипедов</returns>
    public async Task<IList<BikeDto>> GetAllSportBikes()
    {
        var sportTypes = new[] { BikeType.Road, BikeType.Mountain };

        var bikes = await bikeRepository.ReadAll();

        var sportBikes = bikes
            .Where(b => b.Model is not null && sportTypes.Contains(b.Model.Type))
            .ToList();

        return [.. sportBikes.Select(mapper.Map<BikeDto>)];
    }

    /// <summary>
    /// Возвращает топ 5 моделей велосипедов по прибыли от аренды
    /// </summary>
    /// <returns>Список моделей с рассчитанной прибылью</returns>
    public async Task<IList<BikeModelProfitDto>> GetTop5ModelsByProfit()
    {
        var models = await bikeModelRepository.ReadAll();

        var result = models
            .Select(m => new BikeModelProfitDto(
                mapper.Map<BikeModelDto>(m),
                Profit: (m.Bikes ?? [])
                    .SelectMany(b => b.RentalRecords ?? [])
                    .Sum(r => (decimal)r.Duration.TotalHours * m.HourlyPrice)))
            .OrderByDescending(x => x.Profit)
            .Take(5)
            .ToList();

        return result;
    }

    /// <summary>
    /// Возвращает топ 5 моделей велосипедов по суммарной длительности аренды
    /// </summary>
    /// <returns>Список моделей с суммарным временем аренды</returns>
    public async Task<IList<BikeModelTotalDurationDto>> GetTop5ModelsByRentalDuration()
    {
        var models = await bikeModelRepository.ReadAll();

        var result = models
            .Select(m => new BikeModelTotalDurationDto(
                mapper.Map<BikeModelDto>(m),
                TotalHours: (m.Bikes ?? [])
                    .SelectMany(b => b.RentalRecords ?? [])
                    .Sum(r => r.Duration.TotalHours)))
            .OrderByDescending(x => x.TotalHours)
            .Take(5)
            .ToList();

        return result;
    }

    /// <summary>
    /// Возвращает минимальное максимальное и среднее время аренды
    /// </summary>
    /// <returns>Статистика по длительности аренды</returns>
    public async Task<RentalDurationStatsDto> GetMinMaxAvgRentalTime()
    {
        var records = await rentalRecordRepository.ReadAll();

        if (records.Count == 0)
            throw new KeyNotFoundException("Записи аренды не найдены");

        var durations = records.Select(r => r.Duration.TotalHours).ToList();

        return new RentalDurationStatsDto(
            MinHours: durations.Min(),
            MaxHours: durations.Max(),
            AvgHours: durations.Average());
    }

    /// <summary>
    /// Возвращает суммарное время аренды велосипедов по типам
    /// </summary>
    /// <returns>Список типов велосипедов с суммарным временем аренды</returns>
    public async Task<IList<BikeTypeTotalDurationDto>> GetSumRentalTimeByBikeType()
    {
        var models = await bikeModelRepository.ReadAll();

        var result = models
            .GroupBy(m => m.Type)
            .Select(g => new BikeTypeTotalDurationDto(
                Type: g.Key,
                TotalHours: g.SelectMany(m => m.Bikes ?? [])
                    .SelectMany(b => b.RentalRecords ?? [])
                    .Sum(r => r.Duration.TotalHours)))
            .ToList();

        return result;
    }

    /// <summary>
    /// Возвращает клиентов бравших велосипеды на прокат больше всего раз
    /// </summary>
    /// <returns>Список клиентов с максимальным количеством аренд</returns>
    public async Task<IList<RenterTopByRentCountDto>> GetTopRentersByRentCount()
    {
        var renters = await renterRepository.ReadAll();

        if (renters.Count == 0)
            throw new KeyNotFoundException("Клиенты не найдены");

        var maxCount = renters.Max(r => (r.RentalRecords ?? []).Count);

        if (maxCount == 0)
            throw new KeyNotFoundException("У клиентов отсутствуют записи аренды");

        var result = renters
            .Where(r => (r.RentalRecords ?? []).Count == maxCount)
            .Select(r => new RenterTopByRentCountDto(
                Renter: mapper.Map<RenterDto>(r),
                RentCount: maxCount))
            .ToList();

        return result;
    }
}