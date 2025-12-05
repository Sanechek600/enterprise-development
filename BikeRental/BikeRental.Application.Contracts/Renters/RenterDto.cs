namespace BikeRental.Application.Contracts.Renters;

/// <summary>
/// DTO для получения арендатора
/// </summary>
/// <param name="Id">Уникальный идентификатор арендатора</param>
/// <param name="FullName">ФИО арендатора</param>
/// <param name="Phone">Телефон арендатора</param>
public sealed record RenterDto(
    int Id,
    string FullName,
    string Phone
);