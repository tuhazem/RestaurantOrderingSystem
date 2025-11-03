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
    public class OrdersController : ControllerBase
    {
        private readonly OrderService service;
        private readonly IMapper mapper;
        private readonly MenuItemService menuItemService;
        private readonly CustomerService customerService;

        public OrdersController(OrderService service, IMapper mapper ,
            MenuItemService menuItemService , 
            CustomerService customerService)
        {
            this.service = service;
            this.mapper = mapper;
            this.menuItemService = menuItemService;
            this.customerService = customerService;
        }

        [HttpGet]
        [Authorize(Roles = "Customer,Admin")]
        public async Task<IActionResult> GetAll() {

            var item = await service.GetAllAsync();
            var dto = mapper.Map<IEnumerable<OrderDTO>>(item);
            return Ok(dto);

        }


        [HttpGet("{id:int}")]
        [Authorize(Roles = "Customer,Admin")]
        public async Task<IActionResult> GetById(int id) {

            var order = await service.GetByIdAsync(id);
            if (order == null) return NotFound();
            var dto = mapper.Map<OrderDTO>(order);
            return Ok(dto);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> Create(CreateOrderDTO dto) {

            if (dto.CustomerId == null)
                return BadRequest("CustomerId is required");

            var customer = await customerService.GetByIdAsync(dto.CustomerId.Value);

            if ( customer == null) return BadRequest($"this customer is not Found {dto.CustomerId}");

            var entity = mapper.Map<Order>(dto);
            decimal total = 0;


            foreach (var item in entity.OrderItems) {

                var menuitem = await menuItemService.GetById(item.MenuItemId);
                if (menuitem == null) return BadRequest("Not Found");

                item.Price = menuitem.Price;
                total += menuitem.Price * item.Quantity;
            }

            

            entity.TotalPrice = total;
            entity.PaymentMethod = dto.PaymentMethod;
            entity.OrderDate = DateTime.Now;
            await service.AddAsync(entity);
            var resdto = mapper.Map<OrderDTO>(entity);
            return CreatedAtAction(nameof(GetById), new { id = entity.Id }, resdto);
        }

        [HttpPut("{id:int}")]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> Edit(int id, UpdateOrderDTO dto)
        {

            var item = await service.GetByIdAsync(id);
            if (item == null) return NotFound();
            mapper.Map(dto, item);
            await service.UpdateAsync(item);
            return NoContent();

        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> Delete(int id) { 
        
            var order = await service.GetByIdAsync(id);
            if(order == null) { return NotFound(); }
            await service.DeleteAsync(id);
            return NoContent();
        }

        [HttpPost("{orderId:int}")]
        public async Task<IActionResult> Pay(int orderId , [FromBody] string method)
        {

            var succ = await service.ProcessPaymentAsync(orderId , method);
            if (!succ) return NotFound(new { message = "order not found" });

            return Ok(new {
            
                message = $"Payment successful using {method}",
                Status = "Paid"

            });

        }



    }
}
