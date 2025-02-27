using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PaSho_Tracker.Data;
using PaSho_Tracker.Model;

namespace PaSho_Tracker.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TaskController : BaseController
{
    public TaskController(AppDbContext context, ILogger<TaskController> logger) : base(logger, context)
    {
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        var tasks = await _context.Tasks.ToListAsync();
        _logger.LogInformation("Returning all tasks");
        return Ok(tasks);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get(int id)
    {
        var task = await _context.Tasks.FindAsync(id);
        if (task == null)
        {
            _logger.LogError($"Task with ID {id} not found");
            return NotFound();
        }

        _logger.LogInformation($"Returning task: {id}");
        return Ok(task);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] TaskModel model)
    {
        if (model == null || string.IsNullOrWhiteSpace(model.Title))
        {
            _logger.LogError("Task model is invalid");
            return BadRequest("Task title is required");
        }

        _context.Tasks.Add(model);
        await _context.SaveChangesAsync();
        _logger.LogInformation($"Returning HTTP 201 Created for task: {model.Id}");
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

        var task = await _context.Tasks.FindAsync(id);
        if (task == null)
        {
            _logger.LogError($"Task with ID {id} not found");
            return NotFound();
        }

        try
        {
            task.Title = model.Title ?? task.Title;
            task.Description = model.Description ?? task.Description;
            task.Priority = model.Priority;
            task.AssignedUserId = model.AssignedUserId ?? task.AssignedUserId;
            task.Deadline = model.Deadline != default ? model.Deadline : task.Deadline;
            task.Status = model.Status;

            await _context.SaveChangesAsync();
            _logger.LogInformation($"Updated task: {id}");
        }
        catch (Exception ex)
        {
            _logger.LogError($"Failed to update task: {ex.Message}");
            return HandleError(ex);
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> Delete(int id)
    {
        var task = await _context.Tasks.FindAsync(id);
        if (task == null)
        {
            _logger.LogError($"Task with ID {id} not found");
            return NotFound();
        }

        _context.Tasks.Remove(task);
        await _context.SaveChangesAsync();
        _logger.LogInformation($"Deleted task: {id}");
        return NoContent();
    }
}