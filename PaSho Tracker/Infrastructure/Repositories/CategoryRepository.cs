using Microsoft.EntityFrameworkCore;
using PaSho_Tracker.Domain.Model;
using PaSho_Tracker.DTO;
using PaSho_Tracker.Interface;
using PaSho_Tracker.Model;

namespace PaSho_Tracker.Data
{
    public class CategoryRepository : BaseRepository<CategoryModel>, ICategoryRepository
    {
        public CategoryRepository(AppDbContext context) : base(context) { }

        public async Task<bool> ExistsByName(string categoryName)
        {
            return await _context.Categories.AnyAsync(c => c.CategoryName == categoryName);
        }
    }
}
