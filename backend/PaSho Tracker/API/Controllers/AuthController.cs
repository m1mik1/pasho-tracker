using Microsoft.AspNetCore.Mvc;
using PaSho_Tracker.DTO;
using PaSho_Tracker.Interface;

namespace PaSho_Tracker.API.Controllers;

[ApiController]
[Produces("application/json")]
[ProducesResponseType(StatusCodes.Status500InternalServerError)]
[Route("api/[controller]")]
public class AuthController : BaseController
{
    private readonly IAuthService _authService;

    public AuthController(
        ILogger<AuthController> logger,
        IAuthService authService
    ) : base(logger)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> Register([FromBody] RegisterDto model)
    {
        try
        {
            var success = await _authService.Register(model);
            if (!success) return BadRequest();
            return StatusCode(StatusCodes.Status201Created, "Registration successful. Please confirm your email.");

        }
        catch (Exception e)
        {
            return HandleError(e);
        }
    }


    [HttpPost("login")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Login([FromBody] LoginDto model)
    {
        try
        {
            var token = await _authService.Login(model);
            if (string.IsNullOrEmpty(token)) return Unauthorized("Invalid email or password");
            return Ok(new JwtResponseDto { Token = token });
        }
        catch (Exception e)
        {
            return HandleError(e);
        }
    }
}