using PaSho_Tracker.DTO;

namespace PaSho_Tracker.Interface;

public interface IAuthService
{
    Task<string?> Login(LoginDto model);
    Task<bool> Register(RegisterDto model);
    Task<bool> ConfirmEmail(string email, string token);
    Task<bool> ResetPassword(string email, string token, string newPassword);
    Task<bool> ForgotPassword(string email);
}