using api.Data.Repository.Interfaces;
using api.DTOs.Category;
using api.DTOs.Features;
using api.DTOs.Images;
using api.DTOs.Property;
using api.Models;
using api.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using api.Constants;
using api.Helpers;
using System.Linq.Expressions;

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

        public async Task<List<DisplayPropertyDTO>> GetPropertiesAsync(PropertyQueryParams queryParams)
        {
            var properties = GetDisplayProperties(p => !p.IsDeleted);

            properties = FilterProperties(properties, queryParams);
            properties = SortProperties(properties, queryParams);
            properties = PaginateProperties(properties, queryParams);

            return await properties.ToListAsync();
        }

        public async Task<DisplayPropertyDTO> GetPropertyByIdAsync(string id)
        {
            Guid idGuid = Guid.Parse(id);

            var property = await GetDisplayProperties(p => p.Id == idGuid && !p.IsDeleted)
            .FirstOrDefaultAsync();

            if(property == null)
            {
                throw new Exception(PropertiesErrorMessages.PropertyNotFound);
            }

            return property;
        }

        public async Task<DisplayPropertyDTO> CreatePropertyAsync(CreatePropertyDTO createPropertyDTO)
        {
            var newProperty = new Property();
            MapPropertyFields(newProperty, createPropertyDTO);

            await _propertyRepository.AddAsync(newProperty);
            await AddDataToProperty(createPropertyDTO, newProperty);

            return new DisplayPropertyDTO
            {
                Id = newProperty.Id.ToString(),
                Title = newProperty.Title,
                Description = newProperty.Description,
                Address = newProperty.Address,
                Price = newProperty.Price,
                CreatedAt = newProperty.CreatedAt,
                ForSale = newProperty.ForSale,
                ForRent = newProperty.ForRent,
                Bedrooms = newProperty.Bedrooms,
                Bathrooms = newProperty.Bathrooms,
                IsFurnished = newProperty.IsFurnished,
                Area = newProperty.Area,
                YearOfConstruction = newProperty.YearOfConstruction,
                OwnerId = newProperty.OwnerId.ToString(),
                Categories = newProperty.PropertiesCategories.Select(c => new DisplayCategoryDTO
                {
                    Id = c.CategoryId.ToString(),
                    Title = c.Category.Title
                }),
                Features = newProperty.PropertiesFeatures.Select(f => new DisplayFeatureDTO
                {
                    Id = f.FeatureId.ToString(),
                    Title = f.Feature.Title,
                    Image = new CreateImageDTO
                    {
                        Name = f.Feature.Image.Name,
                        Path = f.Feature.Image.Path
                    }
                }),
                Images = newProperty.PropertiesImages.Select(i => new CreateImageDTO
                {
                    Name = i.Image.Name,
                    Path = i.Image.Path
                })
            };
        }

        public async Task<bool> UpdatePropertyAsync(string id, CreatePropertyDTO updatePropertyDTO)
        {
            Guid idGuid = Guid.Parse(id);

            var existingProperty = await _propertyRepository.FirstOrDefaultAsync(p => p.Id == idGuid && !p.IsDeleted);

            if(existingProperty == null)
            {
                throw new Exception(PropertiesErrorMessages.PropertyNotFound);
            }

            MapPropertyFields(existingProperty, updatePropertyDTO);

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

        private void MapPropertyFields(Property property, CreatePropertyDTO propertyDTO)
        {
            property.Title = propertyDTO.Title;
            property.Description = propertyDTO.Description;
            property.Address = propertyDTO.Address;
            property.Price = propertyDTO.Price;
            property.ForSale = propertyDTO.ForSale;
            property.ForRent = propertyDTO.ForRent;
            property.Area = propertyDTO.Area;
            property.YearOfConstruction = propertyDTO.YearOfConstruction;
            property.Bedrooms = propertyDTO.Bedrooms;
            property.Bathrooms = propertyDTO.Bathrooms;
            property.IsFurnished = propertyDTO.IsFurnished;
            property.OwnerId = Guid.Parse(propertyDTO.OwnerId);
        }

        private IQueryable<DisplayPropertyDTO> GetDisplayProperties(Expression<Func<Property, bool>> predicate)
        {
            var properties = _propertyRepository.GetAllAttached()
            .Where(predicate)
            .Select(p => new DisplayPropertyDTO
            {
                Id = p.Id.ToString(),
                Title = p.Title,
                Description = p.Description,
                Address = p.Address,
                Price = p.Price,
                CreatedAt = p.CreatedAt,
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
            .OrderByDescending(p => p.CreatedAt)
            .AsNoTracking();

            return properties;
        }

        private IQueryable<DisplayPropertyDTO> FilterProperties(IQueryable<DisplayPropertyDTO> properties, PropertyQueryParams queryParams)
        {
            if(!string.IsNullOrEmpty(queryParams.search))
            {
                properties = properties
                .Where(p => p.Title.ToLower().Contains(queryParams.search.ToLower()) 
                || p.Description.ToLower().Contains(queryParams.search.ToLower())
                || p.Address.ToLower().Contains(queryParams.search.ToLower()));
            }

            if(queryParams.minPrice != null)
            {
                properties = properties
                .Where(p => p.Price >= queryParams.minPrice);
            }

            if(queryParams.maxPrice != null)
            {
                properties = properties
                .Where(p => p.Price <= queryParams.maxPrice);
            }

            if(queryParams.numberOfBedrooms != null)
            {
                properties = properties
                .Where(p => p.Bedrooms == queryParams.numberOfBedrooms);
            }

            if(queryParams.numberOfBathrooms != null)
            {
                properties = properties
                .Where(p => p.Bathrooms == queryParams.numberOfBathrooms);
            }

            if(queryParams.minArea != null)
            {
                properties = properties
                .Where(p => p.Area >= queryParams.minArea);
            }

            if(queryParams.maxArea != null)
            {
                properties = properties
                .Where(p => p.Area <= queryParams.maxArea);
            }

            if(queryParams.minYearOfConstruction != null)
            {
                properties = properties
                .Where(p => p.YearOfConstruction >= queryParams.minYearOfConstruction);
            }

            if(queryParams.maxYearOfConstruction != null)
            {
                properties = properties
                .Where(p => p.YearOfConstruction <= queryParams.maxYearOfConstruction);
            }

            if(queryParams.forSale != null)
            {
                properties = properties
                .Where(p => p.ForSale == queryParams.forSale);
            }

            if(queryParams.forRent != null)
            {
                properties = properties
                .Where(p => p.ForRent == queryParams.forRent);
            }

            if(queryParams.isFurnished != null)
            {
                properties = properties
                .Where(p => p.IsFurnished == queryParams.isFurnished);
            }

            return properties;
        }

        private IQueryable<DisplayPropertyDTO> SortProperties(IQueryable<DisplayPropertyDTO> properties, PropertyQueryParams queryParams)
        {
            if(queryParams.sortBy != null)
            {
                properties = queryParams.sortBy switch
                {
                    "price" => queryParams.descending ? properties.OrderByDescending(p => p.Price) : properties.OrderBy(p => p.Price),
                    "area" => queryParams.descending ? properties.OrderByDescending(p => p.Area) : properties.OrderBy(p => p.Area),
                    "yearOfConstruction" => queryParams.descending ? properties.OrderByDescending(p => p.YearOfConstruction) : properties.OrderBy(p => p.YearOfConstruction),
                    _ => properties
                };
            }

            return properties;
        }

        private IQueryable<DisplayPropertyDTO> PaginateProperties(IQueryable<DisplayPropertyDTO> properties, PropertyQueryParams queryParams)
        {
            return properties
            .Skip((queryParams.page - 1) * queryParams.perPage)
            .Take(queryParams.perPage);
        }

        private async Task AddDataToProperty(CreatePropertyDTO propertyDTO, Property newProperty)
        {
            // Adding all categories from the dto to the property and the join table
            foreach (var category in propertyDTO.Categories)
            {
                var existingCategory = await _categoryRepository.FirstOrDefaultAsync(c => c.Title == category);
                if (existingCategory != null && !newProperty.PropertiesCategories.Any(pc => pc.CategoryId == existingCategory.Id))
                {
                    var propertyCategory = new PropertyCategories
                    {
                        CategoryId = existingCategory.Id,
                        PropertyId = newProperty.Id
                    };

                    await _propertyCategoriesRepository.AddAsync(propertyCategory);
                    newProperty.PropertiesCategories.Add(propertyCategory);
                }
            }

            // Adding all features from the dto to the property and the join table
            foreach (var feature in propertyDTO.Features)
            {
                var existingFeature = await _featureRepository.FirstOrDefaultAsync(f => f.Title == feature);
                if (existingFeature != null && !newProperty.PropertiesFeatures.Any(pf => pf.FeatureId == existingFeature.Id))
                {
                    var propertyFeature = new PropertyFeatures
                    {
                        FeatureId = existingFeature.Id,
                        PropertyId = newProperty.Id
                    };

                    await _propertyFeaturesRepository.AddAsync(propertyFeature);
                    newProperty.PropertiesFeatures.Add(propertyFeature);
                }
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

                var propertyImage = new PropertyImages
                {
                    ImageId = existingImage.Id,
                    PropertyId = newProperty.Id
                };

                var existingPropertyImage = await _propertyImagesRepository.FirstOrDefaultAsync(pi => pi.PropertyId == newProperty.Id && pi.ImageId == existingImage.Id);
                if (existingPropertyImage == null)
                {
                    await _propertyImagesRepository.AddAsync(propertyImage);
                    newProperty.PropertiesImages.Add(propertyImage);
                }
            }
        }
    }
}