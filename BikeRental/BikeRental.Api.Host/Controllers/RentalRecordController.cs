using BikeRental.Application.Contracts.RentalRecords;
using Microsoft.AspNetCore.Mvc;

namespace BikeRental.Api.Host.Controllers;

/// <summary>
/// Контроллер для работы с записями аренды
/// </summary>
/// <param name="rentalRecordService">Служба приложения для работы с записями аренды</param>
/// <param name="logger">Логгер контроллера</param>
[Route("api/[controller]")]
[ApiController]
[Produces("application/json")]
public class RentalRecordController(
    IRentalRecordService rentalRecordService,
    ILogger<RentalRecordController> logger)
    : CrudControllerBase<RentalRecordDto, RentalRecordCreateUpdateDto, int>(rentalRecordService, logger)
{
    /// <summary>
    /// Возвращает список записей аренды арендатора
    /// </summary>
    /// <param name="renterId">Идентификатор арендатора</param>
    /// <returns>Список DTO записей аренды</returns>
    [HttpGet("by-renter/{renterId:int}")]
    [ProducesResponseType(typeof(IList<RentalRecordDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IList<RentalRecordDto>>> GetByRenterId([FromRoute] int renterId)
    {
        logger.LogInformation("Метод {Method} вызван в {Controller} с параметрами: {@Params}",
            nameof(GetByRenterId), GetType().Name, new object[] { renterId });

        try
        {
            var result = await rentalRecordService.GetRentalRecordsByRenterId(renterId);

            logger.LogInformation("Метод {Method} завершился успешно", nameof(GetByRenterId));
            return Ok(result);
        }
        catch (KeyNotFoundException ex)
        {
            logger.LogWarning(ex, "Не найден ресурс в методе {Method}", nameof(GetByRenterId));
            return NotFound(ex.Message);
        }
        catch (ArgumentException ex)
        {
            logger.LogWarning(ex, "Некорректные параметры в методе {Method}", nameof(GetByRenterId));
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Исключение в методе {Method}", nameof(GetByRenterId));
            return StatusCode(StatusCodes.Status500InternalServerError, $"{ex.Message}\n\r{ex.InnerException?.Message}");
        }
    }

    /// <summary>
    /// Возвращает список записей аренды велосипеда
    /// </summary>
    /// <param name="bikeId">Идентификатор велосипеда</param>
    /// <returns>Список DTO записей аренды</returns>
    [HttpGet("by-bike/{bikeId:int}")]
    [ProducesResponseType(typeof(IList<RentalRecordDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IList<RentalRecordDto>>> GetByBikeId([FromRoute] int bikeId)
    {
        logger.LogInformation("Метод {Method} вызван в {Controller} с параметрами: {@Params}",
            nameof(GetByBikeId), GetType().Name, new object[] { bikeId });

        try
        {
            var result = await rentalRecordService.GetRentalRecordsByBikeId(bikeId);

            logger.LogInformation("Метод {Method} завершился успешно", nameof(GetByBikeId));
            return Ok(result);
        }
        catch (KeyNotFoundException ex)
        {
            logger.LogWarning(ex, "Не найден ресурс в методе {Method}", nameof(GetByBikeId));
            return NotFound(ex.Message);
        }
        catch (ArgumentException ex)
        {
            logger.LogWarning(ex, "Некорректные параметры в методе {Method}", nameof(GetByBikeId));
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Исключение в методе {Method}", nameof(GetByBikeId));
            return StatusCode(StatusCodes.Status500InternalServerError, $"{ex.Message}\n\r{ex.InnerException?.Message}");
        }
    }
}