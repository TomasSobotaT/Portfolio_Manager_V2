using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PortfolioManager.Base.Entities;

namespace PortfolioManager.Data.Configurations;
public class RecordConfiguration : IEntityTypeConfiguration<RecordEntity>
{
    public void Configure(EntityTypeBuilder<RecordEntity> builder)
    {
        builder.HasOne(r => r.User)
            .WithMany(u => u.Records)
            .HasForeignKey(r => r.UserId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.Property(p => p.Amount).HasColumnType("decimal(18,10)");
        builder.Property(p => p.Note).HasMaxLength(160);

        builder.HasQueryFilter(p => !p.IsDeleted);
    }
}
