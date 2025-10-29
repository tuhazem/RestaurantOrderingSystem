using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantSystem.Domain.Entities
{
    public class OrderItem
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int MenuItemId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }

        public Order? Order { get; set; }
        public MenuItem? MenuItem { get; set; }
    }
}
