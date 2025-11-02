using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantSystem.Application.DTOs
{
    public class CategoryDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<MenuItemDTO>? MenuItems { get; set; }
    }

    public class CreateCategoryDTO {

        [Required(ErrorMessage = "This field is Required")]
        [StringLength(50 , MinimumLength = 2 , ErrorMessage = "Name lenght between 2 and 50 chars")]
        public string Name { get; set; }
       // public List<MenuItemDTO>? MenuItems { get; set; }

    }

}
