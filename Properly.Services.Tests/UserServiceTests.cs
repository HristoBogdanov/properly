using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Constants;
using api.Data.Repository.Interfaces;
using api.DTOs.User;
using api.Interfaces;
using api.Models;
using api.Services;
using api.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using MockQueryable;
using Moq;
using NUnit.Framework;

namespace Properly.Services.Tests
{
    [TestFixture]
    public class UserServiceTests
    {
        private Mock<UserManager<ApplicationUser>> _userManagerMock;
        private Mock<SignInManager<ApplicationUser>> _signInManagerMock;
        private Mock<ITokenService> _tokenServiceMock;
        private Mock<IRepository<Property, Guid>> _propertyRepositoryMock;
        private UserService _userService;

        [SetUp]
        public void Setup()
        {
            _userManagerMock = new Mock<UserManager<ApplicationUser>>(
                Mock.Of<IUserStore<ApplicationUser>>(), null, null, null, null, null, null, null, null);
            _signInManagerMock = new Mock<SignInManager<ApplicationUser>>(
                _userManagerMock.Object, Mock.Of<IHttpContextAccessor>(), Mock.Of<IUserClaimsPrincipalFactory<ApplicationUser>>(), null, null, null, null);
            _tokenServiceMock = new Mock<ITokenService>();
            _propertyRepositoryMock = new Mock<IRepository<Property, Guid>>();

            _userService = new UserService(
                _userManagerMock.Object,
                _signInManagerMock.Object,
                _tokenServiceMock.Object,
                _propertyRepositoryMock.Object);
        }

        [Test]
        public async Task GetUsersAsync_ShouldReturnListOfUsers()
        {
            // Arrange
            var users = new List<ApplicationUser>
            {
                new ApplicationUser { Id = Guid.NewGuid(), UserName = "user1", Email = "user1@example.com" },
                new ApplicationUser { Id = Guid.NewGuid(), UserName = "user2", Email = "user2@example.com" }
            }.AsQueryable().BuildMock();

            _userManagerMock.Setup(u => u.Users).Returns(users);
            _userManagerMock.Setup(u => u.FindByIdAsync(It.IsAny<string>())).ReturnsAsync((string id) => users.FirstOrDefault(u => u.Id.ToString() == id));
            _userManagerMock.Setup(u => u.GetRolesAsync(It.IsAny<ApplicationUser>())).ReturnsAsync(new List<string> { "User" });
            _propertyRepositoryMock.Setup(p => p.GetAll()).Returns(new List<Property>().AsQueryable());

            // Act
            var result = await _userService.GetUsersAsync();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count);
            Assert.AreEqual("user1", result[0].Username);
            Assert.AreEqual("user2", result[1].Username);
        }

        [Test]
        public async Task GetUserByIdAsync_ShouldReturnUser_WhenUserExists()
        {
            // Arrange
            var userId = Guid.NewGuid().ToString();
            var user = new ApplicationUser { Id = Guid.Parse(userId), UserName = "user1", Email = "user1@example.com" };

            _userManagerMock.Setup(u => u.Users).Returns(new List<ApplicationUser> { user }.AsQueryable().BuildMock());
            _userManagerMock.Setup(u => u.GetRolesAsync(user)).ReturnsAsync(new List<string> { "User" });

            // Act
            var result = await _userService.GetUserByIdAsync(userId);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(userId, result.Id);
            Assert.AreEqual("user1", result.Username);
        }

        [Test]
        public void GetUserByIdAsync_ShouldThrowException_WhenUserNotFound()
        {
            // Arrange
            var userId = Guid.NewGuid().ToString();

            _userManagerMock.Setup(u => u.Users).Returns(new List<ApplicationUser>().AsQueryable().BuildMock());

            // Act & Assert
            var ex = Assert.ThrowsAsync<Exception>(async () => await _userService.GetUserByIdAsync(userId));
            Assert.AreEqual(UserErrorMessages.UserNotFound, ex.Message);
        }

