using RestaurantSystem.Domain.Entities;
using RestaurantSystem.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantSystem.Application.Services
{
    public class CustomerService
    {
        private readonly ICustomerRepository repository;

        public CustomerService(ICustomerRepository repository)
        {
            this.repository = repository;
        }

        public Task<IEnumerable<Customer>> GetAllAsync() => repository.GetAllAsync();
        public Task<Customer?> GetByIdAsync(int id) => repository.GetByIdAsync(id);
        
        public Task<Customer?> AddAsync(Customer customer) => repository.AddAsync(customer);
        public Task UpdateAsync(Customer customer) => repository.UpdateAsync(customer);
        public Task DeleteAsync(int id) => repository.DeleteAsync(id);

    }
}
