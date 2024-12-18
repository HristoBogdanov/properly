using api.Data.Repository.Interfaces;
using api.DTOs.Images;
using api.Models;
using api.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using api.Constants;

namespace api.Services
{
    public class ImageService : IImageService
    {
        private readonly IRepository<Image, Guid> _imageRepository;
        private readonly IRepository<PropertyImages, object> _propertyImagesRepository;

        public ImageService(IRepository<Image, Guid> imageRepository,
            IRepository<PropertyImages, object> propertyImagesRepository)
        {
            _imageRepository = imageRepository;
            _propertyImagesRepository = propertyImagesRepository;
        }

        public async Task<List<DisplayImageDTO>> GetImagesAsync()
        {
            var images = await _imageRepository.GetAllAttached()
            .Select(i => new DisplayImageDTO
            {
                Id = i.Id.ToString(),
                Name = i.Name,
                Path = i.Path
            })
            .AsNoTracking()
            .ToListAsync();

            return images;
        }

        public async Task<DisplayImageDTO> CreateImageAsync(CreateImageDTO createImageDTO)
        {
            if(await _imageRepository.ContainsAsync(i => i.Path == createImageDTO.Path))
            {
                throw new Exception(ImageErrorMessages.ImageAlreadyExists);
            }

            var image = new Image
            {
                Name = createImageDTO.Name,
                Path = createImageDTO.Path
            };

            await _imageRepository.AddAsync(image);
            return new DisplayImageDTO
            {
                Id = image.Id.ToString(),
                Name = image.Name,
                Path = image.Path
            };
        }

        public async Task<bool> UpdateImageAsync(string id, CreateImageDTO updateImageDTO)
        {
            Guid idGuid = Guid.Parse(id);
            var existingImage = await _imageRepository.FirstOrDefaultAsync(i => i.Id == idGuid);

            existingImage.Name = updateImageDTO.Name;
            existingImage.Path = updateImageDTO.Path;

            await _imageRepository.UpdateAsync(existingImage);
            return true;
        }

        public async Task<bool> DeleteImageAsync(string id)
        {
            Guid idGuid = Guid.Parse(id);
            var existingImage = await _imageRepository.FirstOrDefaultAsync(i => i.Id == idGuid);

            if(existingImage == null)
            {
                throw new Exception(ImageErrorMessages.ImageNotFound);
            }

            var propertyImages = await _propertyImagesRepository
                .GetAllAttached()
                .Where(pi => pi.ImageId == idGuid)
                .ToListAsync();

            foreach (var propertyImage in propertyImages)
            {
                await _propertyImagesRepository.DeleteAsync(propertyImage);
            }

            await _imageRepository.DeleteAsync(existingImage);
            return true;
        }
    }
}