using BikeRental.Application.Contracts;
using BikeRental.Application.Contracts.BikeModels;

namespace BikeRental.Api.Host.Controllers;

/// <summary>
/// Контроллер для работы с моделями велосипедов
/// </summary>
/// <param name="appService">Служба приложения для CRUD операций</param>
/// <param name="logger">Логгер контроллера</param>
public class BikeModelController(
    IApplicationService<BikeModelDto, BikeModelCreateUpdateDto, int> appService,
    ILogger<BikeModelController> logger)
    : CrudControllerBase<BikeModelDto, BikeModelCreateUpdateDto, int>(appService, logger);