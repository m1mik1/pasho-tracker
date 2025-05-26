using Microsoft.AspNetCore.Identity;
using PaSho_Tracker.Data;

namespace PaSho_Tracker.Interface;

public interface IUserRepository : IRepository<IdentityUser>
{
    Task<List<IdentityUser>> GetAll();
    Task<IdentityUser?> GetByEmail(string email);
}