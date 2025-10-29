using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantSystem.Application.DTOs
{
    public class MenuItemDTO
    {

        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }

        public bool IsAvaiable { get; set; }
    }

    public class CreateMenuItemDTO { 
    
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
    
    }

}
