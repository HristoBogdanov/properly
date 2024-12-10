using System.Linq.Expressions;
using api.Constants;
using api.Data.Repository.Interfaces;
using api.DTOs.Images;
using api.DTOs.Property;
using api.Helpers;
using api.Models;
using api.Services;
using Microsoft.AspNetCore.Identity;
using MockQueryable;
using Moq;

namespace Properly.Services.Tests
{
    [TestFixture]
    public class PropertyServiceTests
    {
        private Mock<IRepository<Property, Guid>> _propertyRepositoryMock;
        private Mock<IRepository<Category, Guid>> _categoryRepositoryMock;
        private Mock<IRepository<Feature, Guid>> _featureRepositoryMock;
        private Mock<IRepository<Image, Guid>> _imageRepositoryMock;
        private Mock<IRepository<PropertyCategories, object>> _propertyCategoriesRepositoryMock;
        private Mock<IRepository<PropertyFeatures, object>> _propertyFeaturesRepositoryMock;
        private Mock<IRepository<PropertyImages, object>> _propertyImagesRepositoryMock;
        private Mock<UserManager<ApplicationUser>> _userManagerMock;
        private PropertyService _propertyService;

        private List<Property> propertiesData;

        [SetUp]
        public void Setup()
        {
            _propertyRepositoryMock = new Mock<IRepository<Property, Guid>>();
            _categoryRepositoryMock = new Mock<IRepository<Category, Guid>>();
            _featureRepositoryMock = new Mock<IRepository<Feature, Guid>>();
            _imageRepositoryMock = new Mock<IRepository<Image, Guid>>();
            _propertyCategoriesRepositoryMock = new Mock<IRepository<PropertyCategories, object>>();
            _propertyFeaturesRepositoryMock = new Mock<IRepository<PropertyFeatures, object>>();
            _propertyImagesRepositoryMock = new Mock<IRepository<PropertyImages, object>>();
            _userManagerMock = new Mock<UserManager<ApplicationUser>>(
                Mock.Of<IUserStore<ApplicationUser>>(), null, null, null, null, null, null, null, null);

            _propertyService = new PropertyService(
                _propertyRepositoryMock.Object,
                _imageRepositoryMock.Object,
                _categoryRepositoryMock.Object,
                _featureRepositoryMock.Object,
                _propertyCategoriesRepositoryMock.Object,
                _propertyFeaturesRepositoryMock.Object,
                _propertyImagesRepositoryMock.Object,
                _userManagerMock.Object);

            // Seed data
            propertiesData = new List<Property>
            {
                new Property
                {
                    Id = Guid.NewGuid(),
                    Title = "Property 1",
                    Description = "Description 1",
                    Address = "Address 1",
                    Price = 100000,
                    ForSale = true,
                    ForRent = false,
                    Bedrooms = 3,
                    Bathrooms = 2,
                    Area = 1200,
                    YearOfConstruction = 2000,
                    IsFurnished = true,
                    OwnerId = Guid.NewGuid(),
                    IsDeleted = false,
                    CreatedAt = DateTime.UtcNow
                },
                new Property
                {
                    Id = Guid.NewGuid(),
                    Title = "Property 2",
                    Description = "Description 2",
                    Address = "Address 2",
                    Price = 200000,
                    ForSale = false,
                    ForRent = true,
                    Bedrooms = 4,
                    Bathrooms = 3,
                    Area = 1500,
                    YearOfConstruction = 2010,
                    IsFurnished = false,
                    OwnerId = Guid.NewGuid(),
                    IsDeleted = false,
                    CreatedAt = DateTime.UtcNow.AddDays(-1)
                }
            };
        }

