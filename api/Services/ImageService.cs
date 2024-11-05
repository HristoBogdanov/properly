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

        public ImageService(IRepository<Image, Guid> imageRepository)
        {
            _imageRepository = imageRepository;
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

        public async Task<bool> CreateImageAsync(CreateImageDTO createImageDTO)
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
            return true;
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

            await _imageRepository.DeleteAsync(existingImage);
            return true;
        }
    }
}