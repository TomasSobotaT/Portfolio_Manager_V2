using PortfolioManager.Base.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PortfolioManager.Data.Configurations;

public class CommodityConfiguration : IEntityTypeConfiguration<CommodityEntity>
{
    public void Configure(EntityTypeBuilder<CommodityEntity> builder)
    {
        builder.Property(p => p.ExchangeRate).HasColumnType("decimal(18,10)");
        builder.Property(p => p.Name).HasMaxLength(30);
        builder.HasQueryFilter(p => !p.IsDeleted);
    }
}
