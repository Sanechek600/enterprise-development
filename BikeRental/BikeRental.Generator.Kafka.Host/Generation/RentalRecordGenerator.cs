using Bogus;
using BikeRental.Application.Contracts.RentalRecords;
using BikeRental.Generator.Kafka.Host.Configuration;
using Microsoft.Extensions.Options;

namespace BikeRental.Generator.Kafka.Host.Generation;

/// <summary>
/// Генератор случайных записей аренды
/// </summary>
/// <param name="options">Настройки генератора</param>
public class RentalRecordGenerator(IOptions<GeneratorOptions> options)
{
    private readonly GeneratorOptions _options = options.Value;

    private static readonly Faker _faker = new();

    /// <summary>
    /// Генерирует список DTO записей аренды
    /// </summary>
    /// <param name="count">Количество записей аренды</param>
    /// <returns>Список DTO записей аренды</returns>
    public IList<RentalRecordCreateUpdateDto> Generate(int count)
    {
        var result = new List<RentalRecordCreateUpdateDto>(count);

        for (var i = 0; i < count; i++)
        {
            var renterId = _faker.Random.Int(1, _options.MaxRenterId);
            var bikeId = _faker.Random.Int(1, _options.MaxBikeId);

            var startTime = _faker.Date.RecentOffset(_options.DaysRange).DateTime;

            var durationHours = _faker.Random.Int(_options.MinDurationHours, _options.MaxDurationHours);
            var duration = TimeSpan.FromHours(durationHours);

            result.Add(new RentalRecordCreateUpdateDto(
                RenterId: renterId,
                BikeId: bikeId,
                StartTime: startTime,
                Duration: duration));
        }

        return result;
    }
}