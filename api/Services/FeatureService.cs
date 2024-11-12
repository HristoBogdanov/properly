using api.Constants;
using api.Data.Repository.Interfaces;
using api.DTOs.Features;
using api.DTOs.Images;
using api.Models;
using api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Services
{
    public class FeatureService : IFeatureService
    {
        private readonly IRepository<Feature, Guid> _featureRepository;
        private readonly IRepository<Image, Guid> _imageRepository;

        public FeatureService(IRepository<Feature, Guid> featureRepository, IRepository<Image, Guid> imageRepository)
        {
            _featureRepository = featureRepository;
            _imageRepository = imageRepository;
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

            var newImage = new Image
            {
                Name = updateFeatureDTO.Image.Name,
                Path = updateFeatureDTO.Image.Path
            };

            await _imageRepository.AddAsync(newImage);

            existingFeature.Title = updateFeatureDTO.Title;
            existingFeature.ImageId = newImage.Id;

            await _featureRepository.UpdateAsync(existingFeature);

            return true;
        }

        public async Task<bool> DeleteFeatureAsync(string id)
        {
            Guid idGuid = Guid.Parse(id);
            var existingFeature = await _featureRepository.FirstOrDefaultAsync(f => f.Id == idGuid && !f.IsDeleted);

            await _featureRepository.SoftDeleteAsync(existingFeature);
            return true;
        }
    }
}