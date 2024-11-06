using api.DTOs.Property;
using api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/properties")]
    [ApiController]
    public class PropertyController : Controller
    {
        private readonly IPropertyService _propertyService;

        public PropertyController(IPropertyService propertyService)
        {
            _propertyService = propertyService;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var properties = await _propertyService.GetPropertiesAsync();
                return Ok(properties);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateProperty([FromBody]CreatePropertyDTO createPropertyDTO)
        {
            try
            {
                await _propertyService.CreatePropertyAsync(createPropertyDTO);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateProperty([FromRoute]string id, [FromBody]CreatePropertyDTO createPropertyDTO)
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

        [HttpPost("delete/{id}")]
        public async Task<IActionResult> DeleteProperty([FromRoute]string id)
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