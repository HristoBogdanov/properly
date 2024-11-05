using api.DTOs.Features;
using api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/features")]
    [ApiController]
    public class FeatureController : Controller
    {
        private readonly IFeatureService _featureService;

        public FeatureController(IFeatureService featureService)
        {
            _featureService = featureService;
        }

        [HttpGet("all")]
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

        [HttpPost("create")]
        public async Task<IActionResult> CreateFeature([FromBody]CreateFeatureDTO createFeatureDTO)
        {
            try
            {
                await _featureService.CreateFeatureAsync(createFeatureDTO);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateFeature([FromRoute]string id, [FromBody]CreateFeatureDTO displayFeatureDTO)
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

        [HttpPost("delete/{id}")]
        public async Task<IActionResult> DeleteFeature([FromRoute]string id)
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
}