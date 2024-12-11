using System.Linq.Expressions;
using api.Constants;
using api.Data.Repository.Interfaces;
using api.DTOs.Features;
using api.DTOs.Images;
using api.Models;
using api.Services;
using MockQueryable;
using Moq;

namespace Properly.Services.Tests
{
    [TestFixture]
    public class FeatureServiceTests
    {
        private Mock<IRepository<Feature, Guid>> _featureRepositoryMock;
        private Mock<IRepository<Image, Guid>> _imageRepositoryMock;
        private Mock<IRepository<PropertyFeatures, object>> _featuresPropertiesRepositoryMock;
        private FeatureService _featureService;

        private List<Feature> featuresData;

        [SetUp]
        public void Setup()
        {
            _featureRepositoryMock = new Mock<IRepository<Feature, Guid>>();
            _imageRepositoryMock = new Mock<IRepository<Image, Guid>>();
            _featuresPropertiesRepositoryMock = new Mock<IRepository<PropertyFeatures, object>>();
            _featureService = new FeatureService(_featureRepositoryMock.Object, _imageRepositoryMock.Object, _featuresPropertiesRepositoryMock.Object);

            Image image = new Image 
            { 
                Id = Guid.Parse("C994999B-02DD-46C2-AD56-00C4787E629F"),
                Name = "Image 1",
                Path = "/path-to-img" 
            };

            // Seed data
            featuresData = new List<Feature>
            {
                new Feature
                {
                    Id = Guid.Parse("C994999B-02DD-46C2-ABC4-00C4787E629F"),
                    Title = "Feature 1",
                    IsDeleted = false,
                    Image = image
                },
                new Feature
                {
                    Id = Guid.Parse("4571BF2F-DBB3-446C-A92A-07CB77F47ED0"),
                    Title = "Feature 2",
                    IsDeleted = false,
                    Image = image
                }
            };
        }

        [Test]
        public async Task GetFeaturesAsync_ShouldReturnListOfFeatures()
        {
            // Arrange
            var featuresData = new List<Feature>
            {
                new Feature { Id = Guid.NewGuid(), Title = "Feature 1", Image = new Image { Name = "Image 1", Path = "/path/1" }, IsDeleted = false },
                new Feature { Id = Guid.NewGuid(), Title = "Feature 2", Image = new Image { Name = "Image 2", Path = "/path/2" }, IsDeleted = false }
            };

            var mockQueryable = featuresData.AsQueryable().BuildMock();
            _featureRepositoryMock.Setup(r => r.GetAllAttached()).Returns(mockQueryable);

            // Act
            var result = await _featureService.GetFeaturesAsync();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count);
            Assert.AreEqual("Feature 1", result[0].Title);
            Assert.AreEqual("Feature 2", result[1].Title);
        }

        [Test]
        public async Task GetFeatureByIdAsync_ShouldReturnFeature_WhenFeatureExists()
        {
            // Arrange
            var featureId = Guid.NewGuid();
            var featureData = new Feature { Id = featureId, Title = "Feature 1", Image = new Image { Name = "Image 1", Path = "/path/1" }, IsDeleted = false };
            _featureRepositoryMock
                .Setup(r => r.FirstOrDefaultAsync(It.IsAny<Expression<Func<Feature, bool>>>()))
                .ReturnsAsync(featureData);

            // Act
            var result = await _featureService.GetFeatureByIdAsync(featureId.ToString());

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(featureId.ToString(), result.Id);
            Assert.AreEqual("Feature 1", result.Title);
            Assert.AreEqual("Image 1", result.Image.Name);
        }

