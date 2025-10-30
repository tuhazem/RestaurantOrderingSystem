using AutoMapper;
using Azure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
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
        private readonly CategoryService categoryService;

        public MenuItemsController(MenuItemService service , IMapper mapper , CategoryService categoryService)
        {
            this.service = service;
            this.mapper = mapper;
            this.categoryService = categoryService;
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
            var category = await categoryService.GetById(dto.CategoryId);
            if (category == null)
            {
                return BadRequest("Invalid CategoryId");
            }
            await service.AddItem(entity);
            var resdto = mapper.Map <MenuItemDTO >(entity);
            return CreatedAtAction(nameof(GetById),new {id = entity.Id } , resdto);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateItem(int id , UpdateMenuItemDTO dto) {

            var item = await service.GetById(id);
            if (item == null)
            {
                return NotFound();
            }
            mapper.Map(dto,item);
            await service.Update(item);
            return NoContent();
        }


        [HttpPatch("pathc/{id:int}")]
        public async Task<IActionResult> Patch(int id, [FromBody] JsonPatchDocument<PatchMenuItemDTO> patch) {

            if (patch == null) { 
                return BadRequest();
            }

            var item = await service.GetById(id);
            if (item == null) {
                return NotFound();
            }

            var dto = mapper.Map<PatchMenuItemDTO>(item);
            patch.ApplyTo(dto, (Microsoft.AspNetCore.JsonPatch.Adapters.IObjectAdapter)ModelState);

            if (!ModelState.IsValid)
                return BadRequest();
        
            mapper.Map(dto, item);
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
