namespace BikeRental.Application.Contracts.Renters;

/// <summary>
/// DTO для создания и обновления арендатора
/// </summary>
/// <param name="FullName">ФИО арендатора</param>
/// <param name="Phone">Телефон арендатора</param>
public sealed record RenterCreateUpdateDto(
    string FullName,
    string Phone
);