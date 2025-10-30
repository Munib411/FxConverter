using FxConverter.Application.DTOs;
using Refit;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FxConverter.Infrastructure.Services
{
    public interface IOpenExchangeRatesApi
    {
        [Get("/latest.json")]
        Task<ExchangeRateDto> GetLatestRatesAsync([Query] string app_id, [Query] string @base = "USD");

        [Get("/currencies.json")]
        Task<Dictionary<string, string>> GetCurrenciesAsync([Query] string app_id);
    }
}
