using api.Data;
using api.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace api.Configurations
{
    public static class BuilderConfigurations
    {
        public static void ConfigureSwagger(WebApplicationBuilder builder)
        {
            
            // Swagger configuration
            builder.Services.AddSwaggerGen(option =>
            {
            option.SwaggerDoc("v1", new OpenApiInfo { Title = "Properly API", Version = "v1" });

            // Adds JWT Bearer security definition to Swagger
            option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,  // JWT token is sent in the request headers.
                Description = "Please enter a valid token",  // Description shown in the Swagger UI.
                Name = "Authorization",  // Name of the header where the token is passed.
                Type = SecuritySchemeType.Http,  // Type of security scheme (HTTP-based in this case).
                BearerFormat = "JWT",  // Format is JWT.
                Scheme = "Bearer"  // Specifies Bearer as the token scheme.
            });

                // Sets the security requirements for accessing endpoints in Swagger UI
                option.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,  // Reference to the security scheme.
                                Id = "Bearer"  // ID of the Bearer security scheme.
                            }
                        },
                        new string[] { }  // An empty array allows all endpoints to require the Bearer token.
                    }
                });
            });
        }

        public static void ConfigureBuilderIdeintity(WebApplicationBuilder builder)
        {
            // Adds and configures Identity services (for user authentication and authorization) with password rules.
            builder.Services.AddIdentity<ApplicationUser, IdentityRole<Guid>>(options =>
            {
                ConfigureIdentity(builder, options);  // Configures password rules.
            })
            .AddRoles<IdentityRole<Guid>>()  // Adds roles to the identity system.
            .AddEntityFrameworkStores<ApplicationDbContext>();  // Uses Entity Framework to store identity data.

            // Configures authentication schemes, specifically using JWT Bearer tokens.
            builder.Services.AddAuthentication(options =>
            {
                // Sets JWT as the default authentication scheme for various operations.
                options.DefaultAuthenticateScheme =
                options.DefaultChallengeScheme =
                options.DefaultForbidScheme =
                options.DefaultScheme =
                options.DefaultSignInScheme =
                options.DefaultSignOutScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                // Configures JWT token validation parameters.
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,  // Validate the token issuer.
                    ValidIssuer = builder.Configuration["JwtSettings:Issuer"],  // Gets the issuer from configuration.
                    ValidateAudience = true,  // Validate the token audience.
                    ValidAudience = builder.Configuration["JwtSettings:Audience"],  // Gets the audience from configuration.
                    ValidateIssuerSigningKey = true,  // Ensures the token has a valid signing key.
                    IssuerSigningKey = new SymmetricSecurityKey(  // Signing key used to verify the token.
                        System.Text.Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:DevSigningKey"]!)
                    )
                };
            });
        }

        public static void ConfigureNewtonsoftJson(WebApplicationBuilder builder)
        {
            // Adds Newtonsoft JSON serialization settings to avoid reference loop issues.
            builder.Services.AddControllers().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;  // Ignore reference loops in JSON serialization.
            });
        }

        private static void ConfigureIdentity(WebApplicationBuilder builder, IdentityOptions options)
        {
            options.Password.RequireDigit = builder.Configuration.GetValue<bool>("PasswordSettings:RequireDigit");
            options.Password.RequireLowercase = builder.Configuration.GetValue<bool>("PasswordSettings:RequireLowercase");
            options.Password.RequireUppercase = builder.Configuration.GetValue<bool>("PasswordSettings:RequireUppercase");
            options.Password.RequireNonAlphanumeric = builder.Configuration.GetValue<bool>("PasswordSettings:RequireNonAlphanumeric");
            options.Password.RequiredLength = builder.Configuration.GetValue<int>("PasswordSettings:RequiredLength");
        }
    }
}