using PaSho_Tracker.Application.Services;
using PaSho_Tracker.Data;
using PaSho_Tracker.Domain.Model;
using PaSho_Tracker.DTO;
using PaSho_Tracker.Interface;
using PaSho_Tracker.Model;

namespace PaSho_Tracker.Services;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly ILogger<CategoryService> _logger;

    public CategoryService(ICategoryRepository categoryRepository, ILogger<CategoryService> logger)
    {
        _categoryRepository = categoryRepository;
        _logger = logger;
    }

    public async Task<IEnumerable<CategoryDto>> GetAll()
    {
        try
        {
            var categories = await _categoryRepository.GetAllAsync();
            List<CategoryDto> categoriesDtoList = new List<CategoryDto>();
            foreach (var category in categories)
            {
                var categoryDto = new CategoryDto
                {
                    Id = category.Id,
                    CategoryName = category.CategoryName,
                };
                categoriesDtoList.Add(categoryDto);
            }

            return categoriesDtoList;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting categories");
            return Enumerable.Empty<CategoryDto>();
        }
    }

    public async Task<CategoryDto?> Get(int id)
    {
        try
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            if (category == null)
            {
                _logger.LogInformation("Category with id {Id} not found", id);
                return null;
            }

            var categoryDto = new CategoryDto
            {
                Id = category.Id,
                CategoryName = category.CategoryName
            };
            return categoryDto;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting category with ID: {id}", id);
            return null;
        }
    }

    public async Task<CategoryDto?> Create(CreateCategoryDto model)
    {
        try
        {
            var exists = await _categoryRepository.ExistsByName(model.CategoryName);
            if (exists)
            {
                _logger.LogInformation("Category with name {CategoryName} already exists", model.CategoryName);
                return null;
            }

            var category = new CategoryModel
            {
                CategoryName = model.CategoryName,
            };
            var validationResults = ValidationService.Validate(category);
            if (validationResults.Any())
            {
                _logger.LogWarning("Validation failed: {Errors}", string.Join(" | ", validationResults.Select(r => r.ErrorMessage)));
                return null;
            }

            await _categoryRepository.AddAsync(category);
            var categoryDto = new CategoryDto
            {
                Id = category.Id,
                CategoryName = category.CategoryName
            };

            return categoryDto;
            
            
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating category");
            return null;
        }
    }

    public async Task<bool> Update(UpdateCategoryDto model)
    {
        try
        {
            var category = await _categoryRepository.GetByIdAsync(model.Id);
            if (category == null)
            {
                _logger.LogInformation("Category with id {Id} not found", model.Id);
                return false;
            }

            category.CategoryName = model.CategoryName;
            var validationResults = ValidationService.Validate(category);
            if (validationResults.Any())
            {
                _logger.LogWarning("Validation failed: {Errors}", string.Join(" | ", validationResults.Select(r => r.ErrorMessage)));
                return false;
            }
            await _categoryRepository.UpdateAsync(category);
            _logger.LogInformation("Category with id {Id} updated", model.Id);
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating category");
            return false;
        }
    }

    public async Task<bool> Delete(int id)
    {
        try
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            if (category == null)
            {
                _logger.LogInformation("Category with id {Id} not found", id);
                return false;
            }

            await _categoryRepository.DeleteAsync(category);
            _logger.LogInformation("Category with id {Id} deleted", id);
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting category");
            return false;
        }
    }
}
