using System.Text.Json;
using api.Constants;
using api.Data.Repository.Interfaces;
using api.DTOs.User;
using api.Models;
using api.Services.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace api.Services
{
    public class DataSeederService : IDataSeederService
    {
        private readonly IRepository<Category, Guid> _categoryRepository;
        private readonly IRepository<Feature, Guid> _featureRepository;
        private readonly IRepository<Property, Guid> _propertyRepository;
        private readonly UserManager<ApplicationUser> _userManager;

        private readonly string featureJsonDataPath = "SeedData/Features.json";
        private readonly string categoryJsonDataPath = "SeedData/Categories.json";
        private readonly string propertyJsonDataPath = "SeedData/Properties.json";
        private readonly string userJsonDataPath = "SeedData/Users.json";
        private readonly string brokerJsonDataPath = "SeedData/Brokers.json";

    public DataSeederService(
    IRepository<Category, Guid> categoryRepository,
    IRepository<Feature, Guid> featureRepository,
    IRepository<Property, Guid> propertyRepository,
    UserManager<ApplicationUser> userManager)
    {
        _categoryRepository = categoryRepository;
        _featureRepository = featureRepository;
        _propertyRepository = propertyRepository;
        _userManager = userManager;
    }

    public async Task SeedDataAsync()
    {
        try
        {
            await SeedCategoriesAsync();
            await SeedFeaturesAsync();
            await SeedUsersAsync();
            await SeedBrokersAsync();
            await SeedPropertiesAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine(DataSeederErrorMessages.ErrorSeedingData + ex.Message);
        }
    }

    private async Task SeedCategoriesAsync()
    {
        var existingCategories = await _categoryRepository.GetAllAsync();
        if (existingCategories.Count() == 0)
        {
            var categoriesData = await File.ReadAllTextAsync(categoryJsonDataPath);
            var categories = JsonSerializer.Deserialize<List<Category>>(categoriesData);
            if (categories != null)
            {
                await _categoryRepository.AddRangeAsync(categories);
            }
            else 
            {
                throw new Exception(DataSeederErrorMessages.NoDataInFile + "Categories");
            }

        }
        else 
        {
            Console.WriteLine(DataSeederErrorMessages.CollectionNotEmpty + "Categories");
        }

        Console.WriteLine(DataSeederErrorMessages.SuccessSeedingData + "Categories");
    }

    private async Task SeedFeaturesAsync()
    {
        var existingFeatures = await _featureRepository.GetAllAsync();
        if (existingFeatures.Count() == 0)
        {
            var featuresData = await File.ReadAllTextAsync(featureJsonDataPath);
            var features = JsonSerializer.Deserialize<List<Feature>>(featuresData);
            if (features != null)
            {
                await _featureRepository.AddRangeAsync(features);
            }
            else 
            {
                throw new Exception(DataSeederErrorMessages.NoDataInFile + "Features");
            }
        }
        else 
        {
            Console.WriteLine(DataSeederErrorMessages.CollectionNotEmpty + "Features");
        }

        Console.WriteLine(DataSeederErrorMessages.SuccessSeedingData + "Features");
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
                        var appUser = new ApplicationUser
                        {
                            UserName = user.Username,
                            Email = user.Email
                        };

                        var result = await _userManager.CreateAsync(appUser, user.Password);
                        if (result.Succeeded)
                        {
                            await _userManager.AddToRoleAsync(appUser, "User");
                        }
                    }
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

            Console.WriteLine(DataSeederErrorMessages.SuccessSeedingData + "Users");
        }

        private async Task SeedBrokersAsync()
        {
            var brokersData = await File.ReadAllTextAsync(brokerJsonDataPath);
            var brokers = JsonSerializer.Deserialize<List<RegisterDTO>>(brokersData);
            if (brokers != null)
            {
                foreach (var broker in brokers)
                {
                    var appUser = new ApplicationUser
                    {
                        UserName = broker.Username,
                        Email = broker.Email
                    };

                    var result = await _userManager.CreateAsync(appUser, broker.Password);
                    if (result.Succeeded)
                    {
                        await _userManager.AddToRoleAsync(appUser, "Broker");
                    }
                }
            }
            else
            {
                throw new Exception(DataSeederErrorMessages.NoDataInFile + "Brokers");
            }
        }

        private async Task SeedPropertiesAsync()
        {
            var existingProperties = await _propertyRepository.GetAllAsync();
            if (existingProperties.Count() == 0)
            {
                var propertiesData = await File.ReadAllTextAsync(propertyJsonDataPath);

                var properties = JsonSerializer.Deserialize<List<Property>>(propertiesData);
                if (properties != null)
                {
                    foreach (var property in properties)
                    {
                        property.OwnerId = await GetRandomBrokerIdAsync();
                    }
                    await _propertyRepository.AddRangeAsync(properties);
                }
                else
                {
                    throw new Exception(DataSeederErrorMessages.NoDataInFile + "Properties");
                }
            }
            else 
            {
                Console.WriteLine(DataSeederErrorMessages.CollectionNotEmpty + "Properties");
            }

            Console.WriteLine(DataSeederErrorMessages.SuccessSeedingData + "Properties");
        }

        private async Task<Guid> GetRandomBrokerIdAsync()
        {
            var brokers = await _userManager.GetUsersInRoleAsync("Broker");
            var random = new Random();
            var randomIndex = random.Next(0, brokers.Count());
            return brokers[randomIndex].Id;
        }
    }
}