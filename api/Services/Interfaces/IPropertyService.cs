using api.DTOs.Property;
using api.Helpers;

namespace api.Services.Interfaces
{
    public interface IPropertyService
    {
        Task<PropertyPagesResult> GetPropertiesAsync(PropertyQueryParams queryParams);
        Task<DisplayPropertyDTO> GetPropertyByIdAsync(string id);
        Task<DisplayPropertyDTO> CreatePropertyAsync(CreatePropertyDTO createPropertyDTO);
        Task<bool> UpdatePropertyAsync(string id, CreatePropertyDTO updatePropertyDTO);
        Task<bool> DeletePropertyAsync(string id);
    }
}