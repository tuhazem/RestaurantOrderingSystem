using AutoMapper;
using RestaurantSystem.Application.DTOs;
using RestaurantSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantSystem.Application.MappingProfiles
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {

            CreateMap<Order, OrderDTO>()
               .ForMember(dest => dest.OrderItems, opt => opt.MapFrom(src => src.OrderItems));

            
            CreateMap<OrderItem, OrderItemDTO>()
                .ForMember(dest => dest.MenuItemName,
                           opt => opt.MapFrom(src => src.MenuItem != null ? src.MenuItem.Name : null));

            
            CreateMap<CreateOrderDTO, Order>()
                .ForMember(dest => dest.OrderItems, opt => opt.MapFrom(src => src.Items));

            CreateMap<CreateOrderItemDTO, OrderItem>();

            
            CreateMap<UpdateOrderDTO, Order>()
                .ForMember(dest => dest.OrderItems, opt => opt.Ignore());

            CreateMap<Order, OrderDTO>()
    .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.Customer.Name))
    .ReverseMap();
        }
    }
}
