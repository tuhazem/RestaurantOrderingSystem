using RestaurantSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantSystem.Domain.Interfaces
{
    public interface IOrderItemRepository
    {
        Task<IEnumerable<OrderItem>> GetAllAsync();
        Task<OrderItem?> GetByIdAsync(int id);
        Task AddAsync(OrderItem item);
        Task UpdateAsync(OrderItem item);
        Task DeleteAsync(int id);
    }
}
