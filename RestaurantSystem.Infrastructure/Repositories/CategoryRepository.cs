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
    public class CategoryRepository : ICategoryRepository
    {
        private readonly AppDbContext context;

        public CategoryRepository(AppDbContext _context)
        {
            context = _context;
        }



        public async Task Add(Category category)
        {
            await context.Categories.AddAsync(category);
            await context.SaveChangesAsync();
        }


        public async Task Delete(int id)
        {
            var item = await context.Categories.FirstOrDefaultAsync(c => c.Id == id);
            if (item != null)
            { 
                context.Categories.Remove(item);
                context.SaveChanges();
            }
        }

        public async Task<IEnumerable<Category>> GetAll()
        {
            return await context.Categories.Include(i => i.MenuItems).ToListAsync();
        }

        public async Task<Category?> GetById(int id)
        {
            return await context.Categories.Include(i=> i.MenuItems).FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task Update(Category category)
        {
            context.Categories.Update(category);
            await context.SaveChangesAsync();
        }
    }
}
