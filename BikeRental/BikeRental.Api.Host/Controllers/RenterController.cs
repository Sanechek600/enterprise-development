using BikeRental.Application.Contracts;
using BikeRental.Application.Contracts.Renters;

namespace BikeRental.Api.Host.Controllers;

/// <summary>
/// Контроллер для работы с арендаторами
/// </summary>
/// <param name="appService">Служба приложения для CRUD операций</param>
/// <param name="logger">Логгер контроллера</param>
public class RenterController(
    IApplicationService<RenterDto, RenterCreateUpdateDto, int> appService,
    ILogger<RenterController> logger)
    : CrudControllerBase<RenterDto, RenterCreateUpdateDto, int>(appService, logger);