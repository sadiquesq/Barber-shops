using System.ComponentModel.DataAnnotations;

namespace Barber_shops.DTOs.User
{
    public class UserDto
    {

        [Required(ErrorMessage = "user name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "eamil is required")]
        [EmailAddress]

        public string Email { get; set; }

        [Required(ErrorMessage = "number is required")]
        public int number { get; set; }

        [Required]
        [MinLength(8)]
        [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$",
        ErrorMessage = "Password must contain at least one letter, one number, and one special character.")]
        public string Password { get; set; }

        [Required]

        public string otp { get; set; }

    }
}
