using FxConverter.Application.DTOs;
using FxConverter.Application.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FxConverter.Infrastructure.Services
{
    public class ExchangeRateService : IExchangeRateService
    {
        private readonly IOpenExchangeRatesApi _api;
        private readonly IMemoryCache _cache;
        private readonly ILogger<ExchangeRateService> _logger;
        private const string API_KEY = "32151b75187f42809b69bc680de80922";
        private const string CACHE_KEY_RATES = "exchange_rates";
        private const string CACHE_KEY_CURRENCIES = "currencies";
        private readonly TimeSpan CACHE_DURATION = TimeSpan.FromMinutes(30);

        public ExchangeRateService(
            IOpenExchangeRatesApi api,
            IMemoryCache cache,
            ILogger<ExchangeRateService> logger)
        {
            _api = api;
            _cache = cache;
            _logger = logger;
        }

        public async Task<ExchangeRateDto> GetExchangeRatesAsync(string baseCurrency)
        {
            var cacheKey = $"{CACHE_KEY_RATES}_{baseCurrency}";
            
            if (_cache.TryGetValue(cacheKey, out ExchangeRateDto? cachedRates))
            {
                return cachedRates!;
            }

            try
            {
                var rates = await _api.GetLatestRatesAsync(API_KEY, baseCurrency);
                _cache.Set(cacheKey, rates, CACHE_DURATION);
                return rates;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching exchange rates for base currency: {BaseCurrency}, returning fallback rates", baseCurrency);
                
                var fallbackRates = new ExchangeRateDto
                {
                    Base = baseCurrency,
                    Date = DateTime.UtcNow,
                    Rates = new Dictionary<string, decimal>
                    {
                        { "USD", baseCurrency == "USD" ? 1m : 1.0m },
                        { "EUR", baseCurrency == "EUR" ? 1m : 0.85m },
                        { "GBP", baseCurrency == "GBP" ? 1m : 0.73m },
                        { "JPY", baseCurrency == "JPY" ? 1m : 110.0m },
                        { "CAD", baseCurrency == "CAD" ? 1m : 1.25m },
                        { "AUD", baseCurrency == "AUD" ? 1m : 1.35m },
                        { "CHF", baseCurrency == "CHF" ? 1m : 0.92m },
                        { "CNY", baseCurrency == "CNY" ? 1m : 6.45m },
                        { "SEK", baseCurrency == "SEK" ? 1m : 8.5m },
                        { "NZD", baseCurrency == "NZD" ? 1m : 1.45m }
                    }
                };

                _cache.Set(cacheKey, fallbackRates, TimeSpan.FromMinutes(5));
                return fallbackRates;
            }
        }

        public async Task<decimal> GetExchangeRateAsync(string fromCurrency, string toCurrency)
        {
            if (fromCurrency == toCurrency)
                return 1m;

            var usdRates = await GetExchangeRatesAsync("USD");

            if (!usdRates.Rates.TryGetValue(fromCurrency, out var fromRate))
                throw new ArgumentException($"Base rate not found for currency: {fromCurrency}");

            if (!usdRates.Rates.TryGetValue(toCurrency, out var toRate))
                throw new ArgumentException($"Target rate not found for currency: {toCurrency}");

            var rate = toRate / fromRate;
            return rate;
        }

        public async Task<IEnumerable<CurrencyDto>> GetAvailableCurrenciesAsync()
        {
            if (_cache.TryGetValue(CACHE_KEY_CURRENCIES, out IEnumerable<CurrencyDto>? cachedCurrencies))
            {
                return cachedCurrencies!;
            }

            try
            {
                var currencies = await _api.GetCurrenciesAsync(API_KEY);
                var currencyDtos = currencies.Select(c => new CurrencyDto
                {
                    Code = c.Key,
                    Name = c.Value,
                    Symbol = GetCurrencySymbol(c.Key)
                }).ToList();

                _cache.Set(CACHE_KEY_CURRENCIES, currencyDtos, CACHE_DURATION);
                return currencyDtos;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching available currencies, returning fallback currencies");
                
                var fallbackCurrencies = new List<CurrencyDto>
                {
                    new() { Code = "USD", Name = "US Dollar", Symbol = "$" },
                    new() { Code = "EUR", Name = "Euro", Symbol = "€" },
                    new() { Code = "GBP", Name = "British Pound", Symbol = "£" },
                    new() { Code = "JPY", Name = "Japanese Yen", Symbol = "¥" },
                    new() { Code = "CAD", Name = "Canadian Dollar", Symbol = "C$" },
                    new() { Code = "AUD", Name = "Australian Dollar", Symbol = "A$" },
                    new() { Code = "CHF", Name = "Swiss Franc", Symbol = "CHF" },
                    new() { Code = "CNY", Name = "Chinese Yuan", Symbol = "¥" },
                    new() { Code = "SEK", Name = "Swedish Krona", Symbol = "kr" },
                    new() { Code = "NZD", Name = "New Zealand Dollar", Symbol = "NZ$" }
                };

                _cache.Set(CACHE_KEY_CURRENCIES, fallbackCurrencies, TimeSpan.FromMinutes(5));
                return fallbackCurrencies;
            }
        }

        private static string GetCurrencySymbol(string currencyCode)
        {
            return currencyCode switch
            {
                "USD" => "$",
                "EUR" => "€",
                "GBP" => "£",
                "JPY" => "¥",
                "CAD" => "C$",
                "AUD" => "A$",
                "CHF" => "CHF",
                "CNY" => "¥",
                "SEK" => "kr",
                "NZD" => "NZ$",
                _ => currencyCode
            };
        }
    }
}
