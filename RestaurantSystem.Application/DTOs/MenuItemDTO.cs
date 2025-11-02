using RestaurantSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

        public int CategoryId { get; set; }
        public string? CategoryName { get; set; }
    }

    public class CreateMenuItemDTO {


        [Required(ErrorMessage = "This feiled is Required")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Name lenght between 2 and 50 chars")]
        public string Name { get; set; } = string.Empty;

        [StringLength(200,MinimumLength = 10 , ErrorMessage = "Description lenght between 10 and 200 chars")]
        public string? Description { get; set; } = string.Empty;

        [Required]
        [Range(0.01, 9999.99, ErrorMessage = "Price must be between 0.01 and 9999.99")]
        public decimal Price { get; set; }

        [Required]
        public bool IsAvaiable { get; set; } = true;
        [Required]
        public int CategoryId { get; set; }
        //public string? CategoryName { get; set; }

    }

    public class UpdateMenuItemDTO {

        [StringLength(50, MinimumLength = 2, ErrorMessage = "Name lenght between 2 and 50 chars")]
        public string Name { get; set; } = string.Empty;


        [StringLength(200, MinimumLength = 10, ErrorMessage = "Description lenght between 10 and 200 chars")]
        public string? Description { get; set; } = string.Empty;

        [Range(0.01, 9999.99, ErrorMessage = "Price must be between 0.01 and 9999.99")]
        public decimal Price { get; set; }

        public bool IsAvaiable { get; set; } = true;

        public int CategoryId { get; set; }

    }

    public class PatchMenuItemDTO
    {
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Name lenght between 2 and 50 chars")]
        public string? Name { get; set; } = string.Empty;

        [StringLength(200, MinimumLength = 10, ErrorMessage = "Description lenght between 10 and 200 chars")]
        public string? Description { get; set; } = string.Empty;

        [Range(0.01, 9999.99, ErrorMessage = "Price must be between 0.01 and 9999.99")]
        public decimal? Price { get; set; }

        public bool? IsAvaiable { get; set; } = true;

        public int? CategoryId { get; set; }

    }

}
