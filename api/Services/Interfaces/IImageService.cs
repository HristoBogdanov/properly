using api.DTOs.Images;

namespace api.Services.Interfaces
{
    public interface IImageService
    {
        Task<List<DisplayImageDTO>> GetImagesAsync();
        Task<bool> CreateImageAsync(CreateImageDTO createImageDTO);
        Task<bool> UpdateImageAsync(string id, CreateImageDTO updateImageDTO);
        Task<bool> DeleteImageAsync(string id);
    }
}