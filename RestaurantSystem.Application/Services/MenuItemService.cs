using RestaurantSystem.Domain.Entities;
using RestaurantSystem.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantSystem.Application.Services
{
    public class MenuItemService
    {
        private readonly IMenuItemRepository menu;

        public MenuItemService(IMenuItemRepository menu)
        {
            this.menu = menu;
        }


        public Task<IEnumerable<MenuItem>> GetAllAsync() => menu.GetAllAsync();
        public Task<MenuItem?> GetById(int id) { 
            return menu.GetByIdAsync(id);
        }
        public Task AddItem(MenuItem item) {
            return menu.AddAsync(item);
        }
        public Task DeleteItem(int id) => menu.DeleteAsync(id);
        public Task Update(MenuItem item) => menu.UpdateAsync(item);



    }
}
