using api.DTOs.Category;
using api.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

/// <summary>
/// Controller for managing categories.
/// </summary>
[Route("api/categories")]
[ApiController]
public class CategoryController : ControllerBase
{
    private readonly ICategoryService _categoryService;

    /// <summary>
    /// Initializes a new instance of the <see cref="CategoryController"/> class.
    /// </summary>
    /// <param name="categoryService">The category service.</param>
    public CategoryController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    /// <summary>
    /// Gets all categories.
    /// </summary>
    /// <returns>A list of categories.</returns>
    [HttpGet("all")]
    [Authorize]
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

    /// <summary>
    /// Gets a category by its identifier.
    /// </summary>
    /// <param name="id">The category identifier.</param>
    /// <returns>The category with the specified identifier.</returns>
    [HttpGet("all/{id}")]
    [Authorize]
    public async Task<IActionResult> GetById(string id)
    {
        try
        {
            var category = await _categoryService.GetCategoryByIdAsync(id);
            return Ok(category);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Creates a new category.
    /// </summary>
    /// <param name="createCategoryDTO">The category data transfer object.</param>
    /// <returns>The created category.</returns>
    [HttpPost("create")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> CreateCategory(CreateCategoryDTO createCategoryDTO)
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

    /// <summary>
    /// Updates an existing category.
    /// </summary>
    /// <param name="id">The category identifier.</param>
    /// <param name="displayCategoryDTO">The category data transfer object.</param>
    /// <returns>The updated category.</returns>
    [HttpPut("update/{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> UpdateCategory(string id, CreateCategoryDTO displayCategoryDTO)
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

    /// <summary>
    /// Deletes a category by its identifier.
    /// </summary>
    /// <param name="id">The category identifier.</param>
    /// <returns>The deleted category.</returns>
    [HttpPost("delete/{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteCategory(string id)
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