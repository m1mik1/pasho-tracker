using Microsoft.AspNetCore.Mvc;
using PaSho_Tracker.Data;

namespace PaSho_Tracker.API.Controllers;

[ApiController]
public abstract class BaseController : ControllerBase
{
    protected readonly AppDbContext? _context;
    protected readonly ILogger _logger;

    protected BaseController(ILogger logger, AppDbContext? context = null)
    {
        _logger = logger;
        _context = context;
    }

    protected IActionResult HandleError(Exception ex)
    {
        _logger.LogError($"Error: {ex.Message}");
        return StatusCode(500, new { message = "Internal Server Error", details = ex.Message });
    }
}