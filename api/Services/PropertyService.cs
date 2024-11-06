using api.Data.Repository.Interfaces;
using api.DTOs.Category;
using api.DTOs.Features;
using api.DTOs.Images;
using api.DTOs.Property;
using api.Models;
using api.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using api.Constants;

namespace api.Services
{
    public class PropertyService : IPropertyService
    {
        private readonly IRepository<Property, Guid> _propertyRepository;
        private readonly IRepository<Category, Guid> _categoryRepository;
        private readonly IRepository<Feature, Guid> _featureRepository;
        private readonly IRepository<Image, Guid> _imageRepository;
        private readonly IRepository<PropertyCategories, object> _propertyCategoriesRepository;
        private readonly IRepository<PropertyFeatures, object> _propertyFeaturesRepository;
        private readonly IRepository<PropertyImages, object> _propertyImagesRepository;

        public PropertyService(
        IRepository<Property, Guid> propertyRepository,
        IRepository<Image, Guid> imageRepository,
        IRepository<Category, Guid> categoryRepository,
        IRepository<Feature, Guid> featureRepository,
        IRepository<PropertyCategories, object> propertyCategoriesRepository,
        IRepository<PropertyFeatures, object> propertyFeaturesRepository,
        IRepository<PropertyImages, object> propertyImagesRepository)
        {
            _propertyRepository = propertyRepository;
            _imageRepository = imageRepository;
            _categoryRepository = categoryRepository;
            _featureRepository = featureRepository;
            _propertyCategoriesRepository = propertyCategoriesRepository;
            _propertyFeaturesRepository = propertyFeaturesRepository;
            _propertyImagesRepository = propertyImagesRepository;
        }

        public async Task<List<DisplayPropertyDTO>> GetPropertiesAsync()
        {
            var properties = await _propertyRepository.GetAllAttached()
            .Where(p => p.IsDeleted == false)
            .Select(p => new DisplayPropertyDTO
            {
                Id = p.Id.ToString(),
                Title = p.Title,
                Description = p.Description,
                Address = p.Address,
                Price = p.Price,
                ForSale = p.ForSale,
                ForRent = p.ForRent,
                Bedrooms = p.Bedrooms,
                Bathrooms = p.Bathrooms,
                IsFurnished = p.IsFurnished,
                Area = p.Area,
                YearOfConstruction = p.YearOfConstruction,
                OwnerId = p.OwnerId.ToString(),
                Categories = p.PropertiesCategories.Select(c => new DisplayCategoryDTO
                {
                    Id = c.CategoryId.ToString(),
                    Title = c.Category.Title
                }),
                Features = p.PropertiesFeatures.Select(f => new DisplayFeatureDTO
                {
                    Id = f.FeatureId.ToString(),
                    Title = f.Feature.Title,
                    Image = new CreateImageDTO
                    {
                        Name = f.Feature.Image.Name,
                        Path = f.Feature.Image.Path
                    }
                }),
                Images = p.PropertiesImages.Select(i => new CreateImageDTO
                {
                    Name = i.Image.Name,
                    Path = i.Image.Path
                })
            })
            .AsNoTracking()
            .ToListAsync();

            return properties;
        }
        public async Task<bool> CreatePropertyAsync(CreatePropertyDTO createPropertyDTO)
        {
            var newProperty = new Property
            {
                Title = createPropertyDTO.Title,
                Description = createPropertyDTO.Description,
                Address = createPropertyDTO.Address,
                Price = createPropertyDTO.Price,
                ForSale = createPropertyDTO.ForSale,
                ForRent = createPropertyDTO.ForRent,
                Area = createPropertyDTO.Area,
                YearOfConstruction = createPropertyDTO.YearOfConstruction,
                Bedrooms = createPropertyDTO.Bedrooms,
                Bathrooms = createPropertyDTO.Bathrooms,
                IsFurnished = createPropertyDTO.IsFurnished,
                OwnerId = Guid.Parse(createPropertyDTO.OwnerId),
            };

            await _propertyRepository.AddAsync(newProperty);
            await AddDataToProperty(createPropertyDTO, newProperty);

            return true;
        }

