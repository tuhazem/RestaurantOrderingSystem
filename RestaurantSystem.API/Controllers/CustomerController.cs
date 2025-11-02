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
    public class CustomerController : ControllerBase
    {
        private readonly CustomerService service;
        private readonly IMapper mapper;

        public CustomerController(CustomerService service , IMapper mapper)
        {
            this.service = service;
            this.mapper = mapper;
        }

        [HttpGet]
        [Authorize(Roles = "Customer,Admin")]

        public async Task<IActionResult> GetAll() {

            var customer = await service.GetAllAsync();
            var dto = mapper.Map<IEnumerable<CustomerDTO>>(customer);
            return Ok(dto);
        }

        [HttpGet("{id:int}")]
        [Authorize(Roles = "Customer,Admin")]

        public async Task<IActionResult> GetById(int id) { 
        
            var customer = await service.GetByIdAsync(id);
            if (customer == null) return BadRequest("Not Found");

            var dto = mapper.Map<CustomerDTO>(customer);
            return Ok(dto);
        
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(CreateCustomerDTO dto) {

            var entity = mapper.Map<Customer>(dto);
            await service.AddAsync(entity);
            var resdto = mapper.Map<CustomerDTO>(entity);
            return CreatedAtAction(nameof(GetById), new { id = entity.Id} , resdto);

        }

        [HttpPut("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, UpdateCustomerDTO dto) { 
        
            var customer = await service.GetByIdAsync(id);
            if(customer == null) return NotFound();

            mapper.Map(dto,customer);
            await service.UpdateAsync(customer);
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> Delete(int id)
        {
            var customer = await service.GetByIdAsync(id);
            if (customer == null) return NotFound();

            await service.DeleteAsync(id);
            return NoContent();




        }


    }
}
