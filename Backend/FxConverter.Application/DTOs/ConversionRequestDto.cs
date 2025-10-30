using System.ComponentModel.DataAnnotations;

namespace FxConverter.Application.DTOs
{
    public class ConversionRequestDto
    {
        [Required]
        [Range(typeof(decimal), "0.01", "999999999999999999", ErrorMessage = "Amount must be greater than 0")]
        public decimal Amount { get; set; }

        [Required]
        [StringLength(3, MinimumLength = 3, ErrorMessage = "From currency must be 3 characters")]
        public string FromCurrency { get; set; } = string.Empty;

        [Required]
        [StringLength(3, MinimumLength = 3, ErrorMessage = "To currency must be 3 characters")]
        public string ToCurrency { get; set; } = string.Empty;
    }
}
