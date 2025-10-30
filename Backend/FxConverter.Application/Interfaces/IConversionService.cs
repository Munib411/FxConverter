using FxConverter.Application.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FxConverter.Application.Interfaces
{
    public interface IConversionService
    {
        Task<ConversionResponseDto> ConvertCurrencyAsync(ConversionRequestDto request);
        Task<IEnumerable<ConversionHistoryDto>> GetConversionHistoryAsync(string userId);
    }
}
