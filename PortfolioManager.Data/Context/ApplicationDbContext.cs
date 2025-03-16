using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PortfolioManager.Base.Entities;

namespace PortfolioManager.Data.Context;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : IdentityDbContext<UserEntity, IdentityRole<int>, int>(options), IApplicationDbContext
{
    public DbSet<CommodityEntity> Commodities { get; set; }

    public DbSet<CurrencyEntity> Currencies { get; set; }

    public DbSet<RecordEntity> Records { get; set; }

    public DbSet<UserDocumentEntity> UserDocuments { get; set; }

    public DbSet<CommentEntity> Comments { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }
}

