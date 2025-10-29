using Microsoft.EntityFrameworkCore;
using RestaurantSystem.Domain.Entities;
using RestaurantSystem.Domain.Interfaces;
using RestaurantSystem.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantSystem.Infrastructure.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly AppDbContext context;

        public OrderRepository(AppDbContext _context)
        {
            context = _context;
        }

        public async Task AddAsync(Order order)
        {
            await context.Orders.AddAsync(order);
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsunc(int id)
        {
            var item = await context.Orders.FirstOrDefaultAsync(x => x.Id == id);
            if (item != null) { 
                context.Orders.Remove(item);
                await context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Order>> GetAllAsync()
        {
            return await context.Orders
                .Include(oi => oi.OrderItems)
                .ThenInclude(o => o.MenuItem)
                .ToListAsync();
        }

        public async Task<Order> GetByIdAsync(int id)
        {
            return await context.Orders
                .Include(oi => oi.OrderItems)
                .ThenInclude(o => o.MenuItem)
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task UpdateAsync(Order order)
        {
            context.Orders.Update(order);
            await context.SaveChangesAsync();
        }
    }
}
