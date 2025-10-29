using RestaurantSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantSystem.Domain.Interfaces
{
    public interface IMenuItemRepository
    {

        Task<IEnumerable<MenuItem>> GetAllAsync();
        Task<MenuItem?> GetByIdAsync(int id);
        Task AddAsync(MenuItem item);
        Task DeleteAsync(int id);
        Task UpdateAsync(MenuItem item);


    }
}
