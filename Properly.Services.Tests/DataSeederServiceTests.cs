using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using Microsoft.AspNetCore.Identity;
using api.Models;
using api.DTOs.Category;
using api.DTOs.Features;
using api.DTOs.Property;
using api.Services.Interfaces;
using api.Services;
using api.DTOs.User;
using Microsoft.Extensions.Configuration;
using System.Text.Json;
using api.DTOs.Images;

namespace Properly.Services.Tests
{
    [TestFixture]
    public class DataSeederServiceTests
    {
        private Mock<ICategoryService> _categoryServiceMock;
        private Mock<IFeatureService> _featureServiceMock;
        private Mock<IPropertyService> _propertyServiceMock;
        private Mock<IUserService> _userServiceMock;
        private Mock<UserManager<ApplicationUser>> _userManagerMock;
        private Mock<IConfiguration> _configurationMock;

        private DataSeederService _dataSeederService;

        [SetUp]
        public void Setup()
        {
            _categoryServiceMock = new Mock<ICategoryService>();
            _featureServiceMock = new Mock<IFeatureService>();
            _propertyServiceMock = new Mock<IPropertyService>();
            _userServiceMock = new Mock<IUserService>();
            _userManagerMock = new Mock<UserManager<ApplicationUser>>(
                Mock.Of<IUserStore<ApplicationUser>>(), null, null, null, null, null, null, null, null);
            _configurationMock = new Mock<IConfiguration>();

            _dataSeederService = new DataSeederService(
                _categoryServiceMock.Object,
                _featureServiceMock.Object,
                _propertyServiceMock.Object,
                _userServiceMock.Object,
                _userManagerMock.Object,
                _configurationMock.Object);
        }

        [Test]
        public async Task SeedDataAsync_ShouldSeedCategoriesAndFeatures_WhenCalled()
        {
            // Arrange: Set up mock data for Categories and Features
            var categories = new List<CreateCategoryDTO> { new CreateCategoryDTO { Title = "Category 1" } };
            var features = new List<CreateFeatureDTO> { new CreateFeatureDTO { Title = "Feature 1" } };

            _categoryServiceMock.Setup(c => c.CreateCategoryAsync(It.IsAny<CreateCategoryDTO>())).ReturnsAsync(new DisplayCategoryDTO());
            _featureServiceMock.Setup(f => f.CreateFeatureAsync(It.IsAny<CreateFeatureDTO>())).ReturnsAsync(new DisplayFeatureDTO());

            // Simulate that JSON files exist with data
            File.WriteAllText("SeedData/Categories.json", "[{\"Title\": \"Category 1\"}]");
            File.WriteAllText("SeedData/Features.json", "[{\"Title\": \"Feature 1\"}]");

            // Act: Call SeedDataAsync
            await _dataSeederService.SeedDataAsync();

            // Assert: Ensure the seed methods were called
            _categoryServiceMock.Verify(c => c.CreateCategoryAsync(It.IsAny<CreateCategoryDTO>()), Times.Once);
            _featureServiceMock.Verify(f => f.CreateFeatureAsync(It.IsAny<CreateFeatureDTO>()), Times.Once);
        }

        [Test]
        public async Task SeedAdminAsync_ShouldCreateAdminUser_WhenCalled()
        {
            // Arrange: Set up mock data for Admin creation
            var adminUsername = "admin";
            var adminEmail = "admin@example.com";
            var adminPassword = "AdminPassword123";
            _configurationMock.Setup(c => c["AdminCredentials:Username"]).Returns(adminUsername);
            _configurationMock.Setup(c => c["AdminCredentials:Email"]).Returns(adminEmail);
            _configurationMock.Setup(c => c["AdminCredentials:Password"]).Returns(adminPassword);

            // Mock user service
            _userServiceMock.Setup(u => u.RegisterAdmin(It.IsAny<RegisterDTO>())).ReturnsAsync(new NewUserDTO());

            // Act: Call SeedAdminAsync
            await _dataSeederService.SeedAdminAsync();

            // Assert: Ensure RegisterAdmin was called
            _userServiceMock.Verify(u => u.RegisterAdmin(It.IsAny<RegisterDTO>()), Times.Once);
        }

        [Test]
        public async Task SeedUsersAsync_ShouldNotSeedUsers_WhenUsersAlreadyExist()
        {
            // Arrange: Mock UserManager to simulate existing users
            var users = new List<ApplicationUser> { new ApplicationUser() };
            _userManagerMock.Setup(u => u.Users).Returns(users.AsQueryable());

            // Act: Call SeedUsersAsync
            await _dataSeederService.SeedUsersAsync();

            // Assert: Ensure no users are registered
            _userServiceMock.Verify(u => u.Register(It.IsAny<RegisterDTO>()), Times.Never);
        }

        [Test]
        public async Task SeedPropertiesAsync_ShouldThrowException_WhenPropertyFileIsEmpty()
        {
            // Arrange: Simulate empty property JSON file
            File.WriteAllText("SeedData/Properties.json", "");

            // Act & Assert: Ensure exception is thrown when property file is empty
            var ex = Assert.ThrowsAsync<JsonException>(async () => await _dataSeederService.SeedPropertiesAsync());
        }
    }
}
