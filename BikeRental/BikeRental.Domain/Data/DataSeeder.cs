using BikeRental.Domain.Enums;
using BikeRental.Domain.Models;

namespace BikeRental.Domain.Data;

/// <summary>
/// Сидер данных, создающий тестовые коллекции сущностей для тестов
/// </summary>
public class DataSeeder
{
    /// <summary>
    /// Список велосипедов
    /// </summary>
    public List<Bike> Bikes { get; }

    /// <summary>
    /// Список моделей велосипедов
    /// </summary>
    public List<BikeModel> BikesModel { get; }

    /// <summary>
    /// Список записей аренды
    /// </summary>
    public List<RentalRecord> RentalRecords { get; }

    /// <summary>
    /// Список арендаторов
    /// </summary>
    public List<Renter> Renters { get; }

    /// <summary>
    /// Конструктор, инициализирующий коллекции и заполняющий их связи
    /// </summary>
    public DataSeeder()
    {
        BikesModel =
        [
            new BikeModel { Id = 1, Type = BikeType.Road, WheelSize = 28, MaxRiderWeight = 100, BikeWeight = 9, BrakeType = "Caliper", Year = 2021, HourlyPrice = 150m },
            new BikeModel { Id = 2, Type = BikeType.Mountain, WheelSize = 29, MaxRiderWeight = 110, BikeWeight = 13, BrakeType = "Disc", Year = 2020, HourlyPrice = 180m },
            new BikeModel { Id = 3, Type = BikeType.City, WheelSize = 26, MaxRiderWeight = 120, BikeWeight = 14, BrakeType = "Drum", Year = 2019, HourlyPrice = 100m },
            new BikeModel { Id = 4, Type = BikeType.Electric, WheelSize = 27, MaxRiderWeight = 120, BikeWeight = 22, BrakeType = "Disc", Year = 2022, HourlyPrice = 400m },
            new BikeModel { Id = 5, Type = BikeType.Kids, WheelSize = 20, MaxRiderWeight = 60, BikeWeight = 8, BrakeType = "V-Brake", Year = 2023, HourlyPrice = 60m },
            new BikeModel { Id = 6, Type = BikeType.Mountain, WheelSize = 27, MaxRiderWeight = 105, BikeWeight = 13, BrakeType = "Disc", Year = 2018, HourlyPrice = 160m },
            new BikeModel { Id = 7, Type = BikeType.City, WheelSize = 28, MaxRiderWeight = 130, BikeWeight = 15, BrakeType = "Coaster", Year = 2017, HourlyPrice = 95m },
            new BikeModel { Id = 8, Type = BikeType.Road, WheelSize = 28, MaxRiderWeight = 95, BikeWeight = 8, BrakeType = "Disc", Year = 2024, HourlyPrice = 220m },
            new BikeModel { Id = 9, Type = BikeType.Electric, WheelSize = 26, MaxRiderWeight = 125, BikeWeight = 25, BrakeType = "Hydraulic Disc", Year = 2021, HourlyPrice = 450m },
            new BikeModel { Id = 10, Type = BikeType.Kids, WheelSize = 16, MaxRiderWeight = 40, BikeWeight = 7, BrakeType = "Coaster", Year = 2020, HourlyPrice = 50m }
        ];

        Renters =
        [
            new Renter { Id = 1, FullName = "Иванов Иван Иванович", Phone = "+7-910-000-0001" },
            new Renter { Id = 2, FullName = "Петров Пётр Петрович", Phone = "+7-910-000-0002" },
            new Renter { Id = 3, FullName = "Сидоров Сидор Сидорович", Phone = "+7-910-000-0003" },
            new Renter { Id = 4, FullName = "Кузнецова Анна Андреевна", Phone = "+7-910-000-0004" },
            new Renter { Id = 5, FullName = "Морозов Дмитрий Сергеевич", Phone = "+7-910-000-0005" },
            new Renter { Id = 6, FullName = "Васильева Ольга Николаевна", Phone = "+7-910-000-0006" },
            new Renter { Id = 7, FullName = "Смирнов Алексей Викторович", Phone = "+7-910-000-0007" },
            new Renter { Id = 8, FullName = "Никитина Мария Ивановна", Phone = "+7-910-000-0008" },
            new Renter { Id = 9, FullName = "Орлов Сергей Павлович", Phone = "+7-910-000-0009" },
            new Renter { Id = 10, FullName = "Белова Екатерина Михайловна", Phone = "+7-910-000-0010" }
        ];

        Bikes =
        [
            new Bike { Id = 1, SerialNumber = "SN1001", Color = "Красный", ModelId = 1, Model = BikesModel[0] },
            new Bike { Id = 2, SerialNumber = "SN1002", Color = "Чёрный", ModelId = 2, Model = BikesModel[1] },
            new Bike { Id = 3, SerialNumber = "SN1003", Color = "Белый", ModelId = 3, Model = BikesModel[2] },
            new Bike { Id = 4, SerialNumber = "SN1004", Color = "Синий", ModelId = 4, Model = BikesModel[3] },
            new Bike { Id = 5, SerialNumber = "SN1005", Color = "Зелёный", ModelId = 5, Model = BikesModel[4] },
            new Bike { Id = 6, SerialNumber = "SN1006", Color = "Жёлтый", ModelId = 6, Model = BikesModel[5] },
            new Bike { Id = 7, SerialNumber = "SN1007", Color = "Серый", ModelId = 7, Model = BikesModel[6] },
            new Bike { Id = 8, SerialNumber = "SN1008", Color = "Оранжевый", ModelId = 8, Model = BikesModel[7] },
            new Bike { Id = 9, SerialNumber = "SN1009", Color = "Фиолетовый", ModelId = 9, Model = BikesModel[8] },
            new Bike { Id = 10, SerialNumber = "SN1010", Color = "Розовый", ModelId = 10, Model = BikesModel[9] }
        ];

        for (var i = 0; i < 10; i++)
            BikesModel[i].Bikes = [Bikes[i]];

        var baseDate = new DateTime(2024, 01, 01, 10, 0, 0, DateTimeKind.Utc);

        RentalRecords =
        [
            new RentalRecord 
            { 
                Id = 1, 
                Renter = Renters[0], 
                RenterId = 1, 
                Bike = Bikes[0], 
                BikeId = 1, 
                StartTime = baseDate.AddDays(-30).AddHours(-3), 
                Duration = TimeSpan.FromHours(2) 
            },
            new RentalRecord 
            { 
                Id = 2, 
                Renter = Renters[1], 
                RenterId = 2, 
                Bike = Bikes[1], 
                BikeId = 2, 
                StartTime = baseDate.AddDays(-25).AddHours(-5), 
                Duration = TimeSpan.FromHours(4) 
            },
            new RentalRecord 
            { 
                Id = 3, 
                Renter = Renters[2], 
                RenterId = 3, 
                Bike = Bikes[2], 
                BikeId = 3, 
                StartTime = baseDate.AddDays(-20).AddHours(-2), 
                Duration = TimeSpan.FromHours(1) 
            },
            new RentalRecord 
            { 
                Id = 4, 
                Renter = Renters[3], 
                RenterId = 4, 
                Bike = Bikes[3], 
                BikeId = 4, 
                StartTime = baseDate.AddDays(-18).AddHours(-6), 
                Duration = TimeSpan.FromHours(6) 
            },
            new RentalRecord 
            { 
                Id = 5, 
                Renter = Renters[4], 
                RenterId = 5, 
                Bike = Bikes[4], 
                BikeId = 5, 
                StartTime = baseDate.AddDays(-15).AddHours(-1), 
                Duration = TimeSpan.FromHours(3) 
            },
            new RentalRecord 
            { 
                Id = 6, 
                Renter = Renters[5], 
                RenterId = 6, 
                Bike = Bikes[5], 
                BikeId = 6, 
                StartTime = baseDate.AddDays(-12).AddHours(-4), 
                Duration = TimeSpan.FromHours(5) 
            },
            new RentalRecord 
            { 
                Id = 7, 
                Renter = Renters[6], 
                RenterId = 7, 
                Bike = Bikes[6], 
                BikeId = 7, 
                StartTime = baseDate.AddDays(-10).AddHours(-7), 
                Duration = TimeSpan.FromHours(7) 
            },
            new RentalRecord 
            { 
                Id = 8, 
                Renter = Renters[7], 
                RenterId = 8, 
                Bike = Bikes[7], 
                BikeId = 8, 
                StartTime = baseDate.AddDays(-9).AddHours(-2), 
                Duration = TimeSpan.FromHours(2) 
            },
            new RentalRecord 
            { 
                Id = 9, 
                Renter = Renters[8], 
                RenterId = 9, 
                Bike = Bikes[8], 
                BikeId = 9, 
                StartTime = baseDate.AddDays(-7).AddHours(-8), 
                Duration = TimeSpan.FromHours(8) 
            },
            new RentalRecord 
            { 
                Id = 10,
                Renter = Renters[9], 
                RenterId = 10,
                Bike = Bikes[9], 
                BikeId = 10,
                StartTime = baseDate.AddDays(-6).AddHours(-3), 
                Duration = TimeSpan.FromHours(3) 
            },
            new RentalRecord 
            { 
                Id = 11,
                Renter = Renters[0], 
                RenterId = 1, 
                Bike = Bikes[1], 
                BikeId = 2, 
                StartTime = baseDate.AddDays(-5).AddHours(-1), 
                Duration = TimeSpan.FromHours(2) 
            },
            new RentalRecord 
            { 
                Id = 12,
                Renter = Renters[1], 
                RenterId = 2, 
                Bike = Bikes[1], 
                BikeId = 2, 
                StartTime = baseDate.AddDays(-4).AddHours(-2), 
                Duration = TimeSpan.FromHours(3) 
            },
            new RentalRecord 
            { 
                Id = 13,
                Renter = Renters[2], 
                RenterId = 3, 
                Bike = Bikes[0], 
                BikeId = 1, 
                StartTime = baseDate.AddDays(-3).AddHours(-2), 
                Duration = TimeSpan.FromHours(4) 
            },
            new RentalRecord 
            { 
                Id = 14,
                Renter = Renters[3], 
                RenterId = 4, 
                Bike = Bikes[3], 
                BikeId = 4, 
                StartTime = baseDate.AddDays(-2).AddHours(-3), 
                Duration = TimeSpan.FromHours(1) 
            },
            new RentalRecord 
            { 
                Id = 15,
                Renter = Renters[4], 
                RenterId = 5, 
                Bike = Bikes[3], 
                BikeId = 4, 
                StartTime = baseDate.AddDays(-1).AddHours(-5), 
                Duration = TimeSpan.FromHours(5) 
            },
            new RentalRecord 
            { 
                Id = 16,
                Renter = Renters[5], 
                RenterId = 6, 
                Bike = Bikes[8], 
                BikeId = 9, 
                StartTime = baseDate.AddDays(-20).AddHours(-6), 
                Duration = TimeSpan.FromHours(6) 
            },
            new RentalRecord 
            { 
                Id = 17,
                Renter = Renters[6], 
                RenterId = 7, 
                Bike = Bikes[8], 
                BikeId = 9, 
                StartTime = baseDate.AddDays(-15).AddHours(-4), 
                Duration = TimeSpan.FromHours(2) 
            },
            new RentalRecord 
            { 
                Id = 18,
                Renter = Renters[7], 
                RenterId = 8, 
                Bike = Bikes[7], 
                BikeId = 8, 
                StartTime = baseDate.AddDays(-8).AddHours(-1), 
                Duration = TimeSpan.FromHours(3) 
            },
            new RentalRecord 
            { 
                Id = 19,
                Renter = Renters[8], 
                RenterId = 9, 
                Bike = Bikes[5], 
                BikeId = 6, 
                StartTime = baseDate.AddDays(-11).AddHours(-3), 
                Duration = TimeSpan.FromHours(4) 
            },
            new RentalRecord 
            { 
                Id = 20,
                Renter = Renters[9], 
                RenterId = 10,
                Bike = Bikes[4], 
                BikeId = 5, 
                StartTime = baseDate.AddDays(-2).AddHours(-2), 
                Duration = TimeSpan.FromHours(2) 
            }
        ];


        foreach (var bike in Bikes)
            bike.RentalRecords = [.. RentalRecords.Where(r => r.BikeId == bike.Id)];

        foreach (var renter in Renters)
            renter.RentalRecords = [.. RentalRecords.Where(r => r.RenterId == renter.Id)];
    }
}