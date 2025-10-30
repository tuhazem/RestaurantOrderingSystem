using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantSystem.Application.DTOs
{
    public class OrderItemDTO {

        public int Id { get; set; }
        public int MenuItemId { get; set; }
        public string? MenuItemName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }

    }

    public class OrderDTO
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalPrice { get; set; }
        public string? Status { get; set; }

        public List<OrderItemDTO> OrderItems { get; set; } = new();
    }

    public class CreateOrderDTO
    {
        //public int CustomerId { get; set; }
        public List<CreateOrderItemDTO> Items { get; set; } = new();
    }

    public class CreateOrderItemDTO
    {
        public int MenuItemId { get; set; }
        public int Quantity { get; set; }
        //public decimal Price { get; set; }
    }

    public class UpdateOrderDTO
    {
        public string? Status { get; set; }
    }
}
