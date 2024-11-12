using api.DTOs.Category;

namespace api.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<List<DisplayCategoryWithPropertiesDTO>> GetCategoriesAsync();
        Task<DisplayCategoryDTO> CreateCategoryAsync(CreateCategoryDTO createCategoryDTO);
        Task<bool> UpdateCategoryAsync(string id, CreateCategoryDTO updateCategoryDTO);
        Task<bool> DeleteCategoryAsync(string id);
    }
}