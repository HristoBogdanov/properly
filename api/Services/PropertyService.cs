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
using Microsoft.AspNetCore.Identity;
using System.Security.Cryptography;

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
        private readonly UserManager<ApplicationUser> _userManager;

        public PropertyService(
        IRepository<Property, Guid> propertyRepository,
        IRepository<Image, Guid> imageRepository,
        IRepository<Category, Guid> categoryRepository,
        IRepository<Feature, Guid> featureRepository,
        IRepository<PropertyCategories, object> propertyCategoriesRepository,
        IRepository<PropertyFeatures, object> propertyFeaturesRepository,
        IRepository<PropertyImages, object> propertyImagesRepository,
        UserManager<ApplicationUser> userManager)
        {
            _propertyRepository = propertyRepository;
            _imageRepository = imageRepository;
            _categoryRepository = categoryRepository;
            _featureRepository = featureRepository;
            _propertyCategoriesRepository = propertyCategoriesRepository;
            _propertyFeaturesRepository = propertyFeaturesRepository;
            _propertyImagesRepository = propertyImagesRepository;
            _userManager = userManager;
        }

        public async Task<PropertyPagesResult> GetPropertiesAsync(PropertyQueryParams queryParams)
        {
            var properties = GetDisplayProperties(p => !p.IsDeleted);

            properties = FilterProperties(properties, queryParams);
            properties = SortProperties(properties, queryParams);

            // Calculate the total count before pagination
            int totalCount = await properties.CountAsync();

            properties = PaginateProperties(properties, queryParams);

            var propertyList = await properties.ToListAsync();

            foreach (var property in propertyList)
            {
                var owner = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == Guid.Parse(property.OwnerId));
                property.OwnerName = owner?.UserName ?? "Unknown";
            }

            int Page = queryParams.page == 0 ? 1 : queryParams.page;
            int PerPage = queryParams.perPage == 0 ? 10 : queryParams.perPage;
            int TotalPages = (int)Math.Ceiling((double)totalCount / PerPage);

            PropertyPages pages = new PropertyPages
            {
                Page = Page,
                PerPage = PerPage,
                TotalPages = TotalPages
            };

            return new PropertyPagesResult
            {
                Pages = pages,
                Properties = propertyList
            };
        }

        public async Task<DisplayPropertyDTO> GetPropertyByIdAsync(string id)
        {
            Guid idGuid = Guid.Parse(id);

            var properties = GetDisplayProperties(p => p.Id == idGuid && !p.IsDeleted);

            var property = await properties.FirstOrDefaultAsync();

            if (property == null)
            {
                throw new Exception(PropertiesErrorMessages.PropertyNotFound);
            }

            var owner = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == Guid.Parse(property.OwnerId));
            property.OwnerName = owner?.UserName ?? "Unknown";

            return property;
        }

        public async Task<DisplayPropertyDTO> CreatePropertyAsync(CreatePropertyDTO createPropertyDTO)
        {
            var user = await _userManager.FindByIdAsync(createPropertyDTO.OwnerId);

            if (user == null)
            {
                throw new Exception(UserErrorMessages.UserNotFound);
            }

            var newProperty = new Property();
            MapPropertyFields(newProperty, createPropertyDTO);

            await _propertyRepository.AddAsync(newProperty);
            await AddDataToProperty(createPropertyDTO, newProperty);

            return new DisplayPropertyDTO
            {
                Id = newProperty.Id.ToString(),
                Title = newProperty.Title,
                Description = newProperty.Description,
                Slug = newProperty.Slug,
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
                OwnerName = user.UserName!,
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

            if (existingProperty == null)
            {
                throw new Exception(PropertiesErrorMessages.PropertyNotFound);
            }

            MapPropertyFields(existingProperty, updatePropertyDTO);

            await _propertyRepository.UpdateAsync(existingProperty);
            await AddDataToProperty(updatePropertyDTO, existingProperty);

            return true;
        }

        public async Task AddImageToPropertyAsync(string propertyId, CreateImageDTO image)
        {
            Guid propertyIdGuid = Guid.Parse(propertyId);

            var existingProperty = await _propertyRepository.FirstOrDefaultAsync(p => p.Id == propertyIdGuid && !p.IsDeleted);

            if (existingProperty == null)
            {
                throw new Exception(PropertiesErrorMessages.PropertyNotFound);
            }

            var existingImage = await _imageRepository.FirstOrDefaultAsync(i => i.Name == image.Name && i.Path == image.Path);

            if (existingImage == null)
            {
                existingImage = new Image
                {
                    Name = image.Name,
                    Path = image.Path
                };

                await _imageRepository.AddAsync(existingImage);
            }

            var propertyImage = new PropertyImages
            {
                ImageId = existingImage.Id,
                PropertyId = existingProperty.Id
            };

            await _propertyImagesRepository.AddAsync(propertyImage);
        }

        public async Task<bool> DeletePropertyAsync(string id)
        {
            Guid idGuid = Guid.Parse(id);

            var existingProperty = await _propertyRepository.FirstOrDefaultAsync(p => p.Id == idGuid && !p.IsDeleted);

            if (existingProperty == null)
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
            property.Slug = SlugGenerator.GenerateSlug(propertyDTO.Title);
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
                    Slug = p.Slug,
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
                    }).ToList(),
                    Features = p.PropertiesFeatures.Select(f => new DisplayFeatureDTO
                    {
                        Id = f.FeatureId.ToString(),
                        Title = f.Feature.Title,
                        Image = new CreateImageDTO
                        {
                            Name = f.Feature.Image.Name,
                            Path = f.Feature.Image.Path
                        }
                    }).ToList(),
                    Images = p.PropertiesImages.Select(i => new CreateImageDTO
                    {
                        Name = i.Image.Name,
                        Path = i.Image.Path
                    }).ToList()
                })
                .OrderByDescending(p => p.CreatedAt)
                .AsNoTracking();

            return properties;
        }

        private IQueryable<DisplayPropertyDTO> FilterProperties(IQueryable<DisplayPropertyDTO> properties, PropertyQueryParams queryParams)
        {
            if (!string.IsNullOrEmpty(queryParams.search))
            {
                properties = properties
                .Where(p => p.Title.ToLower().Contains(queryParams.search.ToLower())
                || p.Description.ToLower().Contains(queryParams.search.ToLower())
                || p.Address.ToLower().Contains(queryParams.search.ToLower()));
            }

            if (queryParams.minPrice != null)
            {
                properties = properties
                .Where(p => p.Price >= queryParams.minPrice);
            }

            if (queryParams.maxPrice != null)
            {
                properties = properties
                .Where(p => p.Price <= queryParams.maxPrice);
            }

            if (queryParams.numberOfBedrooms != null)
            {
                properties = properties
                .Where(p => p.Bedrooms == queryParams.numberOfBedrooms);
            }

            if (queryParams.numberOfBathrooms != null)
            {
                properties = properties
                .Where(p => p.Bathrooms == queryParams.numberOfBathrooms);
            }

            if (queryParams.minArea != null)
            {
                properties = properties
                .Where(p => p.Area >= queryParams.minArea);
            }

            if (queryParams.maxArea != null)
            {
                properties = properties
                .Where(p => p.Area <= queryParams.maxArea);
            }

            if (queryParams.minYearOfConstruction != null)
            {
                properties = properties
                .Where(p => p.YearOfConstruction >= queryParams.minYearOfConstruction);
            }

            if (queryParams.maxYearOfConstruction != null)
            {
                properties = properties
                .Where(p => p.YearOfConstruction <= queryParams.maxYearOfConstruction);
            }

            if (queryParams.forSale != null)
            {
                properties = properties
                .Where(p => p.ForSale == queryParams.forSale);
            }

            if (queryParams.forRent != null)
            {
                properties = properties
                .Where(p => p.ForRent == queryParams.forRent);
            }

            if (queryParams.isFurnished != null)
            {
                properties = properties
                .Where(p => p.IsFurnished == queryParams.isFurnished);
            }

            return properties;
        }

        private IQueryable<DisplayPropertyDTO> SortProperties(IQueryable<DisplayPropertyDTO> properties, PropertyQueryParams queryParams)
        {
            if (queryParams.sortBy != null)
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
            // Delete all existing categories
            var existingPropertyCategories = await _propertyCategoriesRepository.GetAllAttached()
                .Where(pc => pc.PropertyId == newProperty.Id)
                .ToListAsync();
            foreach (var existingPropertyCategory in existingPropertyCategories)
            {
                await _propertyCategoriesRepository.DeleteAsync(existingPropertyCategory);
            }

            // Delete all existing features
            var existingPropertyFeatures = await _propertyFeaturesRepository.GetAllAttached()
                .Where(pf => pf.PropertyId == newProperty.Id)
                .ToListAsync();
            foreach (var existingPropertyFeature in existingPropertyFeatures)
            {
                await _propertyFeaturesRepository.DeleteAsync(existingPropertyFeature);
            }

            // Delete all existing images
            var existingPropertyImages = await _propertyImagesRepository.GetAllAttached()
                .Where(pi => pi.PropertyId == newProperty.Id)
                .ToListAsync();
            foreach (var existingPropertyImage in existingPropertyImages)
            {
                await _propertyImagesRepository.DeleteAsync(existingPropertyImage);
            }

            // Adding all categories from the dto to the property and the join table
            newProperty.PropertiesCategories = new List<PropertyCategories>();
            foreach (var category in propertyDTO.Categories)
            {
                var existingCategory = await _categoryRepository.FirstOrDefaultAsync(c => c.Title.ToLower() == category.ToLower());
                if (existingCategory == null)
                {
                    throw new Exception(CategoryErrorMessages.CategoryNotFound);
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
            newProperty.PropertiesFeatures = new List<PropertyFeatures>();
            foreach (var feature in propertyDTO.Features)
            {
                var existingFeature = await _featureRepository.FirstOrDefaultAsync(f => f.Title == feature);
                if (existingFeature == null)
                {
                    throw new Exception(FeatureErrorMessages.FeatureNotFound);
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
            newProperty.PropertiesImages = new List<PropertyImages>();
            foreach (var image in propertyDTO.Images)
            {
                var existingImage = await _imageRepository.FirstOrDefaultAsync(i => i.Name == image.Name && i.Path == image.Path);
                if (existingImage == null)
                {
                    existingImage = new Image
                    {
                        Name = image.Name,
                        Path = image.Path
                    };

                    await _imageRepository.AddAsync(existingImage);
                }

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