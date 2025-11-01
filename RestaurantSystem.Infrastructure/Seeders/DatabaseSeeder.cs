using Microsoft.AspNetCore.Identity;
using RestaurantSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantSystem.Infrastructure.Seeders
{
    public class DatabaseSeeder
    {
        private readonly UserManager<ApplicationUser> user;
        private readonly RoleManager<IdentityRole> role;

        public DatabaseSeeder(UserManager<ApplicationUser> user , RoleManager<IdentityRole> role)
        {
            this.user = user;
            this.role = role;
        }

        public async Task SeedAsync()
        {

            string[] roles = { "Admin", "Customer" };

            foreach (var item in roles) {

                if (!await role.RoleExistsAsync(item))
                    await role.CreateAsync(new IdentityRole(item));
            }

            var adminEmail = "admin@gmail.com";
            var adminuser = await user.FindByEmailAsync(adminEmail);

            if (adminuser == null) {

                var admin = new ApplicationUser
                {
                    UserName = "admin",
                    Email = adminEmail, 
                    FullName = "System Admin",
                    EmailConfirmed = true,
                };
                var result = await user.CreateAsync(admin , "Admin@123");
                if (result.Succeeded)
                    await user.AddToRoleAsync(admin,"Admin");
                else
                    throw new Exception(string.Join(", ", result.Errors.Select(e => e.Description)));
            

        }

        }

    }
}
