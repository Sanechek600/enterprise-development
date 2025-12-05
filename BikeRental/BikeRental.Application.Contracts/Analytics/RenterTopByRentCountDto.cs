using BikeRental.Application.Contracts.Renters;

namespace BikeRental.Application.Contracts.Analytics;

/// <summary>
/// DTO для выдачи клиентов с максимальным количеством аренд
/// </summary>
/// <param name="Renter">Клиент</param>
/// <param name="RentCount">Количество аренд</param>
public sealed record RenterTopByRentCountDto(
    RenterDto Renter,
    int RentCount
);