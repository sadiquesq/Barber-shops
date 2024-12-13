using System.ComponentModel.DataAnnotations;

namespace Barber_shops.Models
{
    public class User
    {

        public Guid UserId { get; set; }


        [Required(ErrorMessage ="user name is required")]
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

        public string Role { get; set; } = "User";

        public DateTime CreatedDate { get; set; }= DateTime.Now;

        public bool Status {  get; set; }=true;

    }
}
