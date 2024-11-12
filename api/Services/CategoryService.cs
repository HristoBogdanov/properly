using api.Constants;
using api.Data.Repository.Interfaces;
using api.DTOs.Category;
using api.DTOs.Property;
using api.Models;
using api.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace api.Services
{
    public class CategoryService : BaseService, ICategoryService
    {
        private readonly IRepository<Category, Guid> _categoryRepository;

        public CategoryService(IRepository<Category, Guid> categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<List<DisplayCategoryWithPropertiesDTO>> GetCategoriesAsync()
        {
            var categories = await _categoryRepository.GetAllAttached()
            .Where(c => !c.IsDeleted)
            .Select(c => new DisplayCategoryWithPropertiesDTO
            {
                Id = c.Id,
                Title = c.Title,
                Properties = c.PropertiesCategories.Select(pc => new DisplaySimplePropertyDTO
                {
                    Id = pc.Property.Id.ToString(),
                    Title = pc.Property.Title,
                    Description = pc.Property.Description,
                    Address = pc.Property.Address,
                    Price = pc.Property.Price,
                    ForSale = pc.Property.ForSale,
                    ForRent = pc.Property.ForRent,
                    Bedrooms = pc.Property.Bedrooms,
                    Bathrooms = pc.Property.Bathrooms,
                    IsFurnished = pc.Property.IsFurnished,
                    Area = pc.Property.Area,
                    YearOfConstruction = pc.Property.YearOfConstruction,
                    OwnerId = pc.Property.OwnerId.ToString(),
                }).ToList()
            })
            .AsNoTracking()
            .ToListAsync();

            return categories;
        }

        public async Task<bool> CreateCategoryAsync(CreateCategoryDTO createCategoryDTO)
        {
            var existingCategory = await _categoryRepository
            .FirstOrDefaultAsync(c => c.Title == createCategoryDTO.Title && !c.IsDeleted);

            if(existingCategory != null)
            {
                throw new Exception(CategoryErrorMessages.CategoryExists);
            }

            var newCategory = new Category
            {
                Title = createCategoryDTO.Title
            };

            await _categoryRepository.AddAsync(newCategory);
            return true;
        }

        public async Task<bool> UpdateCategoryAsync(string id, CreateCategoryDTO updateCategoryDTO)
        {
            Guid IdGuid = Guid.Parse(id);

            var existingCategory = await _categoryRepository
            .FirstOrDefaultAsync(c => c.Id == IdGuid && !c.IsDeleted);

            existingCategory.Title = updateCategoryDTO.Title;
            await _categoryRepository.UpdateAsync(existingCategory);

            return true;
        }

        public async Task<bool> DeleteCategoryAsync(string id)
        {
            Guid IdGuid = Guid.Parse(id);

            var existingCategory = await _categoryRepository
            .FirstOrDefaultAsync(c => c.Id == IdGuid && !c.IsDeleted);

            if(existingCategory == null)
            {
                throw new Exception(CategoryErrorMessages.CategoryNotFound);
            }

            await _categoryRepository.SoftDeleteAsync(existingCategory);

            return true;
        }
    }
}