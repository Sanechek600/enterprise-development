using BikeRental.Application.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace BikeRental.Api.Host.Controllers;

/// <summary>
/// Базовый CRUD контроллер для стандартных операций над DTO
/// </summary>
/// <typeparam name="TDto">DTO для операций чтения</typeparam>
/// <typeparam name="TCreateUpdateDto">DTO для операций создания и обновления</typeparam>
/// <typeparam name="TKey">Тип идентификатора</typeparam>
/// <param name="appService">Служба приложения для CRUD операций</param>
/// <param name="logger">Логгер контроллера</param>
[Route("api/[controller]")]
[ApiController]
public abstract class CrudControllerBase<TDto, TCreateUpdateDto, TKey>(
    IApplicationService<TDto, TCreateUpdateDto, TKey> appService,
    ILogger<CrudControllerBase<TDto, TCreateUpdateDto, TKey>> logger) : ControllerBase
    where TDto : class
    where TCreateUpdateDto : class
    where TKey : struct
{
    /// <summary>
    /// Создаёт новый объект
    /// </summary>
    /// <param name="newDto">DTO для создания</param>
    /// <returns>Созданный объект</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<TDto>> Create([FromBody] TCreateUpdateDto newDto)
    {
        return await ExecuteWithLogging(async () =>
        {
            var result = await appService.Create(newDto);
            return CreatedAtAction(nameof(Create), result);
        }, nameof(Create), newDto);
    }

    /// <summary>
    /// Обновляет объект по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор объекта</param>
    /// <param name="newDto">DTO для обновления</param>
    /// <returns>Обновлённый объект</returns>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<TDto>> Edit([FromRoute] TKey id, [FromBody] TCreateUpdateDto newDto)
    {
        return await ExecuteWithLogging(async () =>
        {
            var result = await appService.Update(newDto, id);
            return Ok(result);
        }, nameof(Edit), id, newDto);
    }

    /// <summary>
    /// Удаляет объект по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор объекта</param>
    /// <returns>Результат удаления</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Delete([FromRoute] TKey id)
    {
        return await ExecuteWithLogging(async () =>
        {
            var result = await appService.Delete(id);
            return result ? Ok(true) : NoContent();
        }, nameof(Delete), id);
    }

    /// <summary>
    /// Возвращает список объектов
    /// </summary>
    /// <returns>Список объектов</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IList<TDto>>> GetAll()
    {
        return await ExecuteWithLogging(async () =>
        {
            var result = await appService.GetAll();
            return result.Count == 0 ? NoContent() : Ok(result);
        }, nameof(GetAll));
    }

    /// <summary>
    /// Возвращает объект по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор объекта</param>
    /// <returns>Объект</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<TDto>> Get([FromRoute] TKey id)
    {
        return await ExecuteWithLogging(async () =>
        {
            var result = await appService.Get(id);
            return result is null ? NoContent() : Ok(result);
        }, nameof(Get), id);
    }

    /// <summary>
    /// Выполняет операцию с логированием и переводом ошибок в ответы API
    /// </summary>
    /// <param name="operation">Операция контроллера</param>
    /// <param name="methodName">Название метода</param>
    /// <param name="parameters">Параметры вызова</param>
    /// <returns>Результат выполнения</returns>
    private async Task<ActionResult> ExecuteWithLogging(
        Func<Task<ActionResult>> operation,
        string methodName,
        params object[] parameters)
    {
        logger.LogInformation("Метод {Method} вызван в {Controller} с параметрами: {@Params}",
            methodName, GetType().Name, parameters);

        try
        {
            var result = await operation();
            logger.LogInformation("Метод {Method} завершился успешно", methodName);
            return result;
        }
        catch (KeyNotFoundException ex)
        {
            logger.LogWarning(ex, "Не найден ресурс в методе {Method}", methodName);
            return NotFound(ex.Message);
        }
        catch (ArgumentException ex)
        {
            logger.LogWarning(ex, "Некорректные параметры в методе {Method}", methodName);
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Исключение в методе {Method}", methodName);
            return StatusCode(StatusCodes.Status500InternalServerError, $"{ex.Message}\n\r{ex.InnerException?.Message}");
        }
    }
}