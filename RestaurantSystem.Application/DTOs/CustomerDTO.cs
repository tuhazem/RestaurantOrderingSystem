using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

        [Required(ErrorMessage = "This field is Required")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Name lenght between 2 and 50 chars")]
        public string Name { get; set; }

        [RegularExpression(@"^\+?[0-9]{10,15}$" , ErrorMessage = "Phone number must contain only digits")]
        public string? PhoneNumber { get; set; }

        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "Please enter a valid email address")]
        public string? Email { get; set; }

    }

    public class UpdateCustomerDTO
    {

        [StringLength(50, MinimumLength = 2, ErrorMessage = "Name lenght between 2 and 50 chars")]
        public string? Name { get; set; }

        [RegularExpression(@"^\+?[0-9]{10,15}$", ErrorMessage = "Phone number must contain only digits")]
        public string? PhoneNumber { get; set; }

        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "Please enter a valid email address")]
        public string? Email { get; set; }

    }



}
