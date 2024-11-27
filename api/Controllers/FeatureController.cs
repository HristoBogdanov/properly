using api.DTOs.Features;
using api.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/features")]
    [ApiController]
    public class FeatureController : ControllerBase
    {
        private readonly IFeatureService _featureService;

        public FeatureController(IFeatureService featureService)
        {
            _featureService = featureService;
        }

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
}