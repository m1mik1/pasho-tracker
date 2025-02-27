using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PaSho_Tracker.Model;

namespace PaSho_Tracker.Data;

public class AppDbContext : IdentityDbContext<IdentityUser>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<TaskModel> Tasks { get; set; } = null!;
    public DbSet<CategoryModel> Categories { get; set; } = null!;
    public DbSet<CommentModel> Comments { get; set; } = null!;
}