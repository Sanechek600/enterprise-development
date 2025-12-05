using BikeRental.Application.Contracts.BikeModels;
using BikeRental.Application.Contracts.Bikes;
using Microsoft.AspNetCore.Mvc;

namespace BikeRental.Api.Host.Controllers;

/// <summary>
/// Контроллер для работы с велосипедами
/// </summary>
/// <param name="bikeService">Служба приложения для работы с велосипедами</param>
/// <param name="logger">Логгер контроллера</param>
public class BikeController(
    IBikeService bikeService,
    ILogger<BikeController> logger)
    : CrudControllerBase<BikeDto, BikeCreateUpdateDto, int>(bikeService, logger)
{
    /// <summary>
    /// Возвращает модель велосипеда по идентификатору велосипеда
    /// </summary>
    /// <param name="id">Идентификатор велосипеда</param>
    /// <returns>DTO модели велосипеда</returns>
    [HttpGet("{id}/model")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<BikeModelDto>> GetModel([FromRoute] int id)
    {
        logger.LogInformation("Метод {Method} вызван в {Controller} с параметрами: {@Params}",
            nameof(GetModel), GetType().Name, new object[] { id });

        try
        {
            var result = await bikeService.GetBikeModel(id);

            logger.LogInformation("Метод {Method} завершился успешно", nameof(GetModel));
            return Ok(result);
        }
        catch (KeyNotFoundException ex)
        {
            logger.LogWarning(ex, "Не найден ресурс в методе {Method}", nameof(GetModel));
            return NotFound(ex.Message);
        }
        catch (ArgumentException ex)
        {
            logger.LogWarning(ex, "Некорректные параметры в методе {Method}", nameof(GetModel));
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Исключение в методе {Method}", nameof(GetModel));
            return StatusCode(StatusCodes.Status500InternalServerError, $"{ex.Message}\n\r{ex.InnerException?.Message}");
        }
    }
}