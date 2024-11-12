using api.DTOs.Features;

namespace api.Services.Interfaces
{
    public interface IFeatureService
    {
        Task<List<DisplayFeatureDTO>> GetFeaturesAsync();
        Task<DisplayFeatureDTO> CreateFeatureAsync(CreateFeatureDTO createFeatureDTO);
        Task<bool> UpdateFeatureAsync(string id, CreateFeatureDTO updateFeatureDTO);
        Task<bool> DeleteFeatureAsync(string id);
    }
}