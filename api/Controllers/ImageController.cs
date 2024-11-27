using api.DTOs.Images;
using api.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/images")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly IImageService _imageService;
        public ImageController(IImageService imageService)
        {
            _imageService = imageService;
        }

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
}