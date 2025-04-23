using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PaSho_Tracker.DTO;
using PaSho_Tracker.Interface;

namespace PaSho_Tracker.API.Controllers;

[Authorize(Roles = "Admin")]
[ApiController]
[Produces("application/json")]
[ProducesResponseType(StatusCodes.Status500InternalServerError)]
[Route("api/[controller]")]
public class TasksController : BaseController
{
    private readonly ITaskService _taskService;

    public TasksController(ILogger<TasksController> logger, ITaskService taskService)
        : base(logger)
    {
        _taskService = taskService;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            var success = await _taskService.GetAll();
            return Ok(success);
        }
        catch (Exception ex)
        {
            return HandleError(ex);
        }
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get(int id)
    {
        try
        {
            var result = await _taskService.Get(id);
            if (result == null) return NotFound();
            return Ok(result);
        }
        catch (Exception ex)
        {
            return HandleError(ex);
        }
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Create([FromBody] CreateTaskDto model)
    {
        try
        {
            var result = await _taskService.Create(model);
            if (result == null) return BadRequest();
            return CreatedAtAction(nameof(Get), new { id = result.Id }, result);
        }
        catch (Exception ex)
        {
            return HandleError(ex);
        }
    }


    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateTaskDto model)
    {
        try
        {
           if (id != model.Id) return BadRequest();
           
           var success = await _taskService.Update(model);
           if (!success) return NotFound();
           return NoContent();
        }
        catch (Exception ex)
        {
            return HandleError(ex);
        }
    }


    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var success = await _taskService.Delete(id);
            if (!success) return NotFound();
            return NoContent();
        }
        catch (Exception ex)
        {
            return HandleError(ex);
        }
    }
    
    [HttpGet("sorted-by-deadline")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetSortedByDeadline()
    {
        try
        {
            var success = await _taskService.GetSortedByDeadline();
            return Ok(success);
        }
        catch (Exception ex)
        {
            return HandleError(ex);
        }
    }

}
