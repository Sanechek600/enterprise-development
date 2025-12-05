using BikeRental.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace BikeRental.Infrastructure.EfCore;

/// <summary>
/// EF Core DbContext для домена BikeRental
/// Содержит DbSet'ы и конфигурацию сущностей (required, длины строк, связи, индексы, ограничения)
/// </summary>
/// <param name="options">Настройки контекста EF Core</param>
public sealed class BikeRentalDbContext(DbContextOptions<BikeRentalDbContext> options) : DbContext(options)
{
    /// <summary>
    /// Коллекция арендаторов
    /// </summary>
    public required DbSet<Renter> Renters;

    /// <summary>
    /// Коллекция велосипедов
    /// </summary>
    public required DbSet<Bike> Bikes;

    /// <summary>
    /// Коллекция моделей велосипедов
    /// </summary>
    public required DbSet<BikeModel> BikeModels;

    /// <summary>
    /// Коллекция записей об аренде
    /// </summary>
    public required DbSet<RentalRecord> RentalRecords;

    /// <summary>
    /// Конфигурирует модель данных EF Core: таблицы, ключи, required, ограничения длины,
    /// связи между сущностями, поведение при удалении, индексы и check-constraints
    /// </summary>
    /// <param name="modelBuilder">Построитель модели</param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Renter>(b =>
        {
            b.ToTable("Renter");

            b.HasKey(x => x.Id);
            b.Property(x => x.Id).ValueGeneratedOnAdd();

            b.Property(x => x.FullName)
                .IsRequired()
                .HasMaxLength(200);

            b.Property(x => x.Phone)
                .IsRequired()
                .HasMaxLength(32);
        });

        modelBuilder.Entity<BikeModel>(b =>
        {
            b.ToTable("BikeModel", t =>
            {
                t.HasCheckConstraint("CK_BikeModels_WheelSize", "[WheelSize] > 0");
                t.HasCheckConstraint("CK_BikeModels_HourlyPrice", "[HourlyPrice] > 0");
                t.HasCheckConstraint("CK_BikeModels_MaxRiderWeight", "[MaxRiderWeight] > 0");
                t.HasCheckConstraint("CK_BikeModels_Year", "[Year] IS NULL OR ([Year] >= 1900 AND [Year] <= 2100)");
            });

            b.HasKey(x => x.Id);
            b.Property(x => x.Id).ValueGeneratedOnAdd();

            b.Property(x => x.Type)
                .IsRequired()
                .HasConversion<int>();

            b.Property(x => x.WheelSize).IsRequired();
            b.Property(x => x.MaxRiderWeight).IsRequired();
            b.Property(x => x.BikeWeight);

            b.Property(x => x.BrakeType)
                .HasMaxLength(80);

            b.Property(x => x.Year);

            b.Property(x => x.HourlyPrice)
                .IsRequired()
                .HasColumnType("decimal(10,2)");
        });

        modelBuilder.Entity<Bike>(b =>
        {
            b.ToTable("Bike");

            b.HasKey(x => x.Id);
            b.Property(x => x.Id).ValueGeneratedOnAdd();

            b.Property(x => x.SerialNumber)
                .IsRequired()
                .HasMaxLength(64);

            b.Property(x => x.Color)
                .HasMaxLength(50);

            b.Property(x => x.ModelId).IsRequired();

            b.HasOne(x => x.Model)
                .WithMany(x => x.Bikes)
                .HasForeignKey(x => x.ModelId)
                .OnDelete(DeleteBehavior.Restrict);

            b.HasIndex(x => x.ModelId);
        });

        modelBuilder.Entity<RentalRecord>(b =>
        {
            b.ToTable("RentalRecord", t =>
            {
                t.HasCheckConstraint("CK_RentalRecords_Duration", "[Duration] > '00:00:00'");
            });

            b.HasKey(x => x.Id);
            b.Property(x => x.Id).ValueGeneratedOnAdd();

            b.Property(x => x.RenterId).IsRequired();
            b.Property(x => x.BikeId).IsRequired();

            b.Property(x => x.StartTime)
                .IsRequired()
                .HasColumnType("datetime2");

            b.Property(x => x.Duration)
                .IsRequired()
                .HasColumnType("time");

            b.HasOne(x => x.Renter)
                .WithMany(x => x.RentalRecords)
                .HasForeignKey(x => x.RenterId)
                .OnDelete(DeleteBehavior.Restrict);

            b.HasOne(x => x.Bike)
                .WithMany(x => x.RentalRecords)
                .HasForeignKey(x => x.BikeId)
                .OnDelete(DeleteBehavior.Restrict);

            b.HasIndex(x => x.RenterId);
            b.HasIndex(x => x.BikeId);
            b.HasIndex(x => x.StartTime);
        });

        base.OnModelCreating(modelBuilder);
    }
}