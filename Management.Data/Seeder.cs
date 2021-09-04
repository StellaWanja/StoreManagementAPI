using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Management.Models;
using Microsoft.AspNetCore.Identity;

namespace Management.Data
{
    public class Seeder
    {
        public async static Task Seed(RoleManager<IdentityRole> roleManager, UserManager<AppUser> userManager,  StoreDbContext context)
        {
            await context.Database.EnsureCreatedAsync();
            if (!context.Users.Any())
            {

                List<string> roles = new List<string> { "Admin", "Regular"};

                foreach (var role in roles)
                {
                  await  roleManager.CreateAsync(new IdentityRole { Name = role });
                }


                List<AppUser> users = new List<AppUser>
                {
                    new AppUser
                    {
                        FirstName = "Luke",
                        LastName = "Skywalker",
                        Email = "luke@gmail.com",
                        UserName = "luke"
                    },
                    new AppUser
                    {
                        FirstName = "Rose",
                        LastName = "Lucy",
                        Email = "rose@gmail.com",
                        UserName = "rose"
                    }
                };


                foreach (var user in users)
                {
                   await userManager.CreateAsync(user, "P@ssW0rd");
                    if (user == users[0])
                    {
                        await userManager.AddToRoleAsync(user, "Admin");
                    }
                    else
                    {
                        //must be both regular and admin to handle delete
                        // await userManager.AddToRoleAsync(user, "Admin");
                        await userManager.AddToRoleAsync(user, "Regular");
                    }

                }
            }
        }
    }
}
