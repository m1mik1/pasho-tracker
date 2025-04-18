using System.Net;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Identity;
using PaSho_Tracker.DTO;
using PaSho_Tracker.Interface;

namespace PaSho_Tracker.Services;

public class AccountService : IAccountService
{
    private readonly ILogger<AccountService> _logger;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly IConfiguration _configuration;
    private readonly IEmailHelper _emailHelper;

    public AccountService(ILogger<AccountService> logger, UserManager<IdentityUser> userManager,
        IConfiguration configuration,
        IEmailHelper emailHelper)
    {
        _logger = logger;
        _userManager = userManager;
        _configuration = configuration;
        _emailHelper = emailHelper;
    }


    public async Task<bool> ConfirmEmail(ConfirmEmailDto model)
    {
        var user = await _userManager.FindByEmailAsync(model.Email);
        if (user == null)
        {
            _logger.LogInformation("Email {email} not found", model.Email);
            return false;
        }

        try
        {
            _logger.LogWarning("TOKEN FROM FRONT: {token}", model.Token);
            var decodedToken = WebUtility.UrlDecode(model.Token);
            _logger.LogWarning("DECODED TOKEN: {token}", decodedToken);
            var result = await _userManager.ConfirmEmailAsync(user, decodedToken);

            _logger.LogInformation("Confirmed email: {email} with Token: {token}", user.Email, model.Token);
            return result.Succeeded;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Email confirmation failed");
            return false;
        }
    }

    public async Task<bool> ResetPassword(ResetPasswordDto model)
    {
        var user = await _userManager.FindByEmailAsync(model.Email);
        if (user == null)
        {
            _logger.LogWarning("User with Email: {email} not found", model.Email);
            return false;
        }

        try
        {
            var result = await _userManager.ResetPasswordAsync(user, model.Token, model.NewPassword);
            _logger.LogInformation("Password reset for user with email: {email}", model.Email);
            return result.Succeeded;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to reset password");
            return false;
        }
    }

    public async Task<bool> ForgotPassword(string email)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user == null)
        {
            _logger.LogWarning("Invalid email address");
            return false;
        }

        var token = await _userManager.GeneratePasswordResetTokenAsync(user);
        if (string.IsNullOrEmpty(token))
        {
            _logger.LogWarning("Invalid password reset token");
            return false;
        }

        try
        {
            _logger.LogInformation("Generated Password Reset Token for user with email: {email}", email);

            var encodedToken = UrlEncoder.Default.Encode(token);
            var baseUrl = _configuration["Frontend:BaseUrl"];
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