using Microsoft.EntityFrameworkCore;
using api.Data;
using api.Models;
using api.Entensions;
using api.Services.Interfaces;
using Extensions;
using api.Configurations;
using api.Services;

public class Program{
    public static void Main(string[] args){
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        BuilderConfigurations.ConfigureSwagger(builder);

        BuilderConfigurations.ConfigureNewtonsoftJson(builder);

        // Configures Entity Framework with SQL Server using a connection string from configuration.
        builder.Services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseSqlServer(builder.Configuration["ConnectionStrings:DevConnection"]);
        });

        BuilderConfigurations.ConfigureBuilderIdeintity(builder);

        //Dependancy Injection
        builder.Services.RegisterRepositories(typeof(ApplicationUser).Assembly);
        builder.Services.RegisterUserDefinedServices(typeof(ICategoryService).Assembly);


        var app = builder.Build();

        //MIDDLEWARES:

        // Configure the HTTP request pipeline. Only enables Swagger in development mode.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();

            // Seed data from SeedData/json files in development mode.
            using (var scope = app.Services.CreateScope())
            {
                var seeder = scope.ServiceProvider.GetRequiredService<IDataSeederService>();
                seeder.SeedDataAsync().GetAwaiter().GetResult();
            }
        }

        app.UseHttpsRedirection();

        // Configures CORS policy to allow any method, header, and credentials.
        app.UseCors(x => x
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials()
            // .WithOrigins("https://localhost:44351)) // Uncomment and customize for specific allowed origins.
            .SetIsOriginAllowed(origin => true));  // Allows any origin (can be customized).

        app.UseAuthentication();  // Enables JWT Bearer authentication.

        app.UseAuthorization();  // Enables authorization based on policies or roles.

        app.MapControllers();

        app.ApplyMigrations();

        app.Run();
    }
}


