using api.Constants;
using api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace api.Data.Configurations
{
    public class SeedUserRoleData : IEntityTypeConfiguration<IdentityRole<Guid>>, IEntityTypeConfiguration<ApplicationUser>, IEntityTypeConfiguration<IdentityUserRole<Guid>>
    {
        private readonly IConfiguration _configuration;

        public SeedUserRoleData(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private readonly Guid adminRoleGuid = Guid.NewGuid();
        private readonly Guid brokerRoleGuid = Guid.NewGuid();
        private readonly Guid userRoleGuid = Guid.NewGuid();
        private readonly Guid adminUserId = Guid.NewGuid();

        // Seed roles with the generated guids
        public void Configure(EntityTypeBuilder<IdentityRole<Guid>> builder)
        {
            List<IdentityRole<Guid>> roles = new List<IdentityRole<Guid>>
            {
                new IdentityRole<Guid>
                {
                    Id = adminRoleGuid,
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                },
                new IdentityRole<Guid>
                {
                    Id = brokerRoleGuid,
                    Name = "Broker",
                    NormalizedName = "BROKER"
                },
                new IdentityRole<Guid>
                {
                    Id = userRoleGuid,
                    Name = "User",
                    NormalizedName = "USER"
                },
            };
            builder.HasData(roles);
        }

        // Seed the admin user with the generated guid
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            if(_configuration["AdminCredentials:Username"] == null 
            || _configuration["AdminCredentials:Email"] == null
            || _configuration["AdminCredentials:Password"] == null)
            {
                throw new Exception(DataSeederErrorMessages.AddAdminCredentials);
            }

            var adminUser = new ApplicationUser
            {
                Id = adminUserId,
                UserName = _configuration["AdminCredentials:Username"],
                NormalizedUserName = _configuration["AdminCredentials:Username"]!.ToUpper(),
                Email = _configuration["AdminCredentials:Email"],
                NormalizedEmail = _configuration["AdminCredentials:Email"]!.ToUpper(),
                EmailConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString("D")
            };

            var passwordHasher = new PasswordHasher<ApplicationUser>();
            adminUser.PasswordHash = passwordHasher.HashPassword(adminUser, _configuration["AdminCredentials:Password"]!);

            builder.HasData(adminUser);
        }

        // Add the admin role to the admnin user, using their guids
        public void Configure(EntityTypeBuilder<IdentityUserRole<Guid>> builder)
        {
            builder.HasData(
                new IdentityUserRole<Guid>
                {
                    UserId = adminUserId,
                    RoleId = adminRoleGuid
                }
            );
        }
    }
}