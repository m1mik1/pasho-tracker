using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PaSho_Tracker.Data;
using PaSho_Tracker.Model;

namespace PaSho_Tracker.Controllers;

[Authorize(Roles = "Admin")]
[ApiController]
[Route("api/[controller]")]
public class TasksController : BaseController
{
    private readonly TaskRepository _taskRepository;

    public TasksController(ILogger<TasksController> logger, TaskRepository taskRepository)
        : base(logger)
    {
        _taskRepository = taskRepository;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        var tasks = await _taskRepository.GetAllAsync();
        _logger.LogInformation("Returning all tasks");
        return Ok(tasks);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get(int id)
    {
        var task = await _taskRepository.GetByIdAsync(id);
        if (task == null)
        {
            _logger.LogError($"Task with ID {id} not found");
            return NotFound($"Task with ID {id} not found");
        }

        _logger.LogInformation($"Returning task: {id}");
        return Ok(task);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Create([FromBody] TaskModel model)
    {
        if (model == null || string.IsNullOrWhiteSpace(model.Title))
        {
            _logger.LogError("Task model is invalid");
            return BadRequest("Task title is required");
        }

        if (await _taskRepository.ExistsAsync(model.Id))
        {
            _logger.LogError($"Task with ID {model.Id} already exists.");
            return Conflict($"Task with ID {model.Id} already exists.");
        }

        await _taskRepository.AddAsync(model);
        _logger.LogInformation($"Task created successfully: {model.Id}");
        return CreatedAtAction(nameof(Get), new { id = model.Id }, model);
    }


    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(int id, [FromBody] TaskModel model)
    {
        if (model == null || id != model.Id)
        {
            return BadRequest("Invalid task data");
        }

        if (!await _taskRepository.ExistsAsync(id))
        {
            _logger.LogError($"Task with ID {id} not found.");
            return NotFound($"Task with ID {id} not found.");
        }

        var task = await _taskRepository.GetByIdAsync(id);
        if (task == null)
        {
            _logger.LogError($"Task with ID {id} not found.");
            return NotFound($"Task with ID {id} not found.");
        }

        try
        {
            task.Title = model.Title ?? task.Title;
            task.Description = model.Description ?? task.Description;
            task.Priority = model.Priority;
            task.AssignedUserId = model.AssignedUserId ?? task.AssignedUserId;
            task.Deadline = DateTime.SpecifyKind(model.Deadline, DateTimeKind.Utc).ToUniversalTime();
            task.Status = model.Status;

            await _taskRepository.UpdateAsync(task);
            _logger.LogInformation($"Updated task: {id}");
        }
        catch (Exception ex)
        {
            var innerMessage = ex.InnerException?.Message ?? "No inner exception";
            _logger.LogError($"❌ Ошибка при обновлении задачи: {ex.Message}. Inner: {innerMessage}");
            return StatusCode(500, "Ошибка при обновлении задачи");
        }

        return NoContent();
    }


    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> Delete(int id)
    {
        var task = await _taskRepository.GetByIdAsync(id);
        if (task == null)
        {
            _logger.LogError($"Task with ID {id} not found");
            return NotFound($"Task with ID {id} not found");
        }

        await _taskRepository.DeleteAsync(task);
        _logger.LogInformation($"Task deleted successfully: {id}");
        return NoContent();
    }
    
    [HttpGet("sorted-by-deadline")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetSortedByDeadline()
    {
        var tasks = await _taskRepository.GetSortedByDeadlineAsync();
        _logger.LogInformation("Returning tasks sorted by deadline");
        return Ok(tasks);
    }

}
