using RestaurantSystem.Application.DTOs;
using RestaurantSystem.Domain.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantSystem.Application.MappingProfiles
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<Category, CategoryDTO>()
                .ForMember(dest => dest.MenuItems, opt => opt.MapFrom(src => src.MenuItems));

            
            CreateMap<CreateCategoryDTO, Category>();

            
            CreateMap<MenuItem, MenuItemDTO>();
        }
    }
}
