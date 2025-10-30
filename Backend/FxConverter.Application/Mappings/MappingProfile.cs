using AutoMapper;
using FxConverter.Application.DTOs;
using FxConverter.Domain.Entities;

namespace FxConverter.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ConversionHistory, ConversionHistoryDto>();
            CreateMap<Currency, CurrencyDto>();
        }
    }
}
