using FxConverter.Application.DTOs;
using FxConverter.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FxConverter.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CurrencyController : ControllerBase
    {
        private readonly IExchangeRateService _exchangeRateService;

        public CurrencyController(IExchangeRateService exchangeRateService)
        {
            _exchangeRateService = exchangeRateService;
        }

        [HttpGet("health")]
        public ActionResult GetHealth()
        {
            return Ok(new { status = "API is running", timestamp = DateTime.UtcNow });
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<CurrencyDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<CurrencyDto>>> GetCurrencies()
        {
            try
            {
                var currencies = await _exchangeRateService.GetAvailableCurrenciesAsync();
                return Ok(currencies);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while retrieving currencies" });
            }
        }

        /// <param name="base">Base currency code (default: USD)</param>
        [HttpGet("rates/{base}")]
        [ProducesResponseType(typeof(ExchangeRateDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ExchangeRateDto>> GetExchangeRates(string @base = "USD")
        {
            try
            {
                var rates = await _exchangeRateService.GetExchangeRatesAsync(@base);
                return Ok(rates);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while retrieving exchange rates" });
            }
        }
    }
}
