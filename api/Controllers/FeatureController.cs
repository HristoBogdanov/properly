using api.DTOs.Features;
using api.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

/// <summary>
/// Controller for managing features.
/// </summary>
[Route("api/features")]
[ApiController]
public class FeatureController : ControllerBase
{
    private readonly IFeatureService _featureService;

    /// <summary>
    /// Initializes a new instance of the <see cref="FeatureController"/> class.
    /// </summary>
    /// <param name="featureService">The feature service.</param>
    public FeatureController(IFeatureService featureService)
    {
        _featureService = featureService;
    }

    /// <summary>
    /// Gets all features.
    /// </summary>
    /// <returns>A list of features.</returns>
    [HttpGet("all")]
    [Authorize]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            var features = await _featureService.GetFeaturesAsync();
            return Ok(features);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Gets a feature by its identifier.
    /// </summary>
    /// <param name="id">The feature identifier.</param>
    /// <returns>The feature with the specified identifier.</returns>
    [HttpGet("all/{id}")]
    [Authorize]
    public async Task<IActionResult> GetById(string id)
    {
        try
        {
            var feature = await _featureService.GetFeatureByIdAsync(id);
            return Ok(feature);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Creates a new feature.
    /// </summary>
    /// <param name="createFeatureDTO">The feature data transfer object.</param>
    /// <returns>The created feature.</returns>
    [HttpPost("create")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> CreateFeature(CreateFeatureDTO createFeatureDTO)
    {
        try
        {
            var feature = await _featureService.CreateFeatureAsync(createFeatureDTO);
            return Ok(feature);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Updates an existing feature.
    /// </summary>
    /// <param name="id">The feature identifier.</param>
    /// <param name="displayFeatureDTO">The feature data transfer object.</param>
    /// <returns>An action result.</returns>
    [HttpPut("update/{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> UpdateFeature(string id, CreateFeatureDTO displayFeatureDTO)
    {
        try
        {
            await _featureService.UpdateFeatureAsync(id, displayFeatureDTO);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Deletes a feature.
    /// </summary>
    /// <param name="id">The feature identifier.</param>
    /// <returns>An action result.</returns>
    [HttpPost("delete/{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteFeature(string id)
    {
        try
        {
            await _featureService.DeleteFeatureAsync(id);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}