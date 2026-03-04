using HumanDesign.Infrastructure.Entities;
using Microsoft.AspNetCore.Identity;

namespace HumanDesign.Infrastructure.Persistence.Seed;
public static class UserDataSeeder
{
    public static async Task SeedAsync(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();

        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole<Guid>>>();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<UserEntity>>();

        string[] roles = ["Admin", "Leader", "Agent", "User"];

        // Create roles
        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(new IdentityRole<Guid>(role));
            }
        }

        // Create default Admin
        var adminEmail = "admin@richhd.com";
        var adminUser = await userManager.FindByEmailAsync(adminEmail);

        if (adminUser == null)
        {
            var admin = new UserEntity
            {
                Id = Guid.NewGuid(),
                UserName = adminEmail,
                Email = adminEmail,
                FullName = "System Admin",
                Role = "Admin",
                ReferralCode = "RICHHD",
                EmailConfirmed = true
            };

            var result = await userManager.CreateAsync(admin, "Admin@123!");

            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(admin, "Admin");
            }
        }
    }
}