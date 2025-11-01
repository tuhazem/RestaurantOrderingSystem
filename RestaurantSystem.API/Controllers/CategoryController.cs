using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestaurantSystem.Application.DTOs;
using RestaurantSystem.Application.Services;
using RestaurantSystem.Domain.Entities;

namespace RestaurantSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly CategoryService service;
        private readonly IMapper mapper;

        public CategoryController(CategoryService service, IMapper mapper)
        {
            this.service = service;
            this.mapper = mapper;
        }

        [HttpGet]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> GetAll() {

            var item = await service.GetAll();
            var dto = mapper.Map<IEnumerable<CategoryDTO>>(item);
            return Ok(dto);

        }

        [HttpGet("{id:int}")]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> GetById(int id) {

            var item = await service.GetById(id);
            if (item == null) {
                return NotFound();
            }
            var map = mapper.Map<CategoryDTO>(item);
            return Ok(map);
        }


        [HttpPost]
        public async Task<IActionResult> AddNew(CreateCategoryDTO dto) {

            var category = mapper.Map<Category>(dto);
            await service.Add(category);
            var resdto = mapper.Map<CategoryDTO>(category);
            return CreatedAtAction(nameof(GetById), new { id = category.Id }, resdto);
        }


        [HttpPut("{id:int}")]
        public async Task<IActionResult> Edit(int id, CreateCategoryDTO dto)
        {

            var category = await service.GetById(id);
            if (category == null) {
                return NotFound();
            }
            mapper.Map(dto,category);
            await service.Update(category);
            return NoContent();

        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id) {
            var category = await service.GetById(id);
            if (category == null) {
                return NotFound();
            }
            await service.Delete(id);
            return NoContent();
        }


    }
}
