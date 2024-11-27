using api.DTOs.Images;
using api.DTOs.Property;
using api.Helpers;
using api.Services.Interfaces;
using Entensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/properties")]
    [ApiController]
    public class PropertyController : ControllerBase
    {
        private readonly IPropertyService _propertyService;

        public PropertyController(IPropertyService propertyService)
        {
            _propertyService = propertyService;
        }

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
}