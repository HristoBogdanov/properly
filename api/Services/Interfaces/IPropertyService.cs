using api.DTOs.Property;

namespace api.Services.Interfaces
{
    public interface IPropertyService
    {
        Task<List<DisplayPropertyDTO>> GetPropertiesAsync();
        Task<bool> CreatePropertyAsync(CreatePropertyDTO createPropertyDTO);
        Task<bool> UpdatePropertyAsync(string id, CreatePropertyDTO updatePropertyDTO);
        Task<bool> DeletePropertyAsync(string id);
    }
}