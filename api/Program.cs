using Microsoft.EntityFrameworkCore;
using api.Data;
using api.Models;
using api.Entensions;
using api.Services.Interfaces;
using Extensions;
using api.Configurations;
using api.Constants;

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

        app.UseHttpsRedirection();

        // Configures CORS policy to allow any method, header, and credentials.
        var frontendUrl = builder.Configuration["FrontendUrl"];
        if (string.IsNullOrEmpty(frontendUrl))
        {
            throw new Exception(MissingConfigurationMessages.MissingFEUrl);
        }

        app.UseCors(x => x
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials()
            .WithOrigins(frontendUrl));

        app.UseAuthentication();  // Enables JWT Bearer authentication.

        app.UseAuthorization();  // Enables authorization based on policies or roles.

        app.MapControllers();

        app.ApplyMigrations();
        
        if (app.Environment.IsDevelopment())
        {
            // Only enables Swagger in development mode.
            app.UseSwagger();
            app.UseSwaggerUI();

            // Only seed data in development mode.
            app.SeedData();
        }

        app.Run();
    }
}


