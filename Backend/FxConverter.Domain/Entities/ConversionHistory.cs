using System;

namespace FxConverter.Domain.Entities
{
    public class ConversionHistory
    {
        public Guid ConversionId { get; set; }
        public string FromCurrency { get; set; } = string.Empty;
        public string ToCurrency { get; set; } = string.Empty;
        public decimal FromAmount { get; set; }
        public decimal ToAmount { get; set; }
        public decimal ExchangeRate { get; set; }
        public DateTime ConversionDate { get; set; }
        public string UserId { get; set; } = string.Empty;
    }
}
