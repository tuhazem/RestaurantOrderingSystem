using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantSystem.Application.DTOs
{
    public class RegisterDTO
    {
        [Required]
        [Display(Name = "User Name")]
        [StringLength(30 , MinimumLength = 2)]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "Enter Valid Email Address")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$",
            ErrorMessage = "Password must be at least 8 characters and contain upper, lower, digit, and special char.")]
        public string Password { get; set; }

        [Required]
        [Display(Name = "Full Name")]
        [StringLength(30 , MinimumLength = 2)]
        public string FullName { get; set; }

    }

    public class LoginDTO {


        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }

    public class ResponseLoginDTO {


        public string Name { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }

        public string Token { get; set; }
        public List<string> Role { get; set; }



    }
}
