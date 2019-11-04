using Cocktail_Magician_DB;
using Cocktail_Magician_DB.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cocktail_Magician.Infrastructure.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static void UpdateDatabase(this IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<CMContext>();
                context.Database.Migrate();

                var roleManager = serviceScope.ServiceProvider.GetService<RoleManager<IdentityRole>>();
                var userManager = serviceScope.ServiceProvider.GetService<UserManager<User>>();

                Task.Run(async () =>
                {
                    var memberRole = "Member";

                    var memberRoleExist = await roleManager.RoleExistsAsync(memberRole);

                    if (!memberRoleExist)
                    {
                        await roleManager.CreateAsync(new IdentityRole 
                        { 
                            Name = memberRole 
                        });
                    }

                    var adminRole = "Administrator";

                    var adminRoleExists = await roleManager.RoleExistsAsync(adminRole);

                    if (!adminRoleExists)
                    {
                        await roleManager.CreateAsync(new IdentityRole
                        {
                            Name = adminRole
                        });
                    }

                    var adminUser = await userManager.FindByNameAsync(adminRole);

                    if (adminUser == null)
                    {
                        adminUser = new User
                        {
                            UserName = "admin",
                            Email = "admin@admin.com"
                        };

                        // By default it takes minimum 6 symbols
                        await userManager.CreateAsync(adminUser, "admin");
                        await userManager.AddToRoleAsync(adminUser, adminRole);
                    }
                })
                .GetAwaiter()
                .GetResult();
            }
        }
    }
}
