using BikeRental.Domain.Data;
using BikeRental.Domain.Enums;

namespace BikeRental.Tests;

/// <summary>
/// Набор тестов для доменной области
/// </summary>
public class RentalTests(DataSeeder data) : IClassFixture<DataSeeder>
{
    /// <summary>
    /// Вывести информацию обо всех спортивных велосипедах (Road и Mountain)
    /// </summary>
    [Fact]
    public void GetAllSportBikes()
    {
        var sportTypes = new[] { BikeType.Road, BikeType.Mountain };

        var result = data.Bikes
            .Where(b => sportTypes.Contains(b.Model!.Type))
            .ToList();

        Assert.NotEmpty(result);
        Assert.All(result, b => Assert.Contains(b.Model!.Type, sportTypes));
    }

    /// <summary>
    /// Вывести топ 5 моделей по прибыли от аренды
    /// </summary>
    [Fact]
    public void GetTop5ModelsByProfit()
    {
        var result = data.BikesModel
            .Select(model => new
            {
                Model = model,
                Profit = model.Bikes!
                    .SelectMany(b => b.RentalRecords!)
                    .Sum(r => (decimal)r.Duration.TotalHours * model.HourlyPrice)
            })
            .OrderByDescending(x => x.Profit)
            .Take(5)
            .ToList();

        Assert.Equal(5, result.Count);
        Assert.All(result, r => Assert.True(r.Profit >= 0));
    }

    /// <summary>
    /// Вывести топ 5 моделей по суммарной длительности аренды
    /// </summary>
    [Fact]
    public void GetTop5ModelsByRentalDuration()
    {
        var result = data.BikesModel
            .Select(model => new
            {
                Model = model,
                TotalHours = model.Bikes!
                    .SelectMany(b => b.RentalRecords!)
                    .Sum(r => r.Duration.TotalHours)
            })
            .OrderByDescending(x => x.TotalHours)
            .Take(5)
            .ToList();

        Assert.Equal(5, result.Count);
    }

    /// <summary>
    /// Вывести минимальное, максимальное и среднее время аренды
    /// </summary>
    [Fact]
    public void GetMinMaxAvgRentalTime()
    {
        var durations = data.RentalRecords
            .Select(r => r.Duration.TotalHours)
            .ToList();

        var min = durations.Min();
        var max = durations.Max();
        var avg = durations.Average();

        Assert.True(min >= 0);
        Assert.True(max >= min);
        Assert.True(avg >= min);
    }

    /// <summary>
    /// Суммарное время аренды велосипедов каждого типа
    /// </summary>
    [Fact]
    public void GetSumRentalTimeByBikeType()
    {
        var result = data.BikesModel
            .GroupBy(m => m.Type)
            .Select(g => new
            {
                Type = g.Key,
                TotalHours = g.SelectMany(m => m.Bikes!)
                              .SelectMany(b => b.RentalRecords!)
                              .Sum(r => r.Duration.TotalHours)
            })
            .ToList();

        Assert.NotEmpty(result);
        Assert.All(result, r => Assert.True(r.TotalHours >= 0));
    }

    /// <summary>
    /// Вывести клиентов, бравших велосипеды больше всего раз
    /// </summary>
    [Fact]
    public void GetTopRentersByRentCount()
    {
        var maxCount = data.Renters
            .Max(r => r.RentalRecords!.Count);

        var result = data.Renters
            .Where(r => r.RentalRecords!.Count == maxCount)
            .ToList();

        Assert.NotEmpty(result);
        Assert.All(result, r => Assert.Equal(maxCount, r.RentalRecords!.Count));
    }
}