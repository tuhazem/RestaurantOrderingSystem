using RestaurantSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantSystem.Domain.Interfaces
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetAll();
        Task<Category?> GetById(int id);
        Task Add(Category category);
        Task Delete(int id);
        Task Update(Category category);

    }
}