        [Test]
        public async Task CreateFeatureAsync_ShouldCreateFeature_WhenValidData()
        {
            // Arrange
            var createFeatureDto = new CreateFeatureDTO
            {
                Title = "New Feature",
                Image = new CreateImageDTO { Name = "Image 3", Path = "/path/3" }
            };

            _featureRepositoryMock
                .Setup(r => r.ContainsAsync(It.IsAny<Expression<Func<Feature, bool>>>()))
                .ReturnsAsync(false);  // No existing feature with this title

            _featureRepositoryMock
                .Setup(r => r.AddAsync(It.IsAny<Feature>()))
                .Callback<Feature>(f => featuresData.Add(f)) // Simulate adding the feature
                .Returns(Task.CompletedTask);

            // Act
            var result = await _featureService.CreateFeatureAsync(createFeatureDto);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("New Feature", result.Title);
            Assert.AreEqual("Image 3", result.Image.Name);
        }

        [Test]
        public void CreateFeatureAsync_ShouldThrowException_WhenFeatureAlreadyExists()
        {
            // Arrange
            var createFeatureDto = new CreateFeatureDTO
            {
                Title = "Feature 1", // This title already exists in the featuresData list
                Image = new CreateImageDTO { Name = "Image 1", Path = "/path/1" }
            };

            _featureRepositoryMock
                .Setup(r => r.ContainsAsync(It.IsAny<Expression<Func<Feature, bool>>>()))
                .ReturnsAsync(true);  // Feature already exists

            // Act & Assert
            var exception = Assert.ThrowsAsync<Exception>(async () =>
                await _featureService.CreateFeatureAsync(createFeatureDto));
            Assert.AreEqual(FeatureErrorMessages.FeatureAlreadyExists, exception.Message);
        }

        [Test]
        public async Task UpdateFeatureAsync_ShouldUpdateFeature_WhenFeatureExists()
        {
            // Arrange
            var featureId = Guid.NewGuid();
            var updateFeatureDto = new CreateFeatureDTO
            {
                Title = "Updated Feature",
                Image = new CreateImageDTO { Name = "Updated Image", Path = "/path/updated" }
            };

            var existingFeature = new Feature
            {
                Id = featureId,
                Title = "Old Feature",
                Image = new Image { Name = "Old Image", Path = "/path/old" },
                IsDeleted = false
            };

            _featureRepositoryMock
                .Setup(r => r.FirstOrDefaultAsync(It.IsAny<Expression<Func<Feature, bool>>>()))
                .ReturnsAsync(existingFeature);

            _featureRepositoryMock
                .Setup(r => r.UpdateAsync(It.IsAny<Feature>()))
                .ReturnsAsync(true);

            _imageRepositoryMock
                .Setup(r => r.AddAsync(It.IsAny<Image>()))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _featureService.UpdateFeatureAsync(featureId.ToString(), updateFeatureDto);

            // Assert
            Assert.IsTrue(result);
            Assert.AreEqual("Updated Feature", existingFeature.Title);
            Assert.AreEqual("Updated Image", existingFeature.Image.Name);
        }

        [Test]
        public async Task DeleteFeatureAsync_ShouldDeleteFeature_WhenFeatureExists()
        {
            // Arrange
            var featureId = Guid.NewGuid();
            var featureData = new Feature
            {
                Id = featureId,
                Title = "Feature to delete",
                Image = new Image { Name = "Image to delete", Path = "/path/to/delete" },
                IsDeleted = false
            };

            _featureRepositoryMock
                .Setup(r => r.FirstOrDefaultAsync(It.IsAny<Expression<Func<Feature, bool>>>()))
                .ReturnsAsync(featureData);

            _featureRepositoryMock
                .Setup(r => r.SoftDeleteAsync(It.IsAny<Feature>()))
                .Callback<Feature>(f => f.IsDeleted = true)
                .ReturnsAsync(true);

            _featuresPropertiesRepositoryMock
                .Setup(pc => pc.GetAllAttached())
                .Returns(new List<PropertyFeatures>().AsQueryable().BuildMock());

            _featuresPropertiesRepositoryMock
                .Setup(pc => pc.DeleteAsync(It.IsAny<PropertyFeatures>()))
                .ReturnsAsync(true);

            // Act
            var result = await _featureService.DeleteFeatureAsync(featureId.ToString());

            // Assert
            Assert.IsTrue(result);
            Assert.IsTrue(featureData.IsDeleted);
        }
    }
}
