using Microsoft.AspNetCore.Identity;
using PaSho_Tracker.Data;
using PaSho_Tracker.DTO;
using PaSho_Tracker.Interface;

namespace PaSho_Tracker.Services;

public class UserService : IUserService
{
    private readonly UserRepository _userRepository;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly ILogger<UserService> _logger;

    public UserService(UserRepository userRepository, UserManager<IdentityUser> userManager, ILogger<UserService> logger)
    {
        _userRepository = userRepository;
        _userManager = userManager;
        _logger = logger;
    }

    public async Task<List<UserDto>> GetAllUsers()
    {
        try
        {
            var users = await _userRepository.GetAll();
            List<UserDto> userDtoList = new List<UserDto>();
            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                var userDto = new UserDto
                {
                    Id = user.Id,
                    Email = user.Email,
                    UserName = user.UserName,
                    Roles = roles
                };
                userDtoList.Add(userDto);
            }
            _logger.LogInformation("Get all users");
            return userDtoList;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return null;
        }
    }

    public async Task<UserDto?> GetUserById(string id)
    {
        try
        {
            var user = await _userRepository.GetById(id);
            if (user != null)
            {
                var roles = await _userManager.GetRolesAsync(user);
                var userDto = new UserDto
                {
                    Id = user.Id,
                    Email = user.Email,
                    UserName = user.UserName,
                    Roles = roles
                };
                _logger.LogInformation("Get user with ID: {UserID}", id);
                return userDto;
            }
            _logger.LogWarning("User with ID: {ID} not found", id);
            return null;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return null;
        }
    }

    public async Task<UserDto?> GetUserByEmail(string email)
    {
        try
        {
            var user = await _userRepository.GetByEmail(email);
            if (user != null)
            {
                var roles = await _userManager.GetRolesAsync(user);
                var userDto = new UserDto
                {
                    Id = user.Id,
                    Email = user.Email,
                    UserName = user.UserName,
                    Roles = roles
                };
                _logger.LogInformation("Get user with email: {Email}", email);
                return userDto;
            }
            _logger.LogWarning("User with email: {Email} not found", email);
            return null;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return null;
        }
    }

    public async Task<bool> DeleteUserById(string id)
    {
        try
        {
            var user = await _userRepository.GetById(id);
            if (user != null)
            {
                var result = await _userManager.DeleteAsync(user);
                _logger.LogInformation("Deleted user with ID: {ID}", id);
                return result.Succeeded;
            }
            _logger.LogWarning("User with ID: {ID} not found", id);
            return false;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return false;
        }
    }
    public async Task<bool> AssignRole(string email, string role)
    {
        try
        {
            var user = await _userRepository.GetByEmail(email);
            if (user != null)
            {
                var roles = await _userManager.GetRolesAsync(user);
                if (roles.Contains(role))
                {
                    _logger.LogWarning("The user already has this role");
                    return false;
                }
                var result = await _userManager.AddToRoleAsync(user, role);
                _logger.LogInformation("Added role: {role} to user with email: {email}", role, email);
                return result.Succeeded;
            }
            _logger.LogWarning("User with email: {email} not found", email);
            return false;
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return false;
        }
    }

    public async Task<bool> RemoveRoleByEmail(string email, string role)
    {
        try
        {
            var user = await _userRepository.GetByEmail(email);
            if (user != null)
            {
                var roles = await _userManager.GetRolesAsync(user);
                if (roles.Contains(role))
                {
                    var result = await _userManager.RemoveFromRoleAsync(user, role);
                    _logger.LogInformation("Removed role: {role} to user with email: {email}", role, email);
                    return result.Succeeded;
                }
                _logger.LogWarning("User with email: {email} don't have this role: {role}", email , role);
                return false;
            }
            _logger.LogWarning("User with email: {email} not found", email);
            return false;
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return false;
        }
    }
    
    public async Task<bool> ChangePassword(ChangePasswordDto model)
    {
        try
        {
            var user = await _userRepository.GetByEmail(model.Email);
            if (user == null)
            {
                _logger.LogWarning("User with email: {Email} not found", model.Email);
                return false;
            }
            var result= await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
            if (result.Succeeded)
            {
                _logger.LogInformation("Password changed");
                return true;
            }
            _logger.LogWarning("Password change failed: {Errors}", string.Join(", ", result.Errors.Select(e => e.Description)));
            return false;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to change password.");
            return false;
        }
    }
}