using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantSystem.Application.DTOs
{
    public class CustomerDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
    }

    public class CreateCustomerDTO {

        public string Name { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }

    }

    public class UpdateCustomerDTO
    {

        public string? Name { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }

    }



}
