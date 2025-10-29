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
    public class OrderItemRepository : IOrderItemRepository
    {
        private readonly AppDbContext context;

        public OrderItemRepository(AppDbContext _context)
        {
            context = _context;
        }

        public async Task AddAsync(OrderItem item)
        {
            await context.OrderItems.AddAsync(item);
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var item = await context.OrderItems.FirstOrDefaultAsync(a=> a.Id == id);
            if (item != null) { 
                context.OrderItems.Remove(item);
                await context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<OrderItem>> GetAllAsync()
        {
            return await context.OrderItems.Include(a => a.MenuItem).Include(o=> o.Order).ToListAsync();
        }

        public async Task<OrderItem?> GetByIdAsync(int id)
        {
            return await context.OrderItems.Include(a => a.MenuItem).Include(a => a.Order)
                .FirstOrDefaultAsync(a=> a.Id == id);
        }

        public async Task UpdateAsync(OrderItem item)
        {
             context.OrderItems.Update(item);
            await context.SaveChangesAsync();
        }
    }
}
