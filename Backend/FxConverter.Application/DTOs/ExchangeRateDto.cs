using System.Collections.Generic;

namespace FxConverter.Application.DTOs
{
    public class ExchangeRateDto
    {
        public string Base { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public Dictionary<string, decimal> Rates { get; set; } = new Dictionary<string, decimal>();
    }
}
