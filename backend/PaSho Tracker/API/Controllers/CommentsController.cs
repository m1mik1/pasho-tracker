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
public class CommentsController : BaseController
{
    private readonly ICommentService _commentService;

    public CommentsController(ILogger<CommentsController> logger, ICommentService commentService) : base(logger)
    {
        _commentService = commentService;
    }
    
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            var success = await _commentService.GetAll();
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
            var result = await _commentService.Get(id);
            if (result == null) return NotFound();
            return Ok(result);
        }
        catch (Exception ex)
        {
            return HandleError(ex);
        }
    }

    
    [HttpGet("by-task/{taskId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetByTaskId(int taskId)
    {
        try
        {
            var result = await _commentService.GetByTaskId(taskId);
            if (!result.Any()) return NotFound();
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
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Create([FromBody] CreateCommentDto comment)
    {
        try
        {
           var result = await _commentService.Create(comment);
           if (result==null) return NotFound();
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
    public async Task<IActionResult> Update(int id, [FromBody] UpdateCommentDto updatedComment)
    {

        try
        {
            if (id != updatedComment.Id) return BadRequest();

            var success = await _commentService.Update(updatedComment);
            if (!success) return NotFound();
            return NoContent();
        }
        catch (Exception ex)
        {
            return HandleError(ex);
        }
    }
    
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    { 
        try
        {
            var success = await _commentService.Delete(id);
            if (!success) return NotFound();
            return NoContent();
        }
        catch (Exception ex)
        {
            return HandleError(ex);
        }
    }
}
