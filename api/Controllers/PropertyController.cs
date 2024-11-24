using api.DTOs.Images;
using api.DTOs.Property;
using api.Helpers;
using api.Services.Interfaces;
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
        public async Task<IActionResult> CreateProperty(CreatePropertyDTO createPropertyDTO)
        {
            try
            {
                var property = await _propertyService.CreatePropertyAsync(createPropertyDTO);
                return Ok(property);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateProperty(string id, CreatePropertyDTO createPropertyDTO)
        {
            try
            {
                await _propertyService.UpdatePropertyAsync(id, createPropertyDTO);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("add-image/{id}")]
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