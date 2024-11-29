using System.ComponentModel.DataAnnotations;

namespace CarRentalSystem.Models
{
    public class RentDTO
    {
        [Required(ErrorMessage = "Car ID is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Car ID must be a positive number.")]
        public int CarId { get; set; }

        [Required(ErrorMessage = "Number of rent days is required.")]
        [Range(1, 365, ErrorMessage = "Rent days must be between 1 and 365.")]
        public int Days { get; set; } 
    }
}
