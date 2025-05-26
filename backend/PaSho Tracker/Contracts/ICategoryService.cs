using PaSho_Tracker.DTO;

namespace PaSho_Tracker.Interface;

public interface ICategoryService
{
    Task<IEnumerable<CategoryDto>> GetAll();
    Task<CategoryDto?> Get(int id);
    Task<CategoryDto?> Create(CreateCategoryDto model);
    Task<bool> Update(UpdateCategoryDto model);
    Task<bool> Delete(int id);
}