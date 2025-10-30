using FxConverter.Application.Interfaces;
using FxConverter.Domain.Entities;
using FxConverter.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FxConverter.Infrastructure.Repositories
{
    public class ConversionRepository : IConversionRepository
    {
        private readonly FxConverterDbContext _context;

        public ConversionRepository(FxConverterDbContext context)
        {
            _context = context;
        }

        public async Task<ConversionHistory> AddAsync(ConversionHistory conversion)
        {
            _context.ConversionHistories.Add(conversion);
            await _context.SaveChangesAsync();
            return conversion;
        }

        public async Task<IEnumerable<ConversionHistory>> GetByUserIdAsync(string userId)
        {
            return await _context.ConversionHistories
                .Where(c => c.UserId == userId)
                .OrderByDescending(c => c.ConversionDate)
                .ToListAsync();
        }

        public async Task<ConversionHistory?> GetByIdAsync(Guid id)
        {
            return await _context.ConversionHistories
                .FirstOrDefaultAsync(c => c.ConversionId == id);
        }
    }
}
