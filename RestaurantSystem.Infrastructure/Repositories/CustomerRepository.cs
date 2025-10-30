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
    public class CustomerRepository : ICustomerRepository
    {
        private readonly AppDbContext context;

        public CustomerRepository(AppDbContext _context)
        {
            context = _context;
        }

        public async Task<Customer?> AddAsync(Customer customer)
        {
           await context.Customers.AddAsync(customer);
            await context.SaveChangesAsync();
            return customer;
        }

        public async Task DeleteAsync(int id)
        {
            var customer = await context.Customers.FirstOrDefaultAsync(a => a.Id == id);
            if (customer != null) { 
                context.Customers.Remove(customer);
                await context.SaveChangesAsync();
                
            }
        }

        public async Task<IEnumerable<Customer>> GetAllAsync()
        {
            return await context.Customers.ToListAsync();
        }

        public async Task<Customer?> GetByIdAsync(int id)
        {
            return await context.Customers.FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task UpdateAsync(Customer customer)
        {
           context.Customers.Update(customer);
            await context.SaveChangesAsync();
        }
    }
}
