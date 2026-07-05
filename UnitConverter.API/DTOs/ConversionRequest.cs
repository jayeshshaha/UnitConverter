using System.ComponentModel.DataAnnotations;
using UnitConverter.Domain.Enums;

namespace UnitConverter.API.DTOs
{
    public class ConversionRequest
    {
        [Required(ErrorMessage = "Category is required")]
        [StringLength(50, ErrorMessage = "Category cannot be longer than 50 characters")]
        public required string Category { get; set; }

        [Required(ErrorMessage = "From unit is required")]
        [StringLength(50, ErrorMessage = "From unit cannot be longer than 50 characters")]
        public required string FromUnit { get; set; }

        [Required(ErrorMessage = "To unit is required")]
        [StringLength(50, ErrorMessage = "To unit cannot be longer than 50 characters")]
        public required string ToUnit { get; set; }


        [Required(ErrorMessage = "Value is required")]
        public double Value { get; set; }
    }
}
