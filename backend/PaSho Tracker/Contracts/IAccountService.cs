using PaSho_Tracker.DTO;

namespace PaSho_Tracker.Interface;

public interface IAccountService
{
    Task<bool> ConfirmEmail(ConfirmEmailDto model);
    Task<bool> ResetPassword(ResetPasswordDto model);
    Task<bool> ForgotPassword(string email);
}