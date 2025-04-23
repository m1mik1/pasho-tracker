using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PaSho_Tracker.DTO;
using PaSho_Tracker.Interface;

namespace PaSho_Tracker.API.Controllers;
    
    [Authorize(Roles = "Admin")]
    [ApiController]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Route("api/[controller]")]
    public class CategoriesController : BaseController
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ILogger<CategoriesController> logger, ICategoryService categoryRepository) 
            : base(logger)
        {
         _categoryService = categoryRepository;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var categories = await _categoryService.GetAll();
                return Ok(categories);
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var result = await _categoryService.Get(id);
                if (result == null) return NotFound();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> Create([FromBody] CreateCategoryDto model)
        {
            try
            {
                var result = await _categoryService.Create(model);
                if (result == null) return Conflict();
                return StatusCode(StatusCodes.Status201Created);
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
        public async Task<IActionResult> Update(int id, [FromBody] UpdateCategoryDto model)
        {
            try
            {
                if (id != model.Id) return BadRequest();
                
                var success = await _categoryService.Update(model);
                if (!success) return NotFound();
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
            try
            {
                var success = await _categoryService.Delete(id);
                if (!success) return NotFound();
                return NoContent();
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }
    }

