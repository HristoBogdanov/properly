using api.DTOs.Images;
using api.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

/// <summary>
/// Controller for managing images.
/// </summary>
[Route("api/images")]
[ApiController]
public class ImageController : ControllerBase
{
    private readonly IImageService _imageService;

    /// <summary>
    /// Initializes a new instance of the <see cref="ImageController"/> class.
    /// </summary>
    /// <param name="imageService">The image service.</param>
    public ImageController(IImageService imageService)
    {
        _imageService = imageService;
    }

    /// <summary>
    /// Gets all images.
    /// </summary>
    /// <returns>A list of images.</returns>
    [HttpGet("all")]
    [Authorize]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            var images = await _imageService.GetImagesAsync();
            return Ok(images);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Creates a new image.
    /// </summary>
    /// <param name="createImageDTO">The data transfer object for creating an image.</param>
    /// <returns>The created image.</returns>
    [HttpPost("create")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> CreateImage(CreateImageDTO createImageDTO)
    {
        try
        {
            var image = await _imageService.CreateImageAsync(createImageDTO);
            return Ok(image);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Updates an existing image.
    /// </summary>
    /// <param name="id">The ID of the image to update.</param>
    /// <param name="updateImageDTO">The data transfer object for updating the image.</param>
    /// <returns>An action result.</returns>
    [HttpPut("update/{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> UpdateImage(string id, CreateImageDTO updateImageDTO)
    {
        try
        {
            await _imageService.UpdateImageAsync(id, updateImageDTO);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Deletes an image.
    /// </summary>
    /// <param name="id">The ID of the image to delete.</param>
    /// <returns>An action result.</returns>
    [HttpDelete("delete/{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteImage(string id)
    {
        try
        {
            await _imageService.DeleteImageAsync(id);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}