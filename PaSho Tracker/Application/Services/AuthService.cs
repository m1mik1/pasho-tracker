using System.Net;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Identity;
using PaSho_Tracker.DTO;
using PaSho_Tracker.Interface;


namespace PaSho_Tracker.Services;

public class AuthService : IAuthService
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly ILogger<AuthService> _logger;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IEmailHelper _emailHelper;
    private readonly JwtService _jwtService;

    private readonly IConfiguration _config;

    public AuthService(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager,
        ILogger<AuthService> logger, RoleManager<IdentityRole> roleManager, IEmailHelper emailHelper,
        IConfiguration config
        , JwtService jwtService)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _logger = logger;
        _roleManager = roleManager;
        _emailHelper = emailHelper;
        _config = config;
        _jwtService = jwtService;
    }

    public async Task<string?> Login(LoginDto model)
    {
        var user = await _userManager.FindByEmailAsync(model.Email);
        if (user == null)
        {
            _logger.LogWarning("Invalid email address");
            return null;
        }

        if (!await _userManager.IsEmailConfirmedAsync(user))
        {
            _logger.LogInformation("Email: {email} is not confirmed.", user.Email);
            return null;
        }


        var result = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);
        if (!result.Succeeded)
        {
            _logger.LogWarning("Wrong password.");
            return null;
        }

        try
        {
            var roles = await _userManager.GetRolesAsync(user);
            var token = _jwtService.GenerateToken(user.Id, user.Email, roles);
            _logger.LogInformation("User logged in.");
            return token;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return null;
        }
    }

    public async Task<bool> Register(RegisterDto model)
    {
        if (await _userManager.FindByEmailAsync(model.Email) != null)
        {
            _logger.LogWarning("Email address already exists");
            return false;
        }

        if (await _userManager.FindByNameAsync(model.Username) != null)
        {
            _logger.LogWarning("Username already exists");
            return false;
        }

        if (model.Password != model.ConfirmPassword)
        {
            _logger.LogWarning("Passwords do not match");
            return false;
        }

        try
        {
            var user = new IdentityUser
            {
                UserName = model.Username,
                Email = model.Email,
            };
            var result = await _userManager.CreateAsync(user, model.Password);

            if (!await _roleManager.RoleExistsAsync("User"))
                await _roleManager.CreateAsync(new IdentityRole("User"));
            if (!await _roleManager.RoleExistsAsync("Admin"))
                await _roleManager.CreateAsync(new IdentityRole("Admin"));

            await _userManager.AddToRoleAsync(user, "User");
            _logger.LogInformation("User with Email: {email} has been registered", user.Email);

            if (user.Email == "pastushenko.rostislav@lll.kpi.ua")
            {
                await _userManager.AddToRoleAsync(user, "Admin");
                _logger.LogInformation("User with Email: {email} assigned Admin role", user.Email);
            }

            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            if (string.IsNullOrEmpty(token))
            {
                _logger.LogWarning("Invalid token generated");
            }

            var encodedToken = WebUtility.UrlEncode(token);
            await _emailHelper.SendConfirmationEmail(user.Email, encodedToken);
            _logger.LogInformation("Confirmation email sent to: {email}", user.Email);
            
            return result.Succeeded;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Registration failed");
            return false;
        }
    }
}