using Microsoft.AspNetCore.Identity;
using RestaurantSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantSystem.Domain.Interfaces
{
    public interface IAuthRepository
    {
        Task<ApplicationUser?> FindByEmailAsync(string username);
        Task<bool> CheckPasswordAsync( ApplicationUser user, string password);
        Task<IdentityResult> RegisterAsync(ApplicationUser user , string password);

        Task AddToRoleAsync(ApplicationUser user, string RoleName);
        Task<IList<string>> GetRolesAsync(ApplicationUser user);


    }
}