        [Test]
        public async Task Login_ShouldReturnNewUserDTO_WhenCredentialsAreValid()
        {
            // Arrange
            var loginDto = new LoginDTO { Username = "user1", Password = "password" };
            var user = new ApplicationUser { Id = Guid.NewGuid(), UserName = "user1", Email = "user1@example.com" };

            _userManagerMock.Setup(u => u.Users).Returns(new List<ApplicationUser> { user }.AsQueryable().BuildMock());
            _signInManagerMock.Setup(s => s.CheckPasswordSignInAsync(user, loginDto.Password, false)).ReturnsAsync(SignInResult.Success);
            _userManagerMock.Setup(u => u.GetRolesAsync(user)).ReturnsAsync(new List<string> { "User" });
            _tokenServiceMock.Setup(t => t.CreateToken(user)).Returns("token");

            // Act
            var result = await _userService.Login(loginDto);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("user1", result.UserName);
            Assert.AreEqual("user1@example.com", result.Email);
            Assert.AreEqual("token", result.Token);
            Assert.AreEqual("User", result.Role);
        }

        [Test]
        public void Login_ShouldThrowUnauthorizedAccessException_WhenCredentialsAreInvalid()
        {
            // Arrange
            var loginDto = new LoginDTO { Username = "user1", Password = "wrongpassword" };
            var user = new ApplicationUser { Id = Guid.NewGuid(), UserName = "user1", Email = "user1@example.com" };

            _userManagerMock.Setup(u => u.Users).Returns(new List<ApplicationUser> { user }.AsQueryable().BuildMock());
            _signInManagerMock.Setup(s => s.CheckPasswordSignInAsync(user, loginDto.Password, false)).ReturnsAsync(SignInResult.Failed);

            // Act & Assert
            var ex = Assert.ThrowsAsync<UnauthorizedAccessException>(async () => await _userService.Login(loginDto));
            Assert.AreEqual(UserErrorMessages.InvalidUsernameOrPassword, ex.Message);
        }

        [Test]
        public async Task Register_ShouldReturnNewUserDTO_WhenRegistrationIsSuccessful()
        {
            // Arrange
            var registerDto = new RegisterDTO { Username = "user1", Email = "user1@example.com", Password = "password" };
            var user = new ApplicationUser { Id = Guid.NewGuid(), UserName = "user1", Email = "user1@example.com" };

            _userManagerMock.Setup(u => u.CreateAsync(It.IsAny<ApplicationUser>(), registerDto.Password)).ReturnsAsync(IdentityResult.Success);
            _userManagerMock.Setup(u => u.AddToRoleAsync(It.IsAny<ApplicationUser>(), "User")).ReturnsAsync(IdentityResult.Success);
            _tokenServiceMock.Setup(t => t.CreateToken(It.IsAny<ApplicationUser>())).Returns("token");

            // Act
            var result = await _userService.Register(registerDto);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("user1", result.UserName);
            Assert.AreEqual("user1@example.com", result.Email);
            Assert.AreEqual("token", result.Token);
            Assert.AreEqual("User", result.Role);
        }

        [Test]
        public void Register_ShouldThrowException_WhenRegistrationFails()
        {
            // Arrange
            var registerDto = new RegisterDTO { Username = "user1", Email = "user1@example.com", Password = "password" };

            _userManagerMock.Setup(u => u.CreateAsync(It.IsAny<ApplicationUser>(), registerDto.Password)).ReturnsAsync(IdentityResult.Failed(new IdentityError { Description = "Error" }));

            // Act & Assert
            var ex = Assert.ThrowsAsync<Exception>(async () => await _userService.Register(registerDto));
            Assert.AreEqual("Error", ex.Message);
        }

        [Test]
        public async Task DeleteUser_ShouldReturnTrue_WhenUserIsDeleted()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var user = new ApplicationUser { Id = userId, UserName = "user1", Email = "user1@example.com" };

            _userManagerMock.Setup(u => u.FindByIdAsync(userId.ToString())).ReturnsAsync(user);
            _propertyRepositoryMock.Setup(p => p.GetAll()).Returns(new List<Property>().AsQueryable());
            _userManagerMock.Setup(u => u.DeleteAsync(user)).ReturnsAsync(IdentityResult.Success);

            // Act
            var result = await _userService.DeleteUser(userId);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void DeleteUser_ShouldThrowException_WhenUserNotFound()
        {
            // Arrange
            var userId = Guid.NewGuid();

            _userManagerMock.Setup(u => u.FindByIdAsync(userId.ToString())).ReturnsAsync((ApplicationUser)null);

            // Act & Assert
            var ex = Assert.ThrowsAsync<Exception>(async () => await _userService.DeleteUser(userId));
            Assert.AreEqual(UserErrorMessages.UserNotFound, ex.Message);
        }
    }
}
