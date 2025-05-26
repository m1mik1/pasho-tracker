using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PaSho_Tracker.Interface;

namespace PaSho_Tracker.Data;

public class UserRepository : BaseRepository<IdentityUser>, IUserRepository
{
    private readonly UserManager<IdentityUser> _userManager;

    public UserRepository(AppDbContext context, UserManager<IdentityUser> userManager) : base(context)
    {
        _userManager = userManager;
    }

    public async Task<List<IdentityUser>> GetAll()
    {
        return await _userManager.Users.ToListAsync();
    }

    public async Task<IdentityUser?> GetByEmail(string email)
    {
        return await _userManager.FindByEmailAsync(email);
    }

    
}