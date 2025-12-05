using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BikeRental.Infrastructure.EfCore.Migrations
{
    /// <inheritdoc />
    public partial class AddData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "BikeModel",
                columns: new[] { "Id", "BikeWeight", "BrakeType", "HourlyPrice", "MaxRiderWeight", "Type", "WheelSize", "Year" },
                values: new object[,]
                {
                    { 1, 9.0, "Caliper", 150m, 100.0, 0, 28, 2021 },
                    { 2, 13.0, "Disc", 180m, 110.0, 1, 29, 2020 },
                    { 3, 14.0, "Drum", 100m, 120.0, 2, 26, 2019 },
                    { 4, 22.0, "Disc", 400m, 120.0, 3, 27, 2022 },
                    { 5, 8.0, "V-Brake", 60m, 60.0, 4, 20, 2023 },
                    { 6, 13.0, "Disc", 160m, 105.0, 1, 27, 2018 },
                    { 7, 15.0, "Coaster", 95m, 130.0, 2, 28, 2017 },
                    { 8, 8.0, "Disc", 220m, 95.0, 0, 28, 2024 },
                    { 9, 25.0, "Hydraulic Disc", 450m, 125.0, 3, 26, 2021 },
                    { 10, 7.0, "Coaster", 50m, 40.0, 4, 16, 2020 }
                });

            migrationBuilder.InsertData(
                table: "Renter",
                columns: new[] { "Id", "FullName", "Phone" },
                values: new object[,]
                {
                    { 1, "Иванов Иван Иванович", "+7-910-000-0001" },
                    { 2, "Петров Пётр Петрович", "+7-910-000-0002" },
                    { 3, "Сидоров Сидор Сидорович", "+7-910-000-0003" },
                    { 4, "Кузнецова Анна Андреевна", "+7-910-000-0004" },
                    { 5, "Морозов Дмитрий Сергеевич", "+7-910-000-0005" },
                    { 6, "Васильева Ольга Николаевна", "+7-910-000-0006" },
                    { 7, "Смирнов Алексей Викторович", "+7-910-000-0007" },
                    { 8, "Никитина Мария Ивановна", "+7-910-000-0008" },
                    { 9, "Орлов Сергей Павлович", "+7-910-000-0009" },
                    { 10, "Белова Екатерина Михайловна", "+7-910-000-0010" }
                });

            migrationBuilder.InsertData(
                table: "Bike",
                columns: new[] { "Id", "Color", "ModelId", "SerialNumber" },
                values: new object[,]
                {
                    { 1, "Красный", 1, "SN1001" },
                    { 2, "Чёрный", 2, "SN1002" },
                    { 3, "Белый", 3, "SN1003" },
                    { 4, "Синий", 4, "SN1004" },
                    { 5, "Зелёный", 5, "SN1005" },
                    { 6, "Жёлтый", 6, "SN1006" },
                    { 7, "Серый", 7, "SN1007" },
                    { 8, "Оранжевый", 8, "SN1008" },
                    { 9, "Фиолетовый", 9, "SN1009" },
                    { 10, "Розовый", 10, "SN1010" }
                });

            migrationBuilder.InsertData(
                table: "RentalRecord",
                columns: new[] { "Id", "BikeId", "Duration", "RenterId", "StartTime" },
                values: new object[,]
                {
                    { 1, 1, new TimeSpan(0, 2, 0, 0, 0), 1, new DateTime(2023, 12, 2, 7, 0, 0, 0, DateTimeKind.Utc) },
                    { 2, 2, new TimeSpan(0, 4, 0, 0, 0), 2, new DateTime(2023, 12, 7, 5, 0, 0, 0, DateTimeKind.Utc) },
                    { 3, 3, new TimeSpan(0, 1, 0, 0, 0), 3, new DateTime(2023, 12, 12, 8, 0, 0, 0, DateTimeKind.Utc) },
                    { 4, 4, new TimeSpan(0, 6, 0, 0, 0), 4, new DateTime(2023, 12, 14, 4, 0, 0, 0, DateTimeKind.Utc) },
                    { 5, 5, new TimeSpan(0, 3, 0, 0, 0), 5, new DateTime(2023, 12, 17, 9, 0, 0, 0, DateTimeKind.Utc) },
                    { 6, 6, new TimeSpan(0, 5, 0, 0, 0), 6, new DateTime(2023, 12, 20, 6, 0, 0, 0, DateTimeKind.Utc) },
                    { 7, 7, new TimeSpan(0, 7, 0, 0, 0), 7, new DateTime(2023, 12, 22, 3, 0, 0, 0, DateTimeKind.Utc) },
                    { 8, 8, new TimeSpan(0, 2, 0, 0, 0), 8, new DateTime(2023, 12, 23, 8, 0, 0, 0, DateTimeKind.Utc) },
                    { 9, 9, new TimeSpan(0, 8, 0, 0, 0), 9, new DateTime(2023, 12, 25, 2, 0, 0, 0, DateTimeKind.Utc) },
                    { 10, 10, new TimeSpan(0, 3, 0, 0, 0), 10, new DateTime(2023, 12, 26, 7, 0, 0, 0, DateTimeKind.Utc) },
                    { 11, 2, new TimeSpan(0, 2, 0, 0, 0), 1, new DateTime(2023, 12, 27, 9, 0, 0, 0, DateTimeKind.Utc) },
                    { 12, 2, new TimeSpan(0, 3, 0, 0, 0), 2, new DateTime(2023, 12, 28, 8, 0, 0, 0, DateTimeKind.Utc) },
                    { 13, 1, new TimeSpan(0, 4, 0, 0, 0), 3, new DateTime(2023, 12, 29, 8, 0, 0, 0, DateTimeKind.Utc) },
                    { 14, 4, new TimeSpan(0, 1, 0, 0, 0), 4, new DateTime(2023, 12, 30, 7, 0, 0, 0, DateTimeKind.Utc) },
                    { 15, 4, new TimeSpan(0, 5, 0, 0, 0), 5, new DateTime(2023, 12, 31, 5, 0, 0, 0, DateTimeKind.Utc) },
                    { 16, 9, new TimeSpan(0, 6, 0, 0, 0), 6, new DateTime(2023, 12, 12, 4, 0, 0, 0, DateTimeKind.Utc) },
                    { 17, 9, new TimeSpan(0, 2, 0, 0, 0), 7, new DateTime(2023, 12, 17, 6, 0, 0, 0, DateTimeKind.Utc) },
                    { 18, 8, new TimeSpan(0, 3, 0, 0, 0), 8, new DateTime(2023, 12, 24, 9, 0, 0, 0, DateTimeKind.Utc) },
                    { 19, 6, new TimeSpan(0, 4, 0, 0, 0), 9, new DateTime(2023, 12, 21, 7, 0, 0, 0, DateTimeKind.Utc) },
                    { 20, 5, new TimeSpan(0, 2, 0, 0, 0), 10, new DateTime(2023, 12, 30, 8, 0, 0, 0, DateTimeKind.Utc) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "RentalRecord",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "RentalRecord",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "RentalRecord",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "RentalRecord",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "RentalRecord",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "RentalRecord",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "RentalRecord",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "RentalRecord",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "RentalRecord",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "RentalRecord",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "RentalRecord",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "RentalRecord",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "RentalRecord",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "RentalRecord",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "RentalRecord",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "RentalRecord",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "RentalRecord",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "RentalRecord",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "RentalRecord",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "RentalRecord",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Bike",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Bike",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Bike",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Bike",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Bike",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Bike",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Bike",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Bike",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Bike",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Bike",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Renter",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Renter",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Renter",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Renter",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Renter",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Renter",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Renter",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Renter",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Renter",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Renter",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "BikeModel",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "BikeModel",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "BikeModel",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "BikeModel",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "BikeModel",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "BikeModel",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "BikeModel",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "BikeModel",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "BikeModel",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "BikeModel",
                keyColumn: "Id",
                keyValue: 10);
        }
    }
}
