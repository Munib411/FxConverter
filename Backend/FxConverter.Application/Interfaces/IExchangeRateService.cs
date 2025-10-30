using FxConverter.Application.DTOs;
using System.Threading.Tasks;

namespace FxConverter.Application.Interfaces
{
    public interface IExchangeRateService
    {
        Task<ExchangeRateDto> GetExchangeRatesAsync(string baseCurrency);
        Task<decimal> GetExchangeRateAsync(string fromCurrency, string toCurrency);
        Task<IEnumerable<CurrencyDto>> GetAvailableCurrenciesAsync();
    }
}
