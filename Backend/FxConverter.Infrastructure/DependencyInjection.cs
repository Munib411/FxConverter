using FxConverter.Application.Interfaces;
using FxConverter.Infrastructure.Data;
using FxConverter.Infrastructure.Repositories;
using FxConverter.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Refit;

namespace FxConverter.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<FxConverterDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IConversionRepository, ConversionRepository>();

            services.AddRefitClient<IOpenExchangeRatesApi>()
                .ConfigureHttpClient(c => c.BaseAddress = new Uri("https://openexchangerates.org/api"));

            services.AddScoped<IExchangeRateService, ExchangeRateService>();

            services.AddMemoryCache();

            return services;
        }
    }
}
