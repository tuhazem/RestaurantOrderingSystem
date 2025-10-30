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
            
            CreateMap<MenuItem, MenuItemDTO>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name));

            CreateMap<CreateMenuItemDTO, MenuItem>();
            CreateMap<PatchMenuItemDTO, MenuItem>();
            CreateMap<UpdateMenuItemDTO, MenuItem>();

        }
    }
}
