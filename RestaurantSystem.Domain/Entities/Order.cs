using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantSystem.Domain.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.Now;
        public decimal TotalPrice { get; set; }
        public string? Status { get; set; } = "Pending";


        public List<OrderItem> OrderItems { get; set; } = new();

    }
}
