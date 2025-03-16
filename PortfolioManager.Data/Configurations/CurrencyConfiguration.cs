using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PortfolioManager.Base.Entities;

namespace PortfolioManager.Data.Configurations;

public class CurrencyConfiguration : IEntityTypeConfiguration<CurrencyEntity>
{
    public void Configure(EntityTypeBuilder<CurrencyEntity> builder)
    {
        builder.Property(p => p.ExchangeRate).HasColumnType("decimal(18,2)");
        builder.Property(p => p.Name).HasMaxLength(20);
        builder.HasQueryFilter(p => !p.IsDeleted);
    }
}
