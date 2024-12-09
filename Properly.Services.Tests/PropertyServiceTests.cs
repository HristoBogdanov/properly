using System.Linq.Expressions;
using api.Data.Repository.Interfaces;
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
    }
}
