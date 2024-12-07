using System.Linq.Expressions;
using api.Constants;
using api.Data.Repository.Interfaces;
using api.DTOs.Category;
using api.Models;
using api.Services;
using MockQueryable;
using Moq;

namespace Properly.Services.Tests
{
    [TestFixture]
    public class CategoryServiceTests
    {
        private Mock<IRepository<Category, Guid>> _categoryRepositoryMock;
        private CategoryService _categoryService;

        private List<Category> categoriesData;

        [SetUp]
        public void Setup()
        {
            _categoryRepositoryMock = new Mock<IRepository<Category, Guid>>();
            _categoryService = new CategoryService(_categoryRepositoryMock.Object);

            // Seed data
            categoriesData = new List<Category>
            {
                new Category
                {
                    Id = Guid.Parse("C994999B-02DD-46C2-ABC4-00C4787E629F"),
                    Title = "Category 1",
                    IsDeleted = false,
                    PropertiesCategories = new List<PropertyCategories>()
                },
                new Category
                {
                    Id = Guid.Parse("4571BF2F-DBB3-446C-A92A-07CB77F47ED0"),
                    Title = "Category 2",
                    IsDeleted = false,
                    PropertiesCategories = new List<PropertyCategories>()
                }
            };
        }

        [Test]
        public async Task GetCategoriesAsync_ShouldReturnCategories()
        {
            // Arrange
            var mockQueryable = categoriesData.AsQueryable().BuildMock();
            _categoryRepositoryMock
                .Setup(r => r.GetAllAttached())
                .Returns(mockQueryable);

            // Act
            var result = await _categoryService.GetCategoriesAsync();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count);

            Assert.AreEqual("Category 1", result[0].Title);
            Assert.AreEqual("Category 2", result[1].Title);
        }

        [Test]
        public async Task GetCategoryByIdAsync_ShouldReturnCategory_WhenExists()
        {
            // Arrange
            var categoryId = Guid.Parse("C994999B-02DD-46C2-ABC4-00C4787E629F");
            _categoryRepositoryMock
                .Setup(r => r.FirstOrDefaultAsync(It.IsAny<Expression<Func<Category, bool>>>()))
                .ReturnsAsync(categoriesData.First(c => c.Id == categoryId));

            // Act
            var result = await _categoryService.GetCategoryByIdAsync(categoryId.ToString());

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(categoryId.ToString(), result.Id);
            Assert.AreEqual("Category 1", result.Title);
        }

        [Test]
        public void GetCategoryByIdAsync_ShouldThrowException_WhenNotFound()
        {
            // Arrange
            _categoryRepositoryMock
                .Setup(r => r.FirstOrDefaultAsync(It.IsAny<Expression<Func<Category, bool>>>()))
                .ReturnsAsync((Category)null);

            // Act & Assert
            var exception = Assert.ThrowsAsync<Exception>(async () =>
                await _categoryService.GetCategoryByIdAsync(Guid.NewGuid().ToString()));
            Assert.AreEqual(CategoryErrorMessages.CategoryNotFound, exception.Message);
        }

        [Test]
        public async Task CreateCategoryAsync_ShouldCreateCategory()
        {
            // Arrange
            var newCategoryDto = new CreateCategoryDTO { Title = "New Category" };

            _categoryRepositoryMock
                .Setup(r => r.FirstOrDefaultAsync(It.IsAny<Expression<Func<Category, bool>>>()))
                .ReturnsAsync((Category)null);

            _categoryRepositoryMock
                .Setup(r => r.AddAsync(It.IsAny<Category>()))
                .Callback<Category>(c => categoriesData.Add(c))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _categoryService.CreateCategoryAsync(newCategoryDto);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("New Category", result.Title);
        }

        [Test]
        public void CreateCategoryAsync_ShouldThrowException_WhenCategoryExists()
        {
            // Arrange
            var newCategoryDto = new CreateCategoryDTO { Title = "Category 1" };

            _categoryRepositoryMock
                .Setup(r => r.FirstOrDefaultAsync(It.IsAny<Expression<Func<Category, bool>>>()))
                .ReturnsAsync(categoriesData.First(c => c.Title == "Category 1"));

            // Act & Assert
            var exception = Assert.ThrowsAsync<Exception>(async () =>
                await _categoryService.CreateCategoryAsync(newCategoryDto));
            Assert.AreEqual(CategoryErrorMessages.CategoryExists, exception.Message);
        }
    }
}