        public async Task<bool> UpdatePropertyAsync(string id, CreatePropertyDTO updatePropertyDTO)
        {
            Guid idGuid = Guid.Parse(id);

            var existingProperty = await _propertyRepository.FirstOrDefaultAsync(p => p.Id == idGuid && !p.IsDeleted);

            if(existingProperty == null)
            {
                throw new Exception(PropertiesErrorMessages.PropertyNotFound);
            }

            existingProperty.Title = updatePropertyDTO.Title;
            existingProperty.Description = updatePropertyDTO.Description;
            existingProperty.Address = updatePropertyDTO.Address;
            existingProperty.Price = updatePropertyDTO.Price;
            existingProperty.ForSale = updatePropertyDTO.ForSale;
            existingProperty.ForRent = updatePropertyDTO.ForRent;
            existingProperty.Area = updatePropertyDTO.Area;
            existingProperty.YearOfConstruction = updatePropertyDTO.YearOfConstruction;
            existingProperty.Bedrooms = updatePropertyDTO.Bedrooms;
            existingProperty.Bathrooms = updatePropertyDTO.Bathrooms;
            existingProperty.IsFurnished = updatePropertyDTO.IsFurnished;
            existingProperty.OwnerId = Guid.Parse(updatePropertyDTO.OwnerId);

            await _propertyRepository.UpdateAsync(existingProperty);
            await AddDataToProperty(updatePropertyDTO, existingProperty);
            return true;
        }

        public async Task<bool> DeletePropertyAsync(string id)
        {
            Guid idGuid = Guid.Parse(id);

            var existingProperty = await _propertyRepository.FirstOrDefaultAsync(p => p.Id == idGuid && !p.IsDeleted);

            if(existingProperty == null)
            {
                throw new Exception(PropertiesErrorMessages.PropertyNotFound);
            }

            await _propertyRepository.SoftDeleteAsync(existingProperty);
            return true;
        }

        private async Task AddDataToProperty(CreatePropertyDTO propertyDTO, Property newProperty)
        {
            // Adding all categories from the dto to the property and the join table
            foreach (var category in propertyDTO.Categories)
            {
                var existingCategory = await _categoryRepository.FirstOrDefaultAsync(c => c.Title == category);
                if (existingCategory == null)
                {
                    throw new NullReferenceException(CategoryErrorMessages.CategoryNotFound);
                }

                if(newProperty.PropertiesCategories.Any(pc => pc.CategoryId == existingCategory.Id))
                {
                    continue;
                }

                var propertyCategory = new PropertyCategories
                {
                    CategoryId = existingCategory.Id,
                    PropertyId = newProperty.Id
                };

                await _propertyCategoriesRepository.AddAsync(propertyCategory);
                newProperty.PropertiesCategories.Add(propertyCategory);
            }

            // Adding all features from the dto to the property and the join table
            foreach (var feature in propertyDTO.Features)
            {
                var existingFeature = await _featureRepository.FirstOrDefaultAsync(f => f.Title == feature);
                if (existingFeature == null)
                {
                    throw new NullReferenceException(FeatureErrorMessages.FeatureNotFound);
                }

                if(newProperty.PropertiesFeatures.Any(pf => pf.FeatureId == existingFeature.Id))
                {
                    continue;
                }

                var propertyFeature = new PropertyFeatures
                {
                    FeatureId = existingFeature.Id,
                    PropertyId = newProperty.Id
                };

                await _propertyFeaturesRepository.AddAsync(propertyFeature);
                newProperty.PropertiesFeatures.Add(propertyFeature);
            }

            // Adding all images from the dto to the property and the join table
            foreach (var image in propertyDTO.Images)
            {
                var existingImage = await _imageRepository.FirstOrDefaultAsync(i => i.Path == image.Path);
                if (existingImage == null)
                {
                    existingImage = new Image
                    {
                        Path = image.Path,
                        Name = image.Name
                    };

                    await _imageRepository.AddAsync(existingImage);
                }

                var existingPropertyImage = await _propertyImagesRepository.FirstOrDefaultAsync(pi => pi.PropertyId == newProperty.Id && pi.ImageId == existingImage.Id);
                if (existingPropertyImage == null)
                {
                    var propertyImage = new PropertyImages
                    {
                        ImageId = existingImage.Id,
                        PropertyId = newProperty.Id
                    };

                    await _propertyImagesRepository.AddAsync(propertyImage);
                    newProperty.PropertiesImages.Add(propertyImage);
                }
            }
        }
    }
}