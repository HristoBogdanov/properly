using api.DTOs.Images;
using api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/images")]
    [ApiController]
    public class ImageController : Controller
    {
        private readonly IImageService _imageService;
        public ImageController(IImageService imageService)
        {
            _imageService = imageService;
        }

        [HttpGet("all")]
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
        public async Task<IActionResult> CreateImage([FromBody]CreateImageDTO createImageDTO)
        {
            try
            {
                await _imageService.CreateImageAsync(createImageDTO);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateImage([FromRoute]string id, [FromBody]CreateImageDTO updateImageDTO)
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
        public async Task<IActionResult> DeleteImage([FromRoute]string id)
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