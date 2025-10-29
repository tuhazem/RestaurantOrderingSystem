using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestaurantSystem.Application.Services;

namespace RestaurantSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly OrderService service;
        private readonly IMapper mapper;

        public OrdersController(OrderService service , IMapper mapper)
        {
            this.service = service;
            this.mapper = mapper;
        }





    }
}
