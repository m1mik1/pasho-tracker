using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PaSho_Tracker.Data;
using PaSho_Tracker.Model;

namespace PaSho_Tracker.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoryController : BaseController
{
    public CategoryController(ILogger<CategoryController> logger, AppDbContext context ) : base(logger, context)
    {
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var categories = await _context.Categories.ToListAsync();
        _logger.LogInformation("Returning all categories");
        return Ok( categories );
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get(int id)
    {
        var category = await _context.Categories.FindAsync(id);
        if (category == null)
        {
            _logger.LogInformation($"Category with {id} not found");
            return NotFound();
        }
        _logger.LogInformation($"Returning category: {id}");
        return Ok(category);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Create([FromBody] CategoryModel model)
    {
        if (string.IsNullOrWhiteSpace(model.CategoryName))
        {
            _logger.LogError("Category name is required");
            return BadRequest("Category name is required");
        }
        var existingCategory = await _context.Categories
            .FirstOrDefaultAsync(c => c.CategoryName == model.CategoryName);
        if (existingCategory != null)
        {
            return Conflict($"Category with name '{model.CategoryName}' already exists.");
        }
        try
        {
            _context.Categories.Add(model);
            await _context.SaveChangesAsync();
            _logger.LogInformation($"Returning HTTP 201 Created for category: {model.Id}");
            return CreatedAtAction(nameof(Get), new { id = model.Id }, model);
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
    public async Task<IActionResult> Update(int id, [FromBody] CategoryModel model)
    {
        if (id != model.Id)
        {
            return BadRequest("ID в URL не совпадает с ID в объекте.");
        }
        var category = await _context.Categories.FindAsync(id);
        if (category == null)
        {
            _logger.LogInformation($"Category with {id} not found");
            return NotFound();
        }

        try
        {
            category.CategoryName = model.CategoryName;
            await _context.SaveChangesAsync();
            _logger.LogInformation($"Returning HTTP 204 Updated for category: {category.Id}");
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
        var category = await _context.Categories.FindAsync(id);
        if (category == null)
        {
            _logger.LogInformation($"Category with {id} not found");
            return NotFound();
        }

        try
        {
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
            _logger.LogInformation($"Returning HTTP 204 Deleted for category: {category.Id}");
            return NoContent();
        }
        catch (Exception ex)
        {
            return HandleError(ex);
        }
    }
}