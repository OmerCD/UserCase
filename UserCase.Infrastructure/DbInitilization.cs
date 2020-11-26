using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Npgsql;
using UserCase.Core.Entities;

namespace UserCase.Infrastructure
{
    public static class DbInitilization
    {
        public static async Task SeedUsers(UserManager<User> userManager)
        {
            var adminEmail = "admin@admin.com";
            if (await userManager.FindByEmailAsync(adminEmail) == null)
            {
                var adminUser = new User()
                {
                    UserName = "Admin",
                    Name = "Admin",
                    CompanyId =1, 
                    Email = adminEmail
                };
                var result = await userManager.CreateAsync(adminUser, "Admin123!");
                if (!result.Succeeded)
                {
                    throw new Exception("Error while creating Admin user");
                }
            }
        }
    }
}