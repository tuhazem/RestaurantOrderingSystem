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
    public class MenuItemProfile : Profile
    {
        public MenuItemProfile()
        {
            CreateMap<MenuItem, MenuItemDTO>().ReverseMap();
            CreateMap<CreateMenuItemDTO, MenuItem>();
        }
    }
}
