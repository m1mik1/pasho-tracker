using Microsoft.EntityFrameworkCore;
using PaSho_Tracker.Model;

namespace PaSho_Tracker.Data
{
    public class CategoryRepository : BaseRepository<CategoryModel>
    {
        public CategoryRepository(AppDbContext context) : base(context) { }

        public async Task<bool> ExistsByNameAsync(string categoryName)
        {
            return await _context.Categories.AnyAsync(c => c.CategoryName == categoryName);
        }

        public async Task<IEnumerable<CategoryModel>> GetSortedByNameAsync()
        {
            return await _context.Categories.OrderBy(c => c.CategoryName).ToListAsync();
        }

        public async Task<IEnumerable<CategoryModel>> GetSortedByIdAsync()
        {
            return await _context.Categories.OrderBy(c => c.Id).ToListAsync();
        }
    }
}