        [Test]
        public async Task GetPropertiesAsync_ShouldReturnProperties_WhenNoFiltersApplied()
        {
            var mockQueryable = propertiesData.AsQueryable().BuildMock();
            var mockUserQueryable = new List<ApplicationUser>().AsQueryable().BuildMock();

            // Arrange
            _propertyRepositoryMock
                    .Setup(p => p.GetAllAttached())
                    .Returns(mockQueryable);

            _userManagerMock
                    .Setup(u => u.Users)
                    .Returns(mockUserQueryable);

            var queryParams = new PropertyQueryParams();

            // Act
            var result = await _propertyService.GetPropertiesAsync(queryParams);

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result.Properties.Count, Is.EqualTo(2));
        }

        [Test]
        public async Task GetPropertiesAsync_ShouldReturnFilteredProperties_ByPrice()
        {
            // Arrange
            var mockQueryable = propertiesData.AsQueryable().BuildMock();
            _propertyRepositoryMock.Setup(r => r.GetAllAttached()).Returns(mockQueryable);
            var mockUserQueryable = new List<ApplicationUser>().AsQueryable().BuildMock();

            _userManagerMock
                   .Setup(u => u.Users)
                   .Returns(mockUserQueryable);

            var queryParams = new PropertyQueryParams
            {
                minPrice = 150000
            };

            // Act
            var result = await _propertyService.GetPropertiesAsync(queryParams);

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result.Properties.Count, Is.EqualTo(1));
            Assert.That(result.Properties.First().Price, Is.EqualTo(200000));
        }

        [Test]
        public async Task GetPropertiesAsync_ShouldReturnFilteredProperties_ByBedrooms()
        {
            // Arrange
            var mockQueryable = propertiesData.AsQueryable().BuildMock();
            var mockUserQueryable = new List<ApplicationUser>().AsQueryable().BuildMock();

            // Arrange
            _propertyRepositoryMock
                    .Setup(p => p.GetAllAttached())
                    .Returns(mockQueryable);

            _userManagerMock
                    .Setup(u => u.Users)
                    .Returns(mockUserQueryable);

            var queryParams = new PropertyQueryParams
            {
                numberOfBedrooms = 4
            };

            // Act
            var result = await _propertyService.GetPropertiesAsync(queryParams);

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result.Properties.Count, Is.EqualTo(1));
            Assert.That(result.Properties.First().Bedrooms, Is.EqualTo(4));
        }

        [Test]
        public async Task GetPropertiesAsync_ShouldReturnFilteredProperties_ByForSale()
        {
            // Arrange
            var mockQueryable = propertiesData.AsQueryable().BuildMock();
            var mockUserQueryable = new List<ApplicationUser>().AsQueryable().BuildMock();

            // Arrange
            _propertyRepositoryMock
                    .Setup(p => p.GetAllAttached())
                    .Returns(mockQueryable);

            _userManagerMock
                    .Setup(u => u.Users)
                    .Returns(mockUserQueryable);

            var queryParams = new PropertyQueryParams
            {
                forSale = true
            };

            // Act
            var result = await _propertyService.GetPropertiesAsync(queryParams);

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result.Properties.Count, Is.EqualTo(1));
            Assert.IsTrue(result.Properties.First().ForSale);
        }

        [Test]
        public async Task GetPropertiesAsync_ShouldReturnFilteredProperties_ByForRent()
        {
            // Arrange
            var mockQueryable = propertiesData.AsQueryable().BuildMock();
            var mockUserQueryable = new List<ApplicationUser>().AsQueryable().BuildMock();

            // Arrange
            _propertyRepositoryMock
                    .Setup(p => p.GetAllAttached())
                    .Returns(mockQueryable);

            _userManagerMock
                    .Setup(u => u.Users)
                    .Returns(mockUserQueryable);

            var queryParams = new PropertyQueryParams
            {
                forRent = true
            };

            // Act
            var result = await _propertyService.GetPropertiesAsync(queryParams);

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result.Properties.Count, Is.EqualTo(1));
            Assert.IsTrue(result.Properties.First().ForRent);
        }

        [Test]
        public async Task GetPropertiesAsync_ShouldReturnFilteredProperties_ByIsFurnished()
        {
            // Arrange
            var mockQueryable = propertiesData.AsQueryable().BuildMock();
            var mockUserQueryable = new List<ApplicationUser>().AsQueryable().BuildMock();

            // Arrange
            _propertyRepositoryMock
                    .Setup(p => p.GetAllAttached())
                    .Returns(mockQueryable);

            _userManagerMock
                    .Setup(u => u.Users)
                    .Returns(mockUserQueryable);

            var queryParams = new PropertyQueryParams
            {
                isFurnished = true
            };

            // Act
            var result = await _propertyService.GetPropertiesAsync(queryParams);

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result.Properties.Count, Is.EqualTo(1));
            Assert.IsTrue(result.Properties.First().IsFurnished);
        }

        [Test]
        public async Task GetPropertiesAsync_ShouldReturnSortedProperties_ByPriceAscending()
        {
            // Arrange
            var mockQueryable = propertiesData.AsQueryable().BuildMock();
            var mockUserQueryable = new List<ApplicationUser>().AsQueryable().BuildMock();

            // Arrange
            _propertyRepositoryMock
                    .Setup(p => p.GetAllAttached())
                    .Returns(mockQueryable);

            _userManagerMock
                    .Setup(u => u.Users)
                    .Returns(mockUserQueryable);

            var queryParams = new PropertyQueryParams
            {
                sortBy = "price",
                descending = false
            };

            // Act
            var result = await _propertyService.GetPropertiesAsync(queryParams);

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result.Properties.Count, Is.EqualTo(2));
            Assert.That(result.Properties.First().Price, Is.EqualTo(100000));
        }

        [Test]
        public async Task GetPropertiesAsync_ShouldReturnSortedProperties_ByPriceDescending()
        {
            // Arrange
            var mockQueryable = propertiesData.AsQueryable().BuildMock();
            var mockUserQueryable = new List<ApplicationUser>().AsQueryable().BuildMock();

            _propertyRepositoryMock
                    .Setup(p => p.GetAllAttached())
                    .Returns(mockQueryable);

            _userManagerMock
                    .Setup(u => u.Users)
                    .Returns(mockUserQueryable);

            var queryParams = new PropertyQueryParams
            {
                sortBy = "price",
                descending = true
            };

            // Act
            var result = await _propertyService.GetPropertiesAsync(queryParams);

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result.Properties.Count, Is.EqualTo(2));
            Assert.That(result.Properties.First().Price, Is.EqualTo(200000));
        }

        [Test]
        public async Task GetPropertiesAsync_ShouldReturnSortedProperties_ByAreaAscending()
        {
            // Arrange
            var mockQueryable = propertiesData.AsQueryable().BuildMock();
            var mockUserQueryable = new List<ApplicationUser>().AsQueryable().BuildMock();

            _propertyRepositoryMock
                    .Setup(p => p.GetAllAttached())
                    .Returns(mockQueryable);

            _userManagerMock
                    .Setup(u => u.Users)
                    .Returns(mockUserQueryable);

            var queryParams = new PropertyQueryParams
            {
                sortBy = "area",
                descending = false
            };

            // Act
            var result = await _propertyService.GetPropertiesAsync(queryParams);

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result.Properties.Count, Is.EqualTo(2));
            Assert.That(result.Properties.First().Area, Is.EqualTo(1200));
        }

        [Test]
        public async Task GetPropertiesAsync_ShouldReturnSortedProperties_ByAreaDescending()
        {
            // Arrange
            var mockQueryable = propertiesData.AsQueryable().BuildMock();
            var mockUserQueryable = new List<ApplicationUser>().AsQueryable().BuildMock();

            _propertyRepositoryMock
                    .Setup(p => p.GetAllAttached())
                    .Returns(mockQueryable);

            _userManagerMock
                    .Setup(u => u.Users)
                    .Returns(mockUserQueryable);

            var queryParams = new PropertyQueryParams
            {
                sortBy = "area",
                descending = true
            };

            // Act
            var result = await _propertyService.GetPropertiesAsync(queryParams);

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result.Properties.Count, Is.EqualTo(2));
            Assert.That(result.Properties.First().Area, Is.EqualTo(1500));
        }

        [Test]
        public async Task GetPropertiesAsync_ShouldReturnSortedProperties_ByYearOfConstructionAscending()
        {
            // Arrange
            var mockQueryable = propertiesData.AsQueryable().BuildMock();
            var mockUserQueryable = new List<ApplicationUser>().AsQueryable().BuildMock();

            _propertyRepositoryMock
                    .Setup(p => p.GetAllAttached())
                    .Returns(mockQueryable);

            _userManagerMock
                    .Setup(u => u.Users)
                    .Returns(mockUserQueryable);

            var queryParams = new PropertyQueryParams
            {
                sortBy = "yearOfConstruction",
                descending = false
            };

            // Act
            var result = await _propertyService.GetPropertiesAsync(queryParams);

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result.Properties.Count, Is.EqualTo(2));
            Assert.That(result.Properties.First().YearOfConstruction, Is.EqualTo(2000));
        }

        [Test]
        public async Task GetPropertiesAsync_ShouldReturnSortedProperties_ByYearOfConstructionDescending()
        {
            // Arrange
            var mockQueryable = propertiesData.AsQueryable().BuildMock();
            var mockUserQueryable = new List<ApplicationUser>().AsQueryable().BuildMock();

            _propertyRepositoryMock
                    .Setup(p => p.GetAllAttached())
                    .Returns(mockQueryable);

            _userManagerMock
                    .Setup(u => u.Users)
                    .Returns(mockUserQueryable);

            var queryParams = new PropertyQueryParams
            {
                sortBy = "yearOfConstruction",
                descending = true
            };

            // Act
            var result = await _propertyService.GetPropertiesAsync(queryParams);

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result.Properties.Count, Is.EqualTo(2));
            Assert.That(result.Properties.First().YearOfConstruction, Is.EqualTo(2010));
        }

        [Test]
        public async Task GetPropertyByIdAsync_ShouldReturnProperty_WhenPropertyExists()
        {
            // Arrange
            var propertyId = propertiesData.First().Id.ToString();
            var mockQueryable = propertiesData.AsQueryable().BuildMock();
            var mockUserQueryable = new List<ApplicationUser>().AsQueryable().BuildMock();

            _propertyRepositoryMock
                .Setup(p => p.GetAllAttached())
                .Returns(mockQueryable);

            _userManagerMock
                .Setup(u => u.Users)
                .Returns(mockUserQueryable);

            // Act
            var result = await _propertyService.GetPropertyByIdAsync(propertyId);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(propertyId, result.Id);
        }

        [Test]
        public void GetPropertyByIdAsync_ShouldThrowException_WhenPropertyDoesNotExist()
        {
            // Arrange
            var propertyId = Guid.NewGuid().ToString();
            var mockQueryable = propertiesData.AsQueryable().BuildMock();

            _propertyRepositoryMock
                .Setup(p => p.GetAllAttached())
                .Returns(mockQueryable);

            // Act & Assert
            var exception = Assert.ThrowsAsync<Exception>(async () =>
                await _propertyService.GetPropertyByIdAsync(propertyId));
            Assert.AreEqual(PropertiesErrorMessages.PropertyNotFound, exception.Message);
        }

        [Test]
        public async Task CreatePropertyAsync_ShouldCreateProperty_WhenValidData()
        {
            // Arrange
            var createPropertyDto = new CreatePropertyDTO
            {
                Title = "New Property",
                Description = "New Description",
                Address = "New Address",
                Price = 300000,
                ForSale = true,
                ForRent = false,
                Bedrooms = 5,
                Bathrooms = 4,
                Area = 2000,
                YearOfConstruction = 2020,
                IsFurnished = true,
                Categories = new List<string> { "Category 1" },
                Features = new List<string> { "Feature 1" },
                Images = new List<CreateImageDTO> { new CreateImageDTO { Name = "Image 1", Path = "/path/1" } }
            };

            var existingCategory = new Category { Id = Guid.NewGuid(), Title = "Category 1" };
            var existingFeature = new Feature { Id = Guid.NewGuid(), Title = "Feature 1" };
            var existingImage = new Image { Id = Guid.NewGuid(), Name = "Image 1", Path = "/path/1" };

            var user = new ApplicationUser { Id = Guid.NewGuid(), UserName = "User1" };

            _propertyRepositoryMock
                .Setup(r => r.AddAsync(It.IsAny<Property>()))
                .Callback<Property>(p => propertiesData.Add(p))
                .Returns(Task.CompletedTask);

            _userManagerMock
                .Setup(u => u.FindByIdAsync(It.IsAny<string>()))
                .ReturnsAsync(user);

            _propertyCategoriesRepositoryMock
                .Setup(r => r.GetAllAttached())
                .Returns(new List<PropertyCategories>().AsQueryable().BuildMock());

            _propertyCategoriesRepositoryMock
                .Setup(r => r.AddAsync(It.IsAny<PropertyCategories>()))
                .Returns(Task.CompletedTask);

            _propertyFeaturesRepositoryMock
                .Setup(r => r.GetAllAttached())
                .Returns(new List<PropertyFeatures>().AsQueryable().BuildMock());

            _propertyImagesRepositoryMock
                .Setup(r => r.GetAllAttached())
                .Returns(new List<PropertyImages>().AsQueryable().BuildMock());

            _categoryRepositoryMock
                .Setup(r => r.FirstOrDefaultAsync(It.IsAny<Expression<Func<Category, bool>>>()))
                .ReturnsAsync(existingCategory);

            _featureRepositoryMock
                .Setup(r => r.FirstOrDefaultAsync(It.IsAny<Expression<Func<Feature, bool>>>()))
                .ReturnsAsync(existingFeature);

            _imageRepositoryMock
                .Setup(r => r.FirstOrDefaultAsync(It.IsAny<Expression<Func<Image, bool>>>()))
                .ReturnsAsync(existingImage);

            // Act
            var result = await _propertyService.CreatePropertyAsync(createPropertyDto, user.Id.ToString());

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("New Property", result.Title);
            Assert.AreEqual("User1", result.OwnerName);
        }

        [Test]
        public async Task UpdatePropertyAsync_ShouldUpdateProperty_WhenPropertyExists()
        {
            // Arrange
            var propertyId = propertiesData.First().Id.ToString();
            var updatePropertyDto = new CreatePropertyDTO
            {
                Title = "Updated Property",
                Description = "Updated Description",
                Address = "Updated Address",
                Price = 400000,
                ForSale = false,
                ForRent = true,
                Bedrooms = 6,
                Bathrooms = 5,
                Area = 2500,
                YearOfConstruction = 2021,
                IsFurnished = false,
                Categories = new List<string> { "Category 2" },
                Features = new List<string> { "Feature 2" },
                Images = new List<CreateImageDTO> { new CreateImageDTO { Name = "Image 2", Path = "/path/2" } }
            };

            var user = new ApplicationUser { Id = Guid.NewGuid(), UserName = "User2" };

            var existingCategory = new Category { Id = Guid.NewGuid(), Title = "Category 1" };
            var existingFeature = new Feature { Id = Guid.NewGuid(), Title = "Feature 1" };
            var existingImage = new Image { Id = Guid.NewGuid(), Name = "Image 1", Path = "/path/1" };

            _propertyRepositoryMock
                .Setup(r => r.FirstOrDefaultAsync(It.IsAny<Expression<Func<Property, bool>>>()))
                .ReturnsAsync(propertiesData.First());

            _propertyRepositoryMock
                .Setup(r => r.UpdateAsync(It.IsAny<Property>()))
                .ReturnsAsync(true);

            _userManagerMock
                .Setup(u => u.FindByIdAsync(It.IsAny<string>()))
                .ReturnsAsync(user);

            _propertyCategoriesRepositoryMock
                .Setup(r => r.GetAllAttached())
                .Returns(new List<PropertyCategories>().AsQueryable().BuildMock());

            _propertyCategoriesRepositoryMock
                .Setup(r => r.AddAsync(It.IsAny<PropertyCategories>()))
                .Returns(Task.CompletedTask);

            _propertyFeaturesRepositoryMock
                .Setup(r => r.GetAllAttached())
                .Returns(new List<PropertyFeatures>().AsQueryable().BuildMock());

            _propertyImagesRepositoryMock
                .Setup(r => r.GetAllAttached())
                .Returns(new List<PropertyImages>().AsQueryable().BuildMock());

            _categoryRepositoryMock
                .Setup(r => r.FirstOrDefaultAsync(It.IsAny<Expression<Func<Category, bool>>>()))
                .ReturnsAsync(existingCategory);

            _featureRepositoryMock
                .Setup(r => r.FirstOrDefaultAsync(It.IsAny<Expression<Func<Feature, bool>>>()))
                .ReturnsAsync(existingFeature);

            _imageRepositoryMock
                .Setup(r => r.FirstOrDefaultAsync(It.IsAny<Expression<Func<Image, bool>>>()))
                .ReturnsAsync(existingImage);

            // Act
            var result = await _propertyService.UpdatePropertyAsync(propertyId, updatePropertyDto, user.Id.ToString());

            // Assert
            Assert.IsTrue(result);
            Assert.AreEqual("Updated Property", propertiesData.First().Title);
        }

        [Test]
        public async Task AddImageToPropertyAsync_ShouldAddImage_WhenPropertyExists()
        {
            // Arrange
            var propertyId = propertiesData.First().Id.ToString();
            var createImageDto = new CreateImageDTO { Name = "New Image", Path = "/path/new" };

            _propertyRepositoryMock
                .Setup(r => r.FirstOrDefaultAsync(It.IsAny<Expression<Func<Property, bool>>>()))
                .ReturnsAsync(propertiesData.First());

            _imageRepositoryMock
                .Setup(r => r.FirstOrDefaultAsync(It.IsAny<Expression<Func<Image, bool>>>()))
                .ReturnsAsync((Image)null);

            _imageRepositoryMock
                .Setup(r => r.AddAsync(It.IsAny<Image>()))
                .Returns(Task.CompletedTask);

            _propertyImagesRepositoryMock
                .Setup(r => r.AddAsync(It.IsAny<PropertyImages>()))
                .Returns(Task.CompletedTask);

            // Act
            await _propertyService.AddImageToPropertyAsync(propertyId, createImageDto);

            // Assert
            _imageRepositoryMock.Verify(r => r.AddAsync(It.IsAny<Image>()), Times.Once);
            _propertyImagesRepositoryMock.Verify(r => r.AddAsync(It.IsAny<PropertyImages>()), Times.Once);
        }

        [Test]
        public async Task DeletePropertyAsync_ShouldDeleteProperty_WhenPropertyExists()
        {
            // Arrange
            var propertyId = propertiesData.First().Id.ToString();

            _propertyRepositoryMock
                .Setup(r => r.FirstOrDefaultAsync(It.IsAny<Expression<Func<Property, bool>>>()))
                .ReturnsAsync(propertiesData.First());

            _propertyRepositoryMock
                .Setup(r => r.SoftDeleteAsync(It.IsAny<Property>()))
                .ReturnsAsync(true);

            // Act
            var result = await _propertyService.DeletePropertyAsync(propertyId);

            // Assert
            Assert.IsTrue(result);
            _propertyRepositoryMock.Verify(r => r.SoftDeleteAsync(It.IsAny<Property>()), Times.Once);
        }
    }
}
