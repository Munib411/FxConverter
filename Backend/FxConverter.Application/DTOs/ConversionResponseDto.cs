namespace FxConverter.Application.DTOs
{
    public class ConversionResponseDto
    {
        public decimal FromAmount { get; set; }
        public decimal ToAmount { get; set; }
        public string FromCurrency { get; set; } = string.Empty;
        public string ToCurrency { get; set; } = string.Empty;
        public decimal ExchangeRate { get; set; }
        public DateTime ConversionDate { get; set; }
    }
}
