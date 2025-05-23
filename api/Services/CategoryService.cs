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
        private readonly IRepository<PropertyCategories, object> _propertyCategoryRepository;

        public CategoryService(IRepository<Category, Guid> categoryRepository,
            IRepository<PropertyCategories, object> propertyCategoryRepository)
        {
            _categoryRepository = categoryRepository;
            _propertyCategoryRepository = propertyCategoryRepository;
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
                    Slug = pc.Property.Slug,
                    Address = pc.Property.Address,
                    Price = pc.Property.Price,
                    CreatedAt = pc.Property.CreatedAt,
                    ForSale = pc.Property.ForSale,
                    ForRent = pc.Property.ForRent,
                    Bedrooms = pc.Property.Bedrooms,
                    Bathrooms = pc.Property.Bathrooms,
                    IsFurnished = pc.Property.IsFurnished,
                    Area = pc.Property.Area,
                    YearOfConstruction = pc.Property.YearOfConstruction,
                    OwnerId = pc.Property.OwnerId.ToString(),
                    OwnerName = pc.Property.Owner.UserName!,
                }).ToList()
            })
            .AsNoTracking()
            .ToListAsync();

            return categories;
        }

        public async Task<DisplayCategoryDTO> GetCategoryByIdAsync(string id)
        {
            Guid IdGuid = Guid.Parse(id);

            var category = await _categoryRepository
            .FirstOrDefaultAsync(c => c.Id == IdGuid && !c.IsDeleted);

            if (category == null)
            {
                throw new Exception(CategoryErrorMessages.CategoryNotFound);
            }

            return new DisplayCategoryDTO
            {
                Id = category.Id.ToString(),
                Title = category.Title
            };
        }

        public async Task<DisplayCategoryDTO> CreateCategoryAsync(CreateCategoryDTO createCategoryDTO)
        {
            var existingCategory = await _categoryRepository
            .FirstOrDefaultAsync(c => c.Title == createCategoryDTO.Title && !c.IsDeleted);

            if (existingCategory != null)
            {
                throw new Exception(CategoryErrorMessages.CategoryExists);
            }

            var newCategory = new Category
            {
                Title = createCategoryDTO.Title
            };

            await _categoryRepository.AddAsync(newCategory);

            return new DisplayCategoryDTO
            {
                Id = newCategory.Id.ToString(),
                Title = newCategory.Title
            };
        }

        public async Task<bool> UpdateCategoryAsync(string id, CreateCategoryDTO updateCategoryDTO)
        {
            Guid IdGuid = Guid.Parse(id);

            var existingCategory = await _categoryRepository
                .FirstOrDefaultAsync(c => c.Id == IdGuid && !c.IsDeleted);

            if (existingCategory == null)
            {
                throw new Exception(CategoryErrorMessages.CategoryNotFound);
            }

            existingCategory.Title = updateCategoryDTO.Title;
            await _categoryRepository.UpdateAsync(existingCategory);

            return true;
        }

        public async Task<bool> DeleteCategoryAsync(string id)
        {
            Guid IdGuid = Guid.Parse(id);

            var existingCategory = await _categoryRepository
            .FirstOrDefaultAsync(c => c.Id == IdGuid && !c.IsDeleted);

            if (existingCategory == null)
            {
                throw new Exception(CategoryErrorMessages.CategoryNotFound);
            }

            var propertiesCategories = await _propertyCategoryRepository
                .GetAllAttached()
                .Where(pc => pc.CategoryId == IdGuid)
                .ToListAsync();

            foreach (var propertyCategory in propertiesCategories)
            {
                await _propertyCategoryRepository.DeleteAsync(propertyCategory);
            }

            await _categoryRepository.SoftDeleteAsync(existingCategory);

            return true;
        }
    }
}