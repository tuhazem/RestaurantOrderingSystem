using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantSystem.Application.DTOs
{
    public class OrderItemDTO {

        public int Id { get; set; }

        [Required]
        public int MenuItemId { get; set; }
        public string? MenuItemName { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1")]
        public int Quantity { get; set; }

        [Required]
        [Range(0.01, 9999.99, ErrorMessage = "Price must be between 0.01 and 9999.99")]
        public decimal Price { get; set; }

    }

    public class OrderDTO
    {
        
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalPrice { get; set; }
        public string? Status { get; set; }

        public string? CustomerName { get; set; }

        public List<OrderItemDTO> OrderItems { get; set; } = new();
    }

    public class CreateOrderDTO
    {

        
        public int? CustomerId { get; set; }

        [Required]
        [MinLength(1, ErrorMessage = "Order must contain at least one item")]
        public List<CreateOrderItemDTO> Items { get; set; } = new();
    }

    public class CreateOrderItemDTO
    {
        [Required]
        public int MenuItemId { get; set; }


        [Required]
        [MinLength(1, ErrorMessage = "Order must contain at least one item")]
        public int Quantity { get; set; }
        //public decimal Price { get; set; }
    }

    public class UpdateOrderDTO
    {
        [Required]
        public string? Status { get; set; }
    }
}
