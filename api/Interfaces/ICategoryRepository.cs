using api.DTOs.Category;

namespace api.Interfaces
{
    public interface ICategoryRepository
    {
        Task<List<DisplayCategoryWithPropertiesDTO>> GetCategoriesAsync();
        Task<DisplayCategoryDTO> CreateCategoryAsync(CreateCategoryDTO createCategoryDTO);
        Task<DisplayCategoryDTO> UpdateCategoryAsync(string id, CreateCategoryDTO updateCategoryDTO);
        Task<DisplayCategoryDTO> DeleteCategoryAsync(string id);
    }
}