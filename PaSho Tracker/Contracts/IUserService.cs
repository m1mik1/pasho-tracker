using PaSho_Tracker.DTO;

namespace PaSho_Tracker.Interface;

public interface IUserService
{
    Task<IEnumerable<UserDto>> GetAllUsers();
    Task<UserDto?> GetUserById(string id);
    Task<UserDto?> GetUserByEmail(string email);
    Task<bool> DeleteUserById(string id);
    Task<bool> AssignRole(string email, string role);
    Task<bool> RemoveRoleByEmail(string id, string role);
    Task<bool> ChangePassword(ChangePasswordDto model);
}