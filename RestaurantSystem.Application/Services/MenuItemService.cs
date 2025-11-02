using AutoMapper;
using RestaurantSystem.Application.DTOs;
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
        private readonly IMapper mapper;

        public MenuItemService(IMenuItemRepository menu , IMapper mapper)
        {
            this.menu = menu;
            this.mapper = mapper;
        }


        public async Task<List<MenuItemDTO>> GetAllAsync() {


            var items = await menu.GetAllAsync();
            var dtos = mapper.Map<List<MenuItemDTO>>(items);
            return dtos;
        }


        public async Task<MenuItemDTO?> GetById(int id) {
            var item = await menu.GetByIdAsync(id);
            if (item == null)
            {
                return null;
            }

            var dto = mapper.Map<MenuItemDTO>(item);
            return dto;
        }


        public async Task<MenuItemDTO> AddItem(CreateMenuItemDTO dto) {
            if (dto.Price <= 0) {
                throw new ArgumentException("Price must be greater than zero.");
            }
            var categoryExists = await menu.CategoryExistsAsync(dto.CategoryId);
            if (!categoryExists)
                throw new ArgumentException("Invalid CategoryId — category not found.");

            var entity = mapper.Map<MenuItem>(dto);
            await menu.AddAsync(entity);

            return mapper.Map<MenuItemDTO>(entity);
        }



        public async Task Delete(int id)
        {
            var existingItem = await menu.GetByIdAsync(id);
            if (existingItem == null)
                throw new KeyNotFoundException("Menu item not found.");

            await menu.DeleteAsync(id);
        }


        public async Task Update(int id, UpdateMenuItemDTO dto)
        {
            var existingItem = await menu.GetByIdAsync(id);
            if (existingItem == null)
                throw new KeyNotFoundException("Menu item not found.");

            mapper.Map(dto, existingItem);
            await menu.UpdateAsync(existingItem);
        }



    }
}
