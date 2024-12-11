using api.Constants;
using api.Data.Repository.Interfaces;
using api.DTOs.Features;
using api.DTOs.Images;
using api.Models;
using api.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace api.Services
{
    public class FeatureService : IFeatureService
    {
        private readonly IRepository<Feature, Guid> _featureRepository;
        private readonly IRepository<Image, Guid> _imageRepository;
        private readonly IRepository<PropertyFeatures, object> _featuresPropertiesRepository;

        public FeatureService(IRepository<Feature, Guid> featureRepository,
            IRepository<Image, Guid> imageRepository,
            IRepository<PropertyFeatures, object> featuresPropertiesRepository)
        {
            _featureRepository = featureRepository;
            _imageRepository = imageRepository;
            _featuresPropertiesRepository = featuresPropertiesRepository;
        }

        public async Task<List<DisplayFeatureDTO>> GetFeaturesAsync()
        {
            var features = await _featureRepository.GetAllAttached()
            .Where(f => f.IsDeleted == false)
            .Select(f => new DisplayFeatureDTO
            {
                Id = f.Id.ToString(),
                Title = f.Title,
                Image = new CreateImageDTO
                {
                    Name = f.Image.Name,
                    Path = f.Image.Path
                }
            })
            .AsNoTracking()
            .ToListAsync();

            return features;
        }

        public async Task<DisplayFeatureDTO> GetFeatureByIdAsync(string id)
        {
            Guid idGuid = Guid.Parse(id);
            var feature = await _featureRepository.FirstOrDefaultAsync(f => f.Id == idGuid && !f.IsDeleted);

            return new DisplayFeatureDTO
            {
                Id = feature.Id.ToString(),
                Title = feature.Title,
                Image = new CreateImageDTO
                {
                    Name = feature.Image.Name,
                    Path = feature.Image.Path
                }
            };
        }

        public async Task<DisplayFeatureDTO> CreateFeatureAsync(CreateFeatureDTO createFeatureDTO)
        {
            if(await _featureRepository.ContainsAsync(i => i.Title == createFeatureDTO.Title && !i.IsDeleted))
            {
                throw new Exception(FeatureErrorMessages.FeatureAlreadyExists);
            }

            var newImage = new Image
            {
                Name = createFeatureDTO.Image.Name,
                Path = createFeatureDTO.Image.Path
            };

            _imageRepository.Add(newImage);

            var feature = new Feature
            {
                Title = createFeatureDTO.Title,
                ImageId = newImage.Id
            };

            await _featureRepository.AddAsync(feature);

            return new DisplayFeatureDTO {
                Id = feature.Id.ToString(),
                Title = feature.Title,
                Image = new CreateImageDTO
                {
                    Name = newImage.Name,
                    Path = newImage.Path
                }
            };
        }

        public async Task<bool> UpdateFeatureAsync(string id, CreateFeatureDTO updateFeatureDTO)
        {
            Guid idGuid = Guid.Parse(id);
            var existingFeature = await _featureRepository.FirstOrDefaultAsync(f => f.Id == idGuid && !f.IsDeleted);

            if (existingFeature == null)
            {
                throw new Exception(FeatureErrorMessages.FeatureNotFound);
            }

            // Update the Image information
            existingFeature.Title = updateFeatureDTO.Title;

            var newImage = new Image
            {
                Name = updateFeatureDTO.Image.Name,
                Path = updateFeatureDTO.Image.Path
            };

            // If the Image already exists, we should update the existing image
            if (existingFeature.Image != null)
            {
                existingFeature.Image.Name = newImage.Name;
                existingFeature.Image.Path = newImage.Path;
            }
            else
            {
                // Otherwise, create a new image and set the ImageId
                await _imageRepository.AddAsync(newImage);
                existingFeature.ImageId = newImage.Id;
            }

            await _featureRepository.UpdateAsync(existingFeature);

            return true;
        }

        public async Task<bool> DeleteFeatureAsync(string id)
        {
            Guid idGuid = Guid.Parse(id);
            var existingFeature = await _featureRepository.FirstOrDefaultAsync(f => f.Id == idGuid && !f.IsDeleted);

            if (existingFeature == null)
            {
                return false;
            }

            var properties = await _featuresPropertiesRepository
                .GetAllAttached()
                .Where(fp => fp.FeatureId == idGuid)
                .ToListAsync();

            foreach (var property in properties)
            {
                await _featuresPropertiesRepository.SoftDeleteAsync(property);
            }

            await _featureRepository.SoftDeleteAsync(existingFeature);
            return true;
        }
    }
}