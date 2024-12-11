using api.DTOs.Images;
using api.DTOs.Property;
using api.Helpers;
using api.Services.Interfaces;
using Entensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

/// <summary>
/// Controller for managing properties.
/// </summary>
[Route("api/properties")]
[ApiController]
public class PropertyController : ControllerBase
{
    private readonly IPropertyService _propertyService;

    /// <summary>
    /// Initializes a new instance of the <see cref="PropertyController"/> class.
    /// </summary>
    /// <param name="propertyService">The property service.</param>
    public PropertyController(IPropertyService propertyService)
    {
        _propertyService = propertyService;
    }

    /// <summary>
    /// Gets all properties based on query parameters.
    /// </summary>
    /// <param name="queryParams">The query parameters.</param>
    /// <returns>A list of properties.</returns>
    [HttpGet("all")]
    public async Task<IActionResult> GetAll([FromQuery] PropertyQueryParams queryParams)
    {
        try
        {
            var properties = await _propertyService.GetPropertiesAsync(queryParams);
            return Ok(properties);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Gets a property by its identifier.
    /// </summary>
    /// <param name="id">The property identifier.</param>
    /// <returns>The property with the specified identifier.</returns>
    [HttpGet("all/{id}")]
    [Authorize]
    public async Task<IActionResult> GetById(string id)
    {
        try
        {
            var property = await _propertyService.GetPropertyByIdAsync(id);
            return Ok(property);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Creates a new property.
    /// </summary>
    /// <param name="createPropertyDTO">The property data transfer object.</param>
    /// <returns>The created property.</returns>
    [HttpPost("create")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> CreateProperty(CreatePropertyDTO createPropertyDTO)
    {
        string? userId = User.GetUserId();

        try
        {
            var property = await _propertyService.CreatePropertyAsync(createPropertyDTO, userId);
            return Ok(property);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Updates an existing property.
    /// </summary>
    /// <param name="id">The property identifier.</param>
    /// <param name="createPropertyDTO">The property data transfer object.</param>
    /// <returns>An action result.</returns>
    [HttpPut("update/{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> UpdateProperty(string id, CreatePropertyDTO createPropertyDTO)
    {
        string? userId = User.GetUserId();

        try
        {
            await _propertyService.UpdatePropertyAsync(id, createPropertyDTO, userId);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Adds an image to a property.
    /// </summary>
    /// <param name="id">The property identifier.</param>
    /// <param name="image">The image data transfer object.</param>
    /// <returns>An action result.</returns>
    [HttpPost("add-image/{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> AddImageToProperty(string id, CreateImageDTO image)
    {
        try
        {
            await _propertyService.AddImageToPropertyAsync(id, image);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Deletes a property.
    /// </summary>
    /// <param name="id">The property identifier.</param>
    /// <returns>An action result.</returns>
    [HttpPost("delete/{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteProperty(string id)
    {
        try
        {
            await _propertyService.DeletePropertyAsync(id);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}