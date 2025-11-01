using Microsoft.AspNetCore.Identity;
using RestaurantSystem.Domain.Entities;
using RestaurantSystem.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantSystem.Infrastructure.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly UserManager<ApplicationUser> userManager;

        public AuthRepository(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }

        public async Task AddToRoleAsync(ApplicationUser user, string RoleName)
        {
            await userManager.AddToRoleAsync(user,RoleName);
        }

        public async Task<bool> CheckPasswordAsync(ApplicationUser user, string password)
        {
            return await userManager.CheckPasswordAsync(user, password);
        }

        public async Task<ApplicationUser?> FindByEmailAsync(string username)
        {
            return await userManager.FindByEmailAsync(username);
        }

        public async Task<IdentityResult> RegisterAsync(ApplicationUser user, string password)
        {
            var result = await userManager.CreateAsync(user,password);
            if(!result.Succeeded)
                throw new Exception(string.Join(", ", result.Errors.Select(e => e.Description)));
            return result; 
        }

        public async Task<IList<string>> GetRolesAsync(ApplicationUser user)
        {
            return await userManager.GetRolesAsync(user);
        }
    }
}
