using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PortfolioManager.Base.Entities;
namespace PortfolioManager.Data.Context;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : IdentityDbContext<UserEntity, IdentityRole<int>, int>(options), IApplicationDbContext
{
    public DbSet<CommodityEntity> Commodities { get; set; }

    public DbSet<PriceEntity> Prices { get; set; }

    public DbSet<RecordEntity> Records { get; set; }

    public DbSet<ErrorLogEntity> ErrorLogs { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<BaseEntity>().UseTpcMappingStrategy();

        modelBuilder.Entity<RecordEntity>(entity =>
        {
            entity.HasBaseType<BaseEntity>();

            entity.HasOne(r => r.User)
                .WithMany(u => u.Records)
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(r => r.Commodity)
                .WithMany(c => c.Records)
                .HasForeignKey(r => r.CommodityId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.Property(p => p.Amount).HasColumnType("decimal(18,2)");
        });

        modelBuilder.Entity<CommodityEntity>(entity =>
        {
            entity.HasBaseType<BaseEntity>();

            entity.HasOne(c => c.Price)
                .WithOne(p => p.Commodity)
                .HasForeignKey<CommodityEntity>(c => c.PriceId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<PriceEntity>(entity =>
        {
            entity.HasBaseType<BaseEntity>();

            entity.Property(p => p.PriceCzk).HasColumnType("decimal(18,2)");
            entity.Property(p => p.PriceUsd).HasColumnType("decimal(18,2)");
        });
    }
}

