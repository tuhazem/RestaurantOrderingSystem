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
    public class MenuItemRepository : IMenuItemRepository
    {
        private readonly AppDbContext dbcontext;

        public MenuItemRepository(AppDbContext _dbcontext)
        {
            dbcontext = _dbcontext;
        }

        public async Task AddAsync(MenuItem item)
        {
            await dbcontext.MenuItems.AddAsync(item);
            await dbcontext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var item = await dbcontext.MenuItems.FirstOrDefaultAsync(x => x.Id == id);
            if (item != null)
            {
                dbcontext.MenuItems.Remove(item);
                await dbcontext.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<MenuItem>> GetAllAsync()
        {
            return await dbcontext.MenuItems.ToListAsync();
        }

        public async Task<MenuItem?> GetByIdAsync(int id)
        {
            return await dbcontext.MenuItems.FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task UpdateAsync(MenuItem item)
        {
            dbcontext.MenuItems.Update(item);
            await dbcontext.SaveChangesAsync();
        }
    }
}
