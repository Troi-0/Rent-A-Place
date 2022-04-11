namespace RentAPlace.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using RentAPlace.Data.Models;

    public class UserSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            var configurationBuilder = new ConfigurationBuilder();
            configurationBuilder
                .AddJsonFile("appsettings.Production.json");
            var configuration = configurationBuilder.Build();

            if (await userManager.FindByNameAsync("Admin@abv.bg") == null)
            {
                var adminUser = new ApplicationUser()
                {
                    UserName = "Admin@abv.bg",
                    Email = "Admin@abv.bg",
                    EmailConfirmed = true,
                };

                var result = await userManager.CreateAsync(adminUser, configuration["Passwords:Admin"]);
                var dbAdminUser = await userManager.FindByEmailAsync("Admin@abv.bg");
                await userManager.AddToRoleAsync(dbAdminUser, "Administrator");
            }

            if (await userManager.FindByNameAsync("RentalAgentTest@abv.bg") == null)
            {
                var adminUser = new ApplicationUser()
                {
                    UserName = "RentalAgentTest@abv.bg",
                    Email = "RentalAgentTest@abv.bg",
                    EmailConfirmed = true,
                };

                var result = await userManager.CreateAsync(adminUser, configuration["Passwords:RentalAgent"]);
                var dbAdminUser = await userManager.FindByEmailAsync("RentalAgentTest@abv.bg");
                await userManager.AddToRoleAsync(dbAdminUser, "RentalAgent");
            }
        }
    }
}
