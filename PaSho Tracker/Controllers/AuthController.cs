using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using PaSho_Tracker.DTO;
using PaSho_Tracker.Interface;
using PaSho_Tracker.Services;

namespace PaSho_Tracker.Controllers;
[Authorize(Roles = "Admin")]
[ApiController]
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
    [AllowAnonymous]
    [HttpPost("register")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> Register([FromBody] RegisterDto model)
    {
        try
        {
            var success= await _authService.Register(model);
            if (!success) return BadRequest();
            return Ok("Registration successful. Please confirm your email.");
        }
        catch (Exception e)
        {
            return HandleError(e);
        }
    }
    
    [AllowAnonymous]
    [HttpPost("login")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Login([FromBody] LoginDto model)
    {
        try
        {
             var token= await _authService.Login(model);
             if (string.IsNullOrEmpty(token)) return Unauthorized("Invalid email or password");
             return Ok(new JwtResponseDto{ Token = token });
        }
        catch (Exception e)
        {
            return HandleError(e);
        }
    }
    
    [HttpPost("confirm-email")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> ConfirmEmail([FromBody] ConfirmEmailDto model)
    {
        try
        {
            var success = await _authService.ConfirmEmail(model.Email, model.Token);
            if (!success) return BadRequest();
            return Ok();
        }
        catch (Exception e)
        {
            return HandleError(e);
        }
    }
    
    [HttpPost("password/reset")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto model)
    {
        try
        {
            var success = await _authService.ResetPassword(model.Email, model.Token, model.NewPassword);
            if (!success) return BadRequest();
            return Ok();
        }
        catch (Exception e)
        {
            return HandleError(e);
        }
    }

    [HttpPost("password/forgot")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordDto model)
    {
        try
        {
            var success = await _authService.ForgotPassword(model.Email);
            if (!success) return BadRequest();
            return Ok();
        }
        catch (Exception e)
        {
            return HandleError(e);
        }
    }
}
