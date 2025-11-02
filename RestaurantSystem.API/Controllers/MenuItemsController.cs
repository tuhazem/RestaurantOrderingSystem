using AutoMapper;
using Azure;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize(Roles = "Admin,Customer")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await service.GetAllAsync());
        }


        [HttpGet("{id:int}")]
        [Authorize(Roles = "Customer,Admin")]

        public async Task<IActionResult> GetById(int id) {
            return Ok(await service.GetById(id));
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> AddNewItem(CreateMenuItemDTO dto) {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var created = await service.AddItem(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.CategoryId }, created);
        }


        [HttpPut("{id:int}")]

        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> UpdateItem(int id , UpdateMenuItemDTO dto) {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await service.Update(id, dto);
            return NoContent();
        }


        [HttpPatch("{id:int}")]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> Patch(int id, [FromBody] JsonPatchDocument<PatchMenuItemDTO> patch) {

            if (patch == null)
                return BadRequest();

            var existingItem = await service.GetById(id);
            if (existingItem == null)
                return NotFound(new { message = "Menu item not found" });

            var dto = mapper.Map<PatchMenuItemDTO>(existingItem);
            patch.ApplyTo(dto, ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var updateDto = mapper.Map<UpdateMenuItemDTO>(dto);
            await service.Update(id, updateDto);

            return NoContent();

        }


        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteItem(int id) {

            await service.Delete(id);
            return NoContent();
        }

    }
}
