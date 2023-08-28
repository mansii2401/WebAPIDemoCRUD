using System.ComponentModel.DataAnnotations;
using System.Data;
using WebAPIDemo.Entities.Domain;
using static WebAPIDemo.Entities.Domain.Role;

namespace WebAPIDemo.Entities.DTO
{

        public class AddUserDTO
        {
            [Key]
            public int Id { get; set; }
            public int UserId { get; set; }

            [Required]
            [RegularExpression("^(?=.*[a-zA-Z])(?=.*\\d)(?=.*[!@#$%^&*()_+])[A-Za-z\\d!@#$%^&*()_+]{6,15}", ErrorMessage = "The username Must be 6 to 15 characters in length, Must contain at least one letter and one number and a special character from !@#$%^&*()_+ ")]
            public string UserName { get; set; }
            [Required]
            [RegularExpression("^(?=.*[A-Za-z])(?=.*\\d)[A-Za-z\\d]{6,}$", ErrorMessage = "Password must be Minimum 6 characters and must contain atleast 1 Alphabet and 1 Number")]
            public string Password { get; set; }

            public string FirstName { get; set; }
            public string LastName { get; set; }

            [Required]
            [EmailAddress(ErrorMessage = "Invalid Email Address, Please enter  Email address in the form of abc@domain.com).")]
            public string UserEmail { get; set; }

            [Required]

            public Role Role { get; set; }
            public Boolean IsStudent { get; set; }

        }
    }