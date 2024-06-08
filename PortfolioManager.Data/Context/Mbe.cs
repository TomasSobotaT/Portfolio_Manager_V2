using Microsoft.EntityFrameworkCore;
using PortfolioManager.Base.Entities;

namespace PortfolioManager.Data.Context
{
    public static class Mbe
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<CommodityEntity>().HasData(
            //    new CommodityEntity
            //    {
            //        Id = 10,
            //        Name = "Zlato",
            //        PriceId = 100,
            //        CreatedDate = DateTime.Now,
            //        UpdatedDate = DateTime.Now,
            //        CreatedBy = "seed",
            //        UpdatedBy = "seed",
            //        IsDeleted = false
            //    },
            //    new CommodityEntity
            //    {
            //        Id = 2,
            //        Name = "Bitcoin",
            //        PriceId = 200,
            //        CreatedDate = DateTime.Now,
            //        UpdatedDate = DateTime.Now,
            //        CreatedBy = "seed",
            //        UpdatedBy = "seed",
            //        IsDeleted = false
            //    }
            //);

            //modelBuilder.Entity<PriceEntity>().HasData(
            //    new PriceEntity
            //    {
            //        Id = 1,
            //        PriceCzk = 40000.00m,
            //        PriceUsd = 1800.00m,
            //        CreatedDate = DateTime.Now,
            //        UpdatedDate = DateTime.Now,
            //        CreatedBy = "seed",
            //        UpdatedBy = "seed",
            //        IsDeleted = false
            //    },
            //    new PriceEntity
            //    {
            //        Id = 2,
            //        PriceCzk = 900000.00m,
            //        PriceUsd = 40000.00m,
            //        CreatedDate = DateTime.Now,
            //        UpdatedDate = DateTime.Now,
            //        CreatedBy = "seed",
            //        UpdatedBy = "seed",
            //        IsDeleted = false
            //    }
            //);
        }
    }
}
