using FxConverter.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FxConverter.Application.Interfaces
{
    public interface IConversionRepository
    {
        Task<ConversionHistory> AddAsync(ConversionHistory conversion);
        Task<IEnumerable<ConversionHistory>> GetByUserIdAsync(string userId);
        Task<ConversionHistory?> GetByIdAsync(Guid id);
    }
}
