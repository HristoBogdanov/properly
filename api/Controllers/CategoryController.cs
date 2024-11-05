using api.DTOs.Category;
using api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/categories")]
    [ApiController]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var categories = await _categoryService.GetCategoriesAsync();
                return Ok(categories);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateCategory([FromBody]CreateCategoryDTO createCategoryDTO)
        {
            try
            {
                var category = await _categoryService.CreateCategoryAsync(createCategoryDTO);
                return Ok(category);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateCategory([FromRoute]string id, [FromBody]CreateCategoryDTO displayCategoryDTO)
        {
            try
            {
                var category = await _categoryService.UpdateCategoryAsync(id, displayCategoryDTO);
                return Ok(category);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("delete/{id}")]
        public async Task<IActionResult> DeleteCategory([FromRoute]string id)
        {
            try
            {
                var category = await _categoryService.DeleteCategoryAsync(id);
                return Ok(category);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}