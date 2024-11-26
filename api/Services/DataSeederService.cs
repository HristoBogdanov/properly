using System.Text.Json;
using api.Constants;
using api.DTOs.Category;
using api.DTOs.Features;
using api.DTOs.Images;
using api.DTOs.Property;
using api.DTOs.User;
using api.Models;
using api.Services.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace api.Services
{
    public class DataSeederService : IDataSeederService
    {
        private readonly ICategoryService _categoryService;
        private readonly IFeatureService _featureService;
        private readonly IPropertyService _propertyService;
        private readonly IUserService _userService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;

        private readonly string featureJsonDataPath = "SeedData/Features.json";
        private readonly string categoryJsonDataPath = "SeedData/Categories.json";
        private readonly string propertyJsonDataPath = "SeedData/Properties.json";
        private readonly string userJsonDataPath = "SeedData/Users.json";
        private readonly string imagesJsonDataPath = "SeedData/Images.json";

        public DataSeederService(
            ICategoryService categoryService,
            IFeatureService featureService,
            IPropertyService propertyService,
            IUserService userService,
            UserManager<ApplicationUser> userManager,
            IConfiguration configuration)
        {
            _categoryService = categoryService;
            _featureService = featureService;
            _propertyService = propertyService;
            _userService = userService;
            _userManager = userManager;
            _configuration = configuration;
        }

        public async Task SeedDataAsync()
        {
            try
            {
                await SeedCategoriesAsync();
                await SeedFeaturesAsync();
                await SeedUsersAsync();
                await SeedAdminAsync();
                await SeedPropertiesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(DataSeederErrorMessages.ErrorSeedingData + ex.Message);
            }
        }

        private async Task SeedCategoriesAsync()
        {
            var categoriesData = await File.ReadAllTextAsync(categoryJsonDataPath);
            var categories = JsonSerializer.Deserialize<List<CreateCategoryDTO>>(categoriesData);
            if (categories != null)
            {
                foreach (var category in categories)
                {
                    await _categoryService.CreateCategoryAsync(category);
                }
                Console.WriteLine(DataSeederErrorMessages.SuccessSeedingData + "Categories");
            }
            else
            {
                throw new Exception(DataSeederErrorMessages.NoDataInFile + "Categories");
            }
        }

        private async Task SeedFeaturesAsync()
        {
            var featuresData = await File.ReadAllTextAsync(featureJsonDataPath);
            var features = JsonSerializer.Deserialize<List<CreateFeatureDTO>>(featuresData);
            if (features != null)
            {
                foreach (var feature in features)
                {
                    await _featureService.CreateFeatureAsync(feature);
                }
                Console.WriteLine(DataSeederErrorMessages.SuccessSeedingData + "Features");
            }
            else
            {
                throw new Exception(DataSeederErrorMessages.NoDataInFile + "Features");
            }
        }

        private async Task SeedUsersAsync()
        {
            if (_userManager.Users.Count() == 0)
            {
                var usersData = await File.ReadAllTextAsync(userJsonDataPath);
                var users = JsonSerializer.Deserialize<List<RegisterDTO>>(usersData);
                if (users != null)
                {
                    foreach (var user in users)
                    {
                        await _userService.Register(user);
                    }
                    Console.WriteLine(DataSeederErrorMessages.SuccessSeedingData + "Users");
                }
                else
                {
                    throw new Exception(DataSeederErrorMessages.NoDataInFile + "Users");
                }
            }
            else
            {
                Console.WriteLine(DataSeederErrorMessages.CollectionNotEmpty + "Users");
            }
        }

        private async Task SeedAdminAsync()
        {       
                string adminUsername = _configuration["AdminCredentials:Username"]!;
                string adminEmail = _configuration["AdminCredentials:Email"]!;
                string adminPassword = _configuration["AdminCredentials:Password"]!;

                if(adminUsername == null || adminEmail == null || adminPassword == null){
                    throw new ArgumentNullException(DataSeederErrorMessages.AdminCredentialsError);
                }

                RegisterDTO adminDTO = new RegisterDTO(){
                    Username = adminUsername,
                    Email = adminEmail,
                    Password = adminPassword
                };

                await _userService.RegisterAdmin(adminDTO);
                Console.WriteLine(DataSeederErrorMessages.SuccessSeedingData + "Admin user");
        }

        private async Task SeedPropertiesAsync()
        {
            var propertiesData = await File.ReadAllTextAsync(propertyJsonDataPath);
            var properties = JsonSerializer.Deserialize<List<CreatePropertyDTO>>(propertiesData);
            if (properties != null)
            {
                foreach (var property in properties)
                {

                    property.OwnerId = await GetAdminId();
                    property.Categories = await GetRandomCategoriesAsync();
                    property.Features = await GetRandomFeaturesAsync();
                    property.Images = await GetRandomImageAsync();

                    await _propertyService.CreatePropertyAsync(property);
                }
                Console.WriteLine(DataSeederErrorMessages.SuccessSeedingData + "Properties");
            }
            else
            {
                throw new Exception(DataSeederErrorMessages.NoDataInFile + "Properties");
            }
        }

        private async Task<string> GetAdminId()
        {
            var admins = await _userManager.GetUsersInRoleAsync("Admin");
            var admin = admins.FirstOrDefault();
            if(admin == null)
            {
                throw new ArgumentNullException(DataSeederErrorMessages.AdminNotSeeded);
            }
            return admin.Id.ToString();
        }

        private async Task<List<string>> GetRandomCategoriesAsync()
        {
            List<string> categories = new();

            var allCategories = await _categoryService.GetCategoriesAsync();
            var random = new Random();
            int minNumberOfCategories = 2;
            var numberOfCategories = random.Next(minNumberOfCategories, allCategories.Count());

            for(int i = 0; i < numberOfCategories; i++)
            {
                var randomIndex = random.Next(0, categories.Count());
                if(!categories.Any(c => c == allCategories[randomIndex].Title))
                {
                    categories.Add(allCategories[randomIndex].Title);
                }
            }

            return categories;
        }

        private async Task<List<string>> GetRandomFeaturesAsync()
        {
            List<string> features = new();

            var allFeatures = await _featureService.GetFeaturesAsync();
            var random = new Random();
            int minNumberOfFeatures = 5;
            var numberOfFeatures = random.Next(minNumberOfFeatures, allFeatures.Count());


            for(int i = 0; i < numberOfFeatures; i++)
            {
                var randomIndex = random.Next(0, features.Count());
                if(!features.Any(f => f == allFeatures[randomIndex].Title))
                {
                    features.Add(allFeatures[randomIndex].Title);
                }
            }

            return features;
        }

        private async Task<List<CreateImageDTO>> GetRandomImageAsync()
        {
            List<CreateImageDTO> images = new();
            int minNumberOfImages = 2;
            int maxNumberOfImages = 10;

            var allImagesData = await File.ReadAllTextAsync(imagesJsonDataPath);
            var allImages = JsonSerializer.Deserialize<List<CreateImageDTO>>(allImagesData);

            if(allImages != null)
            {
                var random = new Random();
                var numberOfImages = random.Next(minNumberOfImages, maxNumberOfImages + 1);
                
                for(int i = 0; i < numberOfImages; i++)
                {
                    var randomIndex = random.Next(0, allImages.Count());
                    if(!images.Any(i => i.Path == allImages[randomIndex].Path))
                    {
                        images.Add(allImages[randomIndex]);
                    }
                }

                return images;
            }
            else
            {
                throw new Exception(DataSeederErrorMessages.NoDataInFile + "Images");
            }
        }
    }
}