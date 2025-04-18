using PaSho_Tracker.DTO;

namespace PaSho_Tracker.Interface;

public interface IAuthService
{
    Task<string?> Login(LoginDto model);
    Task<bool> Register(RegisterDto model);
}