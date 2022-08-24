using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using SiotohBloggingPlateform.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiotohBloggingPlateform.DataAccess.Data
{
    public static class DbInitializer
    {
        public static async Task Initialise(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetRequiredService<ApplicationDbContext>();
            var userManager = serviceProvider.GetService<UserManager<User>>();
            var roleManager = serviceProvider.GetService<RoleManager<IdentityRole>>();

            context.Database.EnsureCreated();

            try
            {

                if (!context.Roles.Any())
                {
                    var roles = new List<string> { "User", "Admin" };

                    foreach (var item in roles)
                    {
                        var role = new IdentityRole(item);
                        await roleManager.CreateAsync(role);
                    }
                };

                if (!userManager.Users.Any())
                {
                    var user = new User
                    {
                        Name = "Admin",
                        UserName = "admin@gmail.com",
                        Email = "admin@gmail.com"
                    };

                    var result = await userManager.CreateAsync(user, "Admin84@");

                    if (result.Succeeded)
                    {
                        await userManager.AddToRoleAsync(user, "Admin");
                    }
                }
            }
            catch(Exception ex)
            {

            }
        }
    }
}
