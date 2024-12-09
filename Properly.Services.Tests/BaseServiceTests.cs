using api.Services;

namespace Properly.Services.Tests
{
    [TestFixture]
    public class BaseServiceTests
    {
        private BaseService _baseService;

        [SetUp]
        public void Setup()
        {
            _baseService = new BaseService();
        }

        [Test]
        public void IsGuidValid_ShouldReturnFalse_WhenIdIsNull()
        {
            // Arrange
            string? id = null;
            Guid parsedGuid = Guid.Empty;

            // Act
            var result = _baseService.IsGuidValid(id, ref parsedGuid);

            // Assert
            Assert.IsFalse(result);
            Assert.AreEqual(Guid.Empty, parsedGuid);  // parsedGuid should remain Guid.Empty
        }

        [Test]
        public void IsGuidValid_ShouldReturnFalse_WhenIdIsEmpty()
        {
            // Arrange
            string id = string.Empty;
            Guid parsedGuid = Guid.Empty;

            // Act
            var result = _baseService.IsGuidValid(id, ref parsedGuid);

            // Assert
            Assert.IsFalse(result);
            Assert.AreEqual(Guid.Empty, parsedGuid);  // parsedGuid should remain Guid.Empty
        }

        [Test]
        public void IsGuidValid_ShouldReturnTrue_WhenIdIsValidGuid()
        {
            // Arrange
            string id = "c994999b-02dd-46c2-abc4-00c4787e629f";  // valid GUID
            Guid parsedGuid = Guid.Empty;

            // Act
            var result = _baseService.IsGuidValid(id, ref parsedGuid);

            // Assert
            Assert.IsTrue(result);
            Assert.AreEqual(Guid.Parse(id), parsedGuid);  // parsedGuid should be the parsed GUID
        }

        [Test]
        public void IsGuidValid_ShouldReturnFalse_WhenIdIsInvalidGuid()
        {
            // Arrange
            string id = "invalid-guid";  // invalid GUID
            Guid parsedGuid = Guid.Empty;

            // Act
            var result = _baseService.IsGuidValid(id, ref parsedGuid);

            // Assert
            Assert.IsFalse(result);
            Assert.AreEqual(Guid.Empty, parsedGuid);  // parsedGuid should remain Guid.Empty
        }
    }
}
