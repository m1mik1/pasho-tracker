using PaSho_Tracker.Data;
using PaSho_Tracker.Domain.Model;
using PaSho_Tracker.Model;

namespace PaSho_Tracker.Interface;

public interface ICategoryRepository : IRepository<CategoryModel>
{
    Task<bool> ExistsByName(string categoryName);
}