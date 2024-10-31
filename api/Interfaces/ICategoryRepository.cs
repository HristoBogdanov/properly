using api.DTOs.Category;

namespace api.Interfaces
{
    public interface ICategoryRepository
    {
        Task<List<DisplayCategoryWithPropertiesDTO>> GetCategoriesAsync();
        Task<DisplayCategoryDTO> CreateCategoryAsync(DisplayCategoryDTO createCategoryDTO);
        Task<DisplayCategoryDTO> UpdateCategoryAsync(string id, DisplayCategoryDTO updateCategoryDTO);
        Task<DisplayCategoryDTO> DeleteCategoryAsync(string id);
    }
}