using AutoFixture;
using AutoMapper;
using FxConverter.Application.DTOs;
using FxConverter.Application.Interfaces;
using FxConverter.Application.Services;
using FxConverter.Domain.Entities;
using Moq;
using Xunit;

namespace FxConverter.Tests.Services
{
    public class ConversionServiceTests
    {
        private readonly Fixture _fixture;
        private readonly Mock<IConversionRepository> _mockRepository;
        private readonly Mock<IExchangeRateService> _mockExchangeRateService;
        private readonly Mock<IMapper> _mockMapper;
        private readonly ConversionService _service;

        public ConversionServiceTests()
        {
            _fixture = new Fixture();
            _mockRepository = new Mock<IConversionRepository>();
            _mockExchangeRateService = new Mock<IExchangeRateService>();
            _mockMapper = new Mock<IMapper>();
            _service = new ConversionService(_mockRepository.Object, _mockExchangeRateService.Object, _mockMapper.Object);
        }

        [Fact]
        public async Task ConvertCurrencyAsync_ValidRequest_ReturnsConversionResponse()
        {
            
            var request = _fixture.Create<ConversionRequestDto>();
            request.Amount = 100m;
            request.FromCurrency = "USD";
            request.ToCurrency = "EUR";
            
            var exchangeRate = 0.85m;
            var expectedResponse = new ConversionResponseDto
            {
                FromAmount = request.Amount,
                ToAmount = request.Amount * exchangeRate,
                FromCurrency = request.FromCurrency,
                ToCurrency = request.ToCurrency,
                ExchangeRate = exchangeRate,
                ConversionDate = DateTime.UtcNow
            };

            _mockExchangeRateService.Setup(x => x.GetExchangeRateAsync(request.FromCurrency, request.ToCurrency))
                .ReturnsAsync(exchangeRate);
            _mockRepository.Setup(x => x.AddAsync(It.IsAny<ConversionHistory>()))
                .ReturnsAsync(new ConversionHistory());

           
            var result = await _service.ConvertCurrencyAsync(request);

            
            Assert.NotNull(result);
            Assert.Equal(request.Amount, result.FromAmount);
            Assert.Equal(request.FromCurrency, result.FromCurrency);
            Assert.Equal(request.ToCurrency, result.ToCurrency);
            Assert.Equal(exchangeRate, result.ExchangeRate);
            Assert.Equal(request.Amount * exchangeRate, result.ToAmount);
            
            _mockExchangeRateService.Verify(x => x.GetExchangeRateAsync(request.FromCurrency, request.ToCurrency), Times.Once);
            _mockRepository.Verify(x => x.AddAsync(It.IsAny<ConversionHistory>()), Times.Once);
        }

        [Fact]
        public async Task GetConversionHistoryAsync_ValidUserId_ReturnsHistory()
        {
            
            var userId = "test-user";
            var history = _fixture.CreateMany<ConversionHistory>(3).ToList();
            var expectedDtos = _fixture.CreateMany<ConversionHistoryDto>(3).ToList();

            _mockRepository.Setup(x => x.GetByUserIdAsync(userId))
                .ReturnsAsync(history);
            _mockMapper.Setup(x => x.Map<ConversionHistoryDto>(It.IsAny<ConversionHistory>()))
                .Returns((ConversionHistory h) => expectedDtos.First());

           
            var result = await _service.GetConversionHistoryAsync(userId);

           
            Assert.NotNull(result);
            Assert.Equal(3, result.Count());
            
            _mockRepository.Verify(x => x.GetByUserIdAsync(userId), Times.Once);
        }

        [Fact]
        public async Task ConvertCurrencyAsync_SameCurrency_ReturnsOneToOneRate()
        {
            
            var request = _fixture.Create<ConversionRequestDto>();
            request.FromCurrency = "USD";
            request.ToCurrency = "USD";
            request.Amount = 100m;

            _mockExchangeRateService.Setup(x => x.GetExchangeRateAsync(request.FromCurrency, request.ToCurrency))
                .ReturnsAsync(1m);
            _mockRepository.Setup(x => x.AddAsync(It.IsAny<ConversionHistory>()))
                .ReturnsAsync(new ConversionHistory());

            
            var result = await _service.ConvertCurrencyAsync(request);

       
            Assert.Equal(request.Amount, result.ToAmount);
            Assert.Equal(1m, result.ExchangeRate);
        }
    }
}
