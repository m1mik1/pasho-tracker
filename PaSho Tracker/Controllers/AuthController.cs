using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using PaSho_Tracker.DTO;
using PaSho_Tracker.Interface;
using PaSho_Tracker.Services;

namespace PaSho_Tracker.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : BaseController
{
    private readonly UserManager<IdentityUser> _userManager; 
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IAuthService _authService;

    public AuthController(
        ILogger<AuthController> logger,
        UserManager<IdentityUser> userManager,
        SignInManager<IdentityUser> signInManager,
        RoleManager<IdentityRole> roleManager,
        IAuthService authService
    ) : base(logger)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _roleManager = roleManager;
        _authService = authService;
    }

    // ✅ Registration endpoint
    [HttpPost("register")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> Register([FromBody] RegisterDto model)
    {
        try
        {
            var success= await _authService.Register(model);
            if (!success) return BadRequest();
            return Created($"/api/auth/register/{model.Email}", model);
        }
        catch (Exception e)
        {
            return HandleError(e);
        }
    }

    // ✅ Login endpoint
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto model)
    {
        var user = await _userManager.FindByEmailAsync(model.Email);

        if (user == null || !await _userManager.IsEmailConfirmedAsync(user))
        {
            _logger.LogWarning("Login failed: unconfirmed or nonexistent user");
            return Unauthorized("Invalid email or email not confirmed");
        }

        var result = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);

        if (!result.Succeeded)
        {
            _logger.LogWarning("Login failed: incorrect password");
            return Unauthorized("Incorrect password");
        }

        _logger.LogInformation("Login successful for user: {Email}", model.Email);
        return Ok("Login successful");
    }

    // ✅ Confirm email endpoint
    [HttpPost("confirm-email")]
    public async Task<IActionResult> ConfirmEmail([FromBody] ConfirmEmailDto model)
    {
        var user = await _userManager.FindByEmailAsync(model.Email);
        if (user == null)
        {
            _logger.LogWarning("User not found for email confirmation");
            return NotFound("User not found");
        }

        var result = await _userManager.ConfirmEmailAsync(user, model.Token);
        if (!result.Succeeded)
        {
            _logger.LogWarning("Email confirmation failed for {Email}", user.Email);
            return BadRequest("Invalid token or confirmation failed");
        }

        _logger.LogInformation("Email confirmed for user {Email}", model.Email);
        return Ok("Email confirmed successfully.");
    }
}
