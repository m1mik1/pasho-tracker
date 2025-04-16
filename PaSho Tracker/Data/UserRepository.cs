using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace PaSho_Tracker.Data;

public class UserRepository : BaseRepository<IdentityUser>
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

    public async Task<IdentityUser> GetByEmail(string email)
    {
        return await _userManager.FindByEmailAsync(email);
    }

    public async Task<IdentityUser> GetById(string id)
    {
        return await _userManager.FindByIdAsync(id);
    }
}