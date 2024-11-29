using System;
using System.ComponentModel.DataAnnotations;

namespace CarRentalSystem.Models
{
    public class CarModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Make is a required field")]
        [StringLength(30, ErrorMessage = "Make must be at most 30 characters long")]
        public string? Make { get; set; }

        [Required(ErrorMessage = "Model name is required")]
        [StringLength(30, ErrorMessage = "Model must be at most 30 characters long")]
        public string? Model { get; set; }

        [Required(ErrorMessage = "Year is required")]
        [Range(1886, int.MaxValue, ErrorMessage = "Year must be 1886 or later")]
        public int Year { get; set; }

        [Required(ErrorMessage = "Price per day is required")]
        [Range(0.0, double.MaxValue, ErrorMessage = "Price per day must be a non-negative value")]
        public decimal PricePerDay { get; set; }

        [Required(ErrorMessage = "Availability status is required")]
        public bool isAvailable { get; set; } = true;
    }
}
