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

    private readonly IConfiguration _config;

    public AuthService(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager,
        ILogger<AuthService> logger , RoleManager<IdentityRole> roleManager, IEmailHelper emailHelper, IConfiguration config)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _logger = logger;
        _roleManager = roleManager;
        _emailHelper = emailHelper;
        _config = config;
    }

    public async Task<string?> Login(LoginDto model)
    {
        try
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                _logger.LogWarning("Invalid email address");
                return null;
            }
            if (await _userManager.IsEmailConfirmedAsync(user))
            {
                var result = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);
                if (!result.Succeeded)
                {
                    _logger.LogWarning("Wrong password.");
                    return null;
                }
                _logger.LogInformation("User logged in.");
                return "logged in";
            }
            _logger.LogInformation("Email: {email} is not confirmed." , user.Email);
            return null;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return null;
        }
    }

    public async Task<bool> Register(RegisterDto model)
    {
        try
        {
            if (await _userManager.FindByEmailAsync(model.Email) != null)
            {
                _logger.LogWarning("Email address already exists");
                return false;
            }

            if (model.Password != model.ConfirmPassword)
            {
                _logger.LogWarning("Passwords do not match");
                return false;
            }

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
                var role = await _userManager.AddToRoleAsync(user, "Admin");
                _logger.LogInformation("User with Email: {email} assigned Admin role", user.Email);
            }
    
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var encodedToken = UrlEncoder.Default.Encode(token);
            var baseUrl = _config["Frontend:BaseUrl"];
            var confirmUrl = $"{baseUrl}/confirm-email?email={user.Email}&token={token}";
            _logger.LogInformation("Token has been generated");
            
            await _emailHelper.SendConfirmationEmail(user.Email, confirmUrl);
            
            _logger.LogInformation("User with Email: {email} has confirmed his email and has been registered", user.Email);
            return result.Succeeded;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Registration failed");
            return false;
        }
    }

    public async Task<bool> ConfirmEmail(string email, string token)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user != null)
        {
            var result = await _userManager.ConfirmEmailAsync(user, token);
            _logger.LogInformation("Confirmed email: {email} with Token: {token}", user.Email, token);
            return result.Succeeded;
        }
        _logger.LogInformation("Email {email} not found", email);
        return false;
    }

    public async Task<bool> ResetPassword(string email, string token ,string newPassword)
    {
        try
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user != null)
            {
                var result = await _userManager.ResetPasswordAsync(user, token, newPassword);
                _logger.LogInformation("Password reset for user with email: {email}", email);
                return result.Succeeded;
            }
            _logger.LogWarning("User with Email: {email} not found", email);
            return false;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to reset password");
            return false;
        }
    }

    public async Task<bool> ForgotPassword(string email)
    {
        try
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                _logger.LogWarning("Invalid email address");
                return false;
            }
            
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            _logger.LogInformation("Generated Password Reset Token for user with email: {email}", email);
            
            var encodedToken = UrlEncoder.Default.Encode(token);
            var baseUrl = _config["Frontend:BaseUrl"];
            var resetUrl = $"{baseUrl}/reset-password?email={email}&token={encodedToken}";
            
            await _emailHelper.SendResetPasswordEmail(user.Email, resetUrl);
            _logger.LogInformation("Sent Password Reset URL");
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Password Reset Failed");
            return false;
        }
    }
}