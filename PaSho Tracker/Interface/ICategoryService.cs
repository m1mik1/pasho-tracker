using PaSho_Tracker.DTO;

namespace PaSho_Tracker.Interface;

public interface ICategoryService
{
    Task<List<CategoryDto>> GetAll();
    Task<CategoryDto?> Get(int id);
    Task<bool> Create(CategoryDto model);
    Task<bool> Update(CategoryDto model);
    Task<bool> Delete(int id);
}