using BikeRental.Application.Contracts.Analytics;
using BikeRental.Application.Contracts;
using BikeRental.Application.Contracts.Bikes;
using Microsoft.AspNetCore.Mvc;

namespace BikeRental.Api.Host.Controllers;

/// <summary>
/// Контроллер для выполнения аналитических запросов
/// </summary>
/// <param name="analyticsService">Служба приложения для выполнения аналитических запросов</param>
/// <param name="logger">Логгер контроллера</param>
[Route("api/[controller]")]
[ApiController]
public class AnalyticsController(IAnalyticsService analyticsService, ILogger<AnalyticsController> logger) : ControllerBase
{
    /// <summary>
    /// Возвращает информацию обо всех спортивных велосипедах
    /// </summary>
    /// <returns>Список велосипедов</returns>
    [HttpGet("sport-bikes")]
    [ProducesResponseType(typeof(IList<BikeDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IList<BikeDto>>> GetAllSportBikes()
    {
        logger.LogInformation("Метод {Method} вызван в {Controller}", nameof(GetAllSportBikes), GetType().Name);

        try
        {
            var result = await analyticsService.GetAllSportBikes();
            logger.LogInformation("Метод {Method} завершился успешно", nameof(GetAllSportBikes));
            return Ok(result);
        }
        catch (KeyNotFoundException ex)
        {
            logger.LogWarning(ex, "Не найден ресурс в методе {Method}", nameof(GetAllSportBikes));
            return NotFound(ex.Message);
        }
        catch (ArgumentException ex)
        {
            logger.LogWarning(ex, "Некорректные параметры в методе {Method}", nameof(GetAllSportBikes));
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Исключение в методе {Method}", nameof(GetAllSportBikes));
            return StatusCode(StatusCodes.Status500InternalServerError, $"{ex.Message}\n\r{ex.InnerException?.Message}");
        }
    }

    /// <summary>
    /// Возвращает топ 5 моделей велосипедов по прибыли от аренды
    /// </summary>
    /// <returns>Список моделей с прибылью</returns>
    [HttpGet("top-models/profit")]
    [ProducesResponseType(typeof(IList<BikeModelProfitDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IList<BikeModelProfitDto>>> GetTop5ModelsByProfit()
    {
        logger.LogInformation("Метод {Method} вызван в {Controller}", nameof(GetTop5ModelsByProfit), GetType().Name);

        try
        {
            var result = await analyticsService.GetTop5ModelsByProfit();
            logger.LogInformation("Метод {Method} завершился успешно", nameof(GetTop5ModelsByProfit));
            return Ok(result);
        }
        catch (KeyNotFoundException ex)
        {
            logger.LogWarning(ex, "Не найден ресурс в методе {Method}", nameof(GetTop5ModelsByProfit));
            return NotFound(ex.Message);
        }
        catch (ArgumentException ex)
        {
            logger.LogWarning(ex, "Некорректные параметры в методе {Method}", nameof(GetTop5ModelsByProfit));
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Исключение в методе {Method}", nameof(GetTop5ModelsByProfit));
            return StatusCode(StatusCodes.Status500InternalServerError, $"{ex.Message}\n\r{ex.InnerException?.Message}");
        }
    }

    /// <summary>
    /// Возвращает топ 5 моделей велосипедов по суммарной длительности аренды
    /// </summary>
    /// <returns>Список моделей с суммарным временем аренды</returns>
    [HttpGet("top-models/duration")]
    [ProducesResponseType(typeof(IList<BikeModelTotalDurationDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IList<BikeModelTotalDurationDto>>> GetTop5ModelsByRentalDuration()
    {
        logger.LogInformation("Метод {Method} вызван в {Controller}", nameof(GetTop5ModelsByRentalDuration), GetType().Name);

        try
        {
            var result = await analyticsService.GetTop5ModelsByRentalDuration();
            logger.LogInformation("Метод {Method} завершился успешно", nameof(GetTop5ModelsByRentalDuration));
            return Ok(result);
        }
        catch (KeyNotFoundException ex)
        {
            logger.LogWarning(ex, "Не найден ресурс в методе {Method}", nameof(GetTop5ModelsByRentalDuration));
            return NotFound(ex.Message);
        }
        catch (ArgumentException ex)
        {
            logger.LogWarning(ex, "Некорректные параметры в методе {Method}", nameof(GetTop5ModelsByRentalDuration));
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Исключение в методе {Method}", nameof(GetTop5ModelsByRentalDuration));
            return StatusCode(StatusCodes.Status500InternalServerError, $"{ex.Message}\n\r{ex.InnerException?.Message}");
        }
    }

    /// <summary>
    /// Возвращает минимальное максимальное и среднее время аренды велосипедов
    /// </summary>
    /// <returns>Статистика по длительности аренды</returns>
    [HttpGet("rental-time/stats")]
    [ProducesResponseType(typeof(RentalDurationStatsDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<RentalDurationStatsDto>> GetMinMaxAvgRentalTime()
    {
        logger.LogInformation("Метод {Method} вызван в {Controller}", nameof(GetMinMaxAvgRentalTime), GetType().Name);

        try
        {
            var result = await analyticsService.GetMinMaxAvgRentalTime();
            logger.LogInformation("Метод {Method} завершился успешно", nameof(GetMinMaxAvgRentalTime));
            return Ok(result);
        }
        catch (KeyNotFoundException ex)
        {
            logger.LogWarning(ex, "Не найден ресурс в методе {Method}", nameof(GetMinMaxAvgRentalTime));
            return NotFound(ex.Message);
        }
        catch (ArgumentException ex)
        {
            logger.LogWarning(ex, "Некорректные параметры в методе {Method}", nameof(GetMinMaxAvgRentalTime));
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Исключение в методе {Method}", nameof(GetMinMaxAvgRentalTime));
            return StatusCode(StatusCodes.Status500InternalServerError, $"{ex.Message}\n\r{ex.InnerException?.Message}");
        }
    }

    /// <summary>
    /// Возвращает суммарное время аренды велосипедов каждого типа
    /// </summary>
    /// <returns>Список типов с суммарным временем аренды</returns>
    [HttpGet("rental-time/by-type")]
    [ProducesResponseType(typeof(IList<BikeTypeTotalDurationDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IList<BikeTypeTotalDurationDto>>> GetSumRentalTimeByBikeType()
    {
        logger.LogInformation("Метод {Method} вызван в {Controller}", nameof(GetSumRentalTimeByBikeType), GetType().Name);

        try
        {
            var result = await analyticsService.GetSumRentalTimeByBikeType();
            logger.LogInformation("Метод {Method} завершился успешно", nameof(GetSumRentalTimeByBikeType));
            return Ok(result);
        }
        catch (ArgumentException ex)
        {
            logger.LogWarning(ex, "Некорректные параметры в методе {Method}", nameof(GetSumRentalTimeByBikeType));
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Исключение в методе {Method}", nameof(GetSumRentalTimeByBikeType));
            return StatusCode(StatusCodes.Status500InternalServerError, $"{ex.Message}\n\r{ex.InnerException?.Message}");
        }
    }

    /// <summary>
    /// Возвращает клиентов бравших велосипеды на прокат больше всего раз
    /// </summary>
    /// <returns>Список клиентов с максимальным количеством аренд</returns>
    [HttpGet("renters/top-by-rent-count")]
    [ProducesResponseType(typeof(IList<RenterTopByRentCountDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IList<RenterTopByRentCountDto>>> GetTopRentersByRentCount()
    {
        logger.LogInformation("Метод {Method} вызван в {Controller}", nameof(GetTopRentersByRentCount), GetType().Name);

        try
        {
            var result = await analyticsService.GetTopRentersByRentCount();
            logger.LogInformation("Метод {Method} завершился успешно", nameof(GetTopRentersByRentCount));
            return Ok(result);
        }
        catch (KeyNotFoundException ex)
        {
            logger.LogWarning(ex, "Не найден ресурс в методе {Method}", nameof(GetTopRentersByRentCount));
            return NotFound(ex.Message);
        }
        catch (ArgumentException ex)
        {
            logger.LogWarning(ex, "Некорректные параметры в методе {Method}", nameof(GetTopRentersByRentCount));
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Исключение в методе {Method}", nameof(GetTopRentersByRentCount));
            return StatusCode(StatusCodes.Status500InternalServerError, $"{ex.Message}\n\r{ex.InnerException?.Message}");
        }
    }
}