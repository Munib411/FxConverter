using AutoFixture;
using FxConverter.Application.Interfaces;
using FxConverter.Domain.Entities;
using FxConverter.Infrastructure.Data;
using FxConverter.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace FxConverter.Tests.Repositories
{
    public class ConversionRepositoryTests : IDisposable
    {
        private readonly FxConverterDbContext _context;
        private readonly IConversionRepository _repository;
        private readonly Fixture _fixture;

        public ConversionRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<FxConverterDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new FxConverterDbContext(options);
            _repository = new ConversionRepository(_context);
            _fixture = new Fixture();
        }

        [Fact]
        public async Task AddAsync_ValidConversion_ReturnsConversion()
        {
            var conversion = _fixture.Create<ConversionHistory>();
            conversion.ConversionId = Guid.NewGuid();

            var result = await _repository.AddAsync(conversion);

            Assert.NotNull(result);
            Assert.Equal(conversion.ConversionId, result.ConversionId);
            Assert.Equal(conversion.FromCurrency, result.FromCurrency);
            Assert.Equal(conversion.ToCurrency, result.ToCurrency);
        }

        [Fact]
        public async Task GetByUserIdAsync_ValidUserId_ReturnsConversions()
        {
            var userId = "test-user";
            var conversions = _fixture.CreateMany<ConversionHistory>(3).ToList();
            foreach (var conversion in conversions)
            {
                conversion.UserId = userId;
                conversion.ConversionId = Guid.NewGuid();
                await _repository.AddAsync(conversion);
            }

            var result = await _repository.GetByUserIdAsync(userId);

            Assert.NotNull(result);
            Assert.Equal(3, result.Count());
            Assert.All(result, c => Assert.Equal(userId, c.UserId));
        }

        [Fact]
        public async Task GetByIdAsync_ValidId_ReturnsConversion()
        {
            var conversion = _fixture.Create<ConversionHistory>();
            conversion.ConversionId = Guid.NewGuid();
            await _repository.AddAsync(conversion);

           
            var result = await _repository.GetByIdAsync(conversion.ConversionId);

            
            Assert.NotNull(result);
            Assert.Equal(conversion.ConversionId, result.ConversionId);
        }

        [Fact]
        public async Task GetByIdAsync_InvalidId_ReturnsNull()
        {
            
            var invalidId = Guid.NewGuid();

            
            var result = await _repository.GetByIdAsync(invalidId);

            
            Assert.Null(result);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
