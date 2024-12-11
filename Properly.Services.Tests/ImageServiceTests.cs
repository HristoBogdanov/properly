using System.Linq.Expressions;
using api.Constants;
using api.Data.Repository.Interfaces;
using api.DTOs.Images;
using api.Models;
using api.Services;
using MockQueryable;
using Moq;

namespace Properly.Services.Tests
{
    [TestFixture]
    public class ImageServiceTests
    {
        private Mock<IRepository<Image, Guid>> _imageRepositoryMock;
        private Mock<IRepository<PropertyImages, object>> _propertyImagesRepositoryMock;
        private ImageService _imageService;

        private List<Image> imagesData;

        [SetUp]
        public void Setup()
        {
            _imageRepositoryMock = new Mock<IRepository<Image, Guid>>();
            _propertyImagesRepositoryMock = new Mock<IRepository<PropertyImages, object>>();
            _imageService = new ImageService(_imageRepositoryMock.Object, _propertyImagesRepositoryMock.Object);

            // Seed data
            imagesData = new List<Image>
            {
                new Image
                {
                    Id = Guid.Parse("C994999B-02DD-46C2-ABC4-00C4787E629F"),
                    Name = "Image 1",
                    Path = "/path/to/image1"
                },
                new Image
                {
                    Id = Guid.Parse("4571BF2F-DBB3-446C-A92A-07CB77F47ED0"),
                    Name = "Image 2",
                    Path = "/path/to/image2"
                }
            };
        }

        [Test]
        public async Task GetImagesAsync_ShouldReturnImages()
        {
            // Arrange
            var mockQueryable = imagesData.AsQueryable().BuildMock();
            _imageRepositoryMock
                .Setup(r => r.GetAllAttached())
                .Returns(mockQueryable);

            // Act
            var result = await _imageService.GetImagesAsync();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count);
            Assert.AreEqual("Image 1", result[0].Name);
            Assert.AreEqual("Image 2", result[1].Name);
        }

        [Test]
        public async Task CreateImageAsync_ShouldCreateImage()
        {
            // Arrange
            var newImageDto = new CreateImageDTO { Name = "New Image", Path = "/path/to/newimage" };

            _imageRepositoryMock
                .Setup(r => r.ContainsAsync(It.IsAny<Expression<Func<Image, bool>>>()))
                .ReturnsAsync(false);

            _imageRepositoryMock
                .Setup(r => r.AddAsync(It.IsAny<Image>()))
                .Callback<Image>(i => imagesData.Add(i))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _imageService.CreateImageAsync(newImageDto);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("New Image", result.Name);
        }

        [Test]
        public void CreateImageAsync_ShouldThrowException_WhenImageExists()
        {
            // Arrange
            var newImageDto = new CreateImageDTO { Name = "Image 1", Path = "/path/to/image1" };

            _imageRepositoryMock
                .Setup(r => r.ContainsAsync(It.IsAny<Expression<Func<Image, bool>>>()))
                .ReturnsAsync(true);

            // Act & Assert
            var exception = Assert.ThrowsAsync<Exception>(async () =>
                await _imageService.CreateImageAsync(newImageDto));
            Assert.AreEqual(ImageErrorMessages.ImageAlreadyExists, exception.Message);
        }

        [Test]
        public async Task UpdateImageAsync_ShouldUpdateImage_WhenImageExists()
        {
            // Arrange
            var imageId = Guid.Parse("C994999B-02DD-46C2-ABC4-00C4787E629F");
            var updateImageDto = new CreateImageDTO { Name = "Updated Image", Path = "/path/to/updatedimage" };

            _imageRepositoryMock
                .Setup(r => r.FirstOrDefaultAsync(It.IsAny<Expression<Func<Image, bool>>>()))
                .ReturnsAsync(imagesData.First(i => i.Id == imageId));

            _imageRepositoryMock
                .Setup(r => r.UpdateAsync(It.IsAny<Image>()))
                .ReturnsAsync(true);

            // Act
            var result = await _imageService.UpdateImageAsync(imageId.ToString(), updateImageDto);

            // Assert
            Assert.IsTrue(result);
            var updatedImage = imagesData.First(i => i.Id == imageId);
            Assert.AreEqual("Updated Image", updatedImage.Name);
        }

        [Test]
        public async Task DeleteImageAsync_ShouldDeleteImage_WhenImageExists()
        {
            // Arrange
            var imageId = Guid.Parse("C994999B-02DD-46C2-ABC4-00C4787E629F");

            _imageRepositoryMock
                .Setup(r => r.FirstOrDefaultAsync(It.IsAny<Expression<Func<Image, bool>>>()))
                .ReturnsAsync(imagesData.First(i => i.Id == imageId));

            _imageRepositoryMock
                .Setup(r => r.DeleteAsync(It.IsAny<Image>()))
                .ReturnsAsync(true);

            _imageRepositoryMock.Setup(r => r.GetAllAttached()).Returns(imagesData.AsQueryable().BuildMock());

            _propertyImagesRepositoryMock
                .Setup(pc => pc.GetAllAttached())
                .Returns(new List<PropertyImages>().AsQueryable().BuildMock());

            _propertyImagesRepositoryMock
                .Setup(pc => pc.DeleteAsync(It.IsAny<PropertyImages>()))
                .ReturnsAsync(true);

            // Act
            var result = await _imageService.DeleteImageAsync(imageId.ToString());

            // Assert
            Assert.IsTrue(result);
        }
    }
}
