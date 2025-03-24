using Microsoft.AspNetCore.Mvc;
using PaSho_Tracker.Data;
using PaSho_Tracker.Model;

namespace PaSho_Tracker.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : BaseController
    {
        private readonly CategoryRepository _categoryRepository;

        public CategoryController(ILogger<CategoryController> logger, CategoryRepository categoryRepository) 
            : base(logger)
        {
            _categoryRepository = categoryRepository;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var categories = await _categoryRepository.GetAllAsync();
            _logger.LogInformation("Returning all categories");
            return Ok(categories);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            if (category == null)
            {
                _logger.LogInformation($"Category with ID {id} not found");
                return NotFound($"Category with ID {id} not found");
            }
            _logger.LogInformation($"Returning category: {id}");
            return Ok(category);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> Create([FromBody] CategoryModel model)
        {
            if (string.IsNullOrWhiteSpace(model.CategoryName))
            {
                _logger.LogError("Category name is required");
                return BadRequest("Category name is required");
            }

            if (await _categoryRepository.ExistsByNameAsync(model.CategoryName))
            {
                _logger.LogError($"Category '{model.CategoryName}' already exists.");
                return Conflict($"Category '{model.CategoryName}' already exists.");
            }

            try
            {
                await _categoryRepository.AddAsync(model);
                _logger.LogInformation($"Category created successfully: {model.Id}");
                return CreatedAtAction(nameof(Get), new { id = model.Id }, model);
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(int id, [FromBody] CategoryModel model)
        {
            if (id != model.Id)
            {
                _logger.LogError("ID in URL does not match ID in object.");
                return BadRequest("ID in URL does not match ID in object.");
            }

            var category = await _categoryRepository.GetByIdAsync(id);
            if (category == null)
            {
                _logger.LogInformation($"Category with ID {id} not found.");
                return NotFound($"Category with ID {id} not found.");
            }

            try
            {
                category.CategoryName = model.CategoryName;
                await _categoryRepository.UpdateAsync(category);
                _logger.LogInformation($"Category updated successfully: {id}");
                return NoContent();
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            if (category == null)
            {
                _logger.LogInformation($"Category with ID {id} not found.");
                return NotFound($"Category with ID {id} not found.");
            }

            try
            {
                await _categoryRepository.DeleteAsync(category);
                _logger.LogInformation($"Category deleted successfully: {id}");
                return NoContent();
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }
        
        [HttpGet("sorted")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetSortedByName()
        {
            var sortedCategories = await _categoryRepository.GetSortedByNameAsync();
            _logger.LogInformation("Returning categories sorted by name");
            return Ok(sortedCategories);
        }

        [HttpGet("sorted-by-id")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetSortedById()
        {
            var sortedCategories = await _categoryRepository.GetSortedByIdAsync();
            _logger.LogInformation("Returning categories sorted by id");
            return Ok(sortedCategories);
        }
    }
}
