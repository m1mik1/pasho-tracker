using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PaSho_Tracker.Data;
using PaSho_Tracker.Model;

namespace PaSho_Tracker.Controllers;

[Authorize(Roles = "Admin")]
[ApiController]
[Route("api/[controller]")]
public class CommentsController : BaseController
{
    private readonly CommentRepository _commentRepository;

    public CommentsController(ILogger<CommentsController> logger, CommentRepository commentRepository)
        : base(logger)
    {
        _commentRepository = commentRepository;
    }

    // Получить все комментарии
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        var comments = await _commentRepository.GetAllAsync();
        _logger.LogInformation("Returning all comments");
        return Ok(comments);
    }

    // Получить комментарий по ID
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get(int id)
    {
        var comment = await _commentRepository.GetByIdAsync(id);
        if (comment == null)
        {
            _logger.LogInformation($"Comment with ID {id} not found");
            return NotFound($"Comment with ID {id} not found");
        }

        _logger.LogInformation($"Returning comment: {id}");
        return Ok(comment);
    }

    // Получить комментарии по TaskId
    [HttpGet("by-task/{taskId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetByTaskId(int taskId)
    {
        var comments = await _commentRepository.GetByTaskIdAsync(taskId);
        _logger.LogInformation($"Returning comments for task {taskId}");
        return Ok(comments);
    }

    // Создать комментарий
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CommentModel comment)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        if (string.IsNullOrWhiteSpace(comment.CommentText))
        {
            _logger.LogError("Comment text is required");
            return BadRequest("Comment text is required");
        }

        var relatedTask = await _commentRepository.GetRelatedTaskAsync(comment.RelatedTaskId);
        if (relatedTask == null)
        {
            _logger.LogError($"Task with ID {comment.RelatedTaskId} not found.");
            return BadRequest($"Task with ID {comment.RelatedTaskId} not found.");
        }

        comment.RelatedTask = relatedTask;

        try
        {
            await _commentRepository.AddAsync(comment);
            _logger.LogInformation($"Comment created successfully: {comment.Id}");
            return CreatedAtAction(nameof(Get), new { id = comment.Id }, comment);
        }
        catch (Exception ex)
        {
            return HandleError(ex);
        }
    }



    // Обновить комментарий
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(int id, [FromBody] CommentModel updatedComment)
    {
        if (id != updatedComment.Id)
        {
            _logger.LogError("ID in URL does not match ID in object.");
            return BadRequest("ID in URL does not match ID in object.");
        }

        var existingComment = await _commentRepository.GetByIdAsync(id);
        if (existingComment == null)
        {
            _logger.LogInformation($"Comment with ID {id} not found.");
            return NotFound($"Comment with ID {id} not found.");
        }

        try
        {
            existingComment.CommentText = updatedComment.CommentText;
            existingComment.Author = updatedComment.Author;
            existingComment.RelatedTaskId = updatedComment.RelatedTaskId;

            await _commentRepository.UpdateAsync(existingComment);
            _logger.LogInformation($"Comment updated successfully: {id}");
            return NoContent();
        }
        catch (Exception ex)
        {
            return HandleError(ex);
        }
    }

    // Удалить комментарий
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var comment = await _commentRepository.GetByIdAsync(id);
        if (comment == null)
        {
            _logger.LogInformation($"Comment with ID {id} not found.");
            return NotFound($"Comment with ID {id} not found.");
        }

        try
        {
            await _commentRepository.DeleteAsync(comment);
            _logger.LogInformation($"Comment deleted successfully: {id}");
            return NoContent();
        }
        catch (Exception ex)
        {
            return HandleError(ex);
        }
    }
}
