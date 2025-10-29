using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestaurantSystem.Application.DTOs;
using RestaurantSystem.Application.Services;
using RestaurantSystem.Domain.Entities;

namespace RestaurantSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuItemsController : ControllerBase
    {
        private readonly MenuItemService service;
        private readonly IMapper mapper;

        public MenuItemsController(MenuItemService service , IMapper mapper)
        {
            this.service = service;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var items = await service.GetAllAsync();
            var dtos = mapper.Map<IEnumerable<MenuItemDTO>>(items);
            return Ok(dtos);
        }


        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id) {

            var item = await service.GetById(id);
            if (item == null) {
                return NotFound();
            }

            var dto = mapper.Map<MenuItemDTO>(item);
            return Ok(dto);

        }

        [HttpPost]
        public async Task<IActionResult> AddNewItem(CreateMenuItemDTO dto) {

            var entity = mapper.Map<MenuItem>(dto);
            await service.AddItem(entity);
            var resdto = mapper.Map < MenuItemDTO >(entity);
            return CreatedAtAction(nameof(GetById),new {id = entity.Id } , resdto);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateItem(int id , MenuItemDTO dto) {

            var item = await service.GetById(id);
            if (item == null)
            {
                return NotFound();
            }
            mapper.Map(dto,item);
            await service.Update(item);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItem(int id) { 
        
            var item = await service.GetById(id);
            if (item == null) {
                return NotFound();
            }
            await service.DeleteItem(id);
            return NoContent();
        }

    }
}
