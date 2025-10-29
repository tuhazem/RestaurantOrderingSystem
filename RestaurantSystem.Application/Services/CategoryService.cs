using RestaurantSystem.Domain.Entities;
using RestaurantSystem.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantSystem.Application.Services
{
    public class CategoryService
    {
        private readonly ICategoryRepository repo;

        public CategoryService(ICategoryRepository _category)
        {
            this.repo = _category;
        }

        public Task<IEnumerable<Category>> GetAll() => repo.GetAll();
        public Task<Category?> GetById(int id) => repo.GetById(id);

        public Task Add(Category category) => repo.Add(category);
        public Task Delete(int id)=> repo.Delete(id);
        public Task Update(Category category)=> repo.Update(category);


    }
}
