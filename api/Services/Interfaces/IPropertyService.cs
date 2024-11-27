using api.DTOs.Images;
using api.DTOs.Property;
using api.Helpers;

namespace api.Services.Interfaces
{
    public interface IPropertyService
    {
        Task<PropertyPagesResult> GetPropertiesAsync(PropertyQueryParams queryParams);
        Task<DisplayPropertyDTO> GetPropertyByIdAsync(string id);
        Task<DisplayPropertyDTO> CreatePropertyAsync(CreatePropertyDTO createPropertyDTO, string? userId);
        Task AddImageToPropertyAsync(string propertyId, CreateImageDTO image);
        Task<bool> UpdatePropertyAsync(string id, CreatePropertyDTO updatePropertyDTO, string? userId);
        Task<bool> DeletePropertyAsync(string id);
    }
}