using System.ComponentModel.DataAnnotations;

namespace CarRentalSystem.Models
{
    public class UserLoginDTO
    {
   
        
            [Required(ErrorMessage = "Email should be required.")]
            [EmailAddress(ErrorMessage = "Invalid Email")]
            public string? Email { get; set; }

            [Required(ErrorMessage = "Password should be  required.")]
            [StringLength(100, MinimumLength = 8, ErrorMessage = "Password must have atleast of 8 characters.")]
            public string? Password { get; set; }
        
    }
}
