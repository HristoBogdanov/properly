using api.Constants;
using api.Data;
using api.DTOs.Category;
using api.DTOs.Property;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _context;

        public CategoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<DisplayCategoryWithPropertiesDTO>> GetCategoriesAsync()
        {
            var categories = await _context.Categories
            .Where(c => !c.IsDeleted)
            .Select(c => new DisplayCategoryWithPropertiesDTO
            {
                Id = c.Id,
                Title = c.Title,
                Properties = c.PropertiesCategories.Select(pc => new DisplaySimplePropertyDTO
                {
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
                }).ToList()
            })
            .AsNoTracking()
            .ToListAsync();

            return categories;
        }

        public async Task<DisplayCategoryDTO> CreateCategoryAsync(CreateCategoryDTO createCategoryDTO)
        {
            var existingCategory = await _context.Categories
            .Where(c => !c.IsDeleted)
            .FirstOrDefaultAsync(c => c.Title == createCategoryDTO.Title);
            if(existingCategory != null)
            {
                throw new Exception(CategoryErrorMessages.CategoryExists);
            }

            var newCategory = new Category
            {
                Title = createCategoryDTO.Title
            };

            _context.Categories.Add(newCategory);
            await _context.SaveChangesAsync();

            return new DisplayCategoryDTO
            {
                Id = newCategory.Id,
                Title = newCategory.Title
            };
        }

        public async Task<DisplayCategoryDTO> UpdateCategoryAsync(string id, CreateCategoryDTO updateCategoryDTO)
        {
            Guid IdGuid = Guid.Parse(id);

            var existingCategory = await _context.Categories
            .Where(c => !c.IsDeleted)
            .FirstOrDefaultAsync(c => c.Id == IdGuid);

            if(existingCategory == null)
            {
                throw new Exception(CategoryErrorMessages.CategoryNotFound);
            }

            existingCategory.Title = updateCategoryDTO.Title;
            await _context.SaveChangesAsync();

            return new DisplayCategoryDTO
            {
                Id = existingCategory.Id,
                Title = existingCategory.Title
            };
        }

        public async Task<DisplayCategoryDTO> DeleteCategoryAsync(string id)
        {
            Guid IdGuid = Guid.Parse(id);

            var existingCategory = await _context.Categories
            .Where(c => !c.IsDeleted)
            .FirstOrDefaultAsync(c => c.Id == IdGuid);

            if(existingCategory == null)
            {
                throw new Exception(CategoryErrorMessages.CategoryNotFound);
            }

            existingCategory.IsDeleted = true;
            await _context.SaveChangesAsync();

            return new DisplayCategoryDTO(){
                Id = existingCategory.Id,
                Title = existingCategory.Title
            };
        }
    }
}