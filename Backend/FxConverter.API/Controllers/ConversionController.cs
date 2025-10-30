using FxConverter.Application.DTOs;
using FxConverter.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FxConverter.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ConversionController : ControllerBase
    {
        private readonly IConversionService _conversionService;

        public ConversionController(IConversionService conversionService)
        {
            _conversionService = conversionService;
        }

      
        /// <param name="request">Conversion request details</param>
        [HttpPost]
        [ProducesResponseType(typeof(ConversionResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ConversionResponseDto>> ConvertCurrency([FromBody] ConversionRequestDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var result = await _conversionService.ConvertCurrencyAsync(request);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while converting currency" });
            }
        }

       
        /// <param name="userId">User ID (hardcoded for assessment)</param>
        [HttpGet("history")]
        [ProducesResponseType(typeof(IEnumerable<ConversionHistoryDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<ConversionHistoryDto>>> GetConversionHistory([FromQuery] string userId = "default-user")
        {
            try
            {
                var history = await _conversionService.GetConversionHistoryAsync(userId);
                return Ok(history);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while retrieving conversion history" });
            }
        }
    }
}
