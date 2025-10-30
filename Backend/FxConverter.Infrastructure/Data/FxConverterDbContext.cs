using FxConverter.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FxConverter.Infrastructure.Data
{
    public class FxConverterDbContext : DbContext
    {
        public FxConverterDbContext(DbContextOptions<FxConverterDbContext> options) : base(options)
        {
        }

        public DbSet<ConversionHistory> ConversionHistories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ConversionHistory>(entity =>
            {
                entity.HasKey(e => e.ConversionId);
                entity.Property(e => e.FromCurrency).HasMaxLength(3).IsRequired();
                entity.Property(e => e.ToCurrency).HasMaxLength(3).IsRequired();
                entity.Property(e => e.FromAmount).HasColumnType("decimal(18,4)");
                entity.Property(e => e.ToAmount).HasColumnType("decimal(18,4)");
                entity.Property(e => e.ExchangeRate).HasColumnType("decimal(18,6)");
                entity.Property(e => e.UserId).HasMaxLength(50).IsRequired();
                entity.Property(e => e.ConversionDate).IsRequired();
            });
        }
    }
}
