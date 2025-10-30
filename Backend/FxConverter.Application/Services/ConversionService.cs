using AutoMapper;
using FxConverter.Application.DTOs;
using FxConverter.Application.Interfaces;
using FxConverter.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FxConverter.Application.Services
{
    public class ConversionService : IConversionService
    {
        private readonly IConversionRepository _conversionRepository;
        private readonly IExchangeRateService _exchangeRateService;
        private readonly IMapper _mapper;

        public ConversionService(
            IConversionRepository conversionRepository,
            IExchangeRateService exchangeRateService,
            IMapper mapper)
        {
            _conversionRepository = conversionRepository;
            _exchangeRateService = exchangeRateService;
            _mapper = mapper;
        }

        public async Task<ConversionResponseDto> ConvertCurrencyAsync(ConversionRequestDto request)
        {
            var exchangeRate = await _exchangeRateService.GetExchangeRateAsync(
                request.FromCurrency, 
                request.ToCurrency);

            var convertedAmount = request.Amount * exchangeRate;

            var conversion = new ConversionHistory
            {
                ConversionId = Guid.NewGuid(),
                FromCurrency = request.FromCurrency,
                ToCurrency = request.ToCurrency,
                FromAmount = request.Amount,
                ToAmount = convertedAmount,
                ExchangeRate = exchangeRate,
                ConversionDate = DateTime.UtcNow,
                UserId = "default-user" 
            };

            await _conversionRepository.AddAsync(conversion);

            return new ConversionResponseDto
            {
                FromAmount = request.Amount,
                ToAmount = convertedAmount,
                FromCurrency = request.FromCurrency,
                ToCurrency = request.ToCurrency,
                ExchangeRate = exchangeRate,
                ConversionDate = conversion.ConversionDate
            };
        }

        public async Task<IEnumerable<ConversionHistoryDto>> GetConversionHistoryAsync(string userId)
        {
            var history = await _conversionRepository.GetByUserIdAsync(userId);
            return history.Select(h => _mapper.Map<ConversionHistoryDto>(h));
        }
    }
}
