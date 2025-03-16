﻿using AutoMapper;
using PortfolioManager.Base.Entities;
using PortfolioManager.Models.Models.Commodity;
using PortfolioManager.Models.Models.Currency;
using PortfolioManager.Models.Models.File;
using PortfolioManager.Models.Models.Record;
using PortfolioManager.Models.Models.User;

namespace PortfolioManager.Configurations;

public class AutomapperConfiguration : Profile
{
    public AutomapperConfiguration()
    {
        CreateMap<RegisterUser, UserEntity>();

        CreateMap<CommodityEditModel, CommodityEntity>();
        CreateMap<CommodityEntity, Commodity>();
        CreateMap<Commodity, CommodityEditModel>();

        CreateMap<CurrencyEditModel, CurrencyEntity>();
        CreateMap<CurrencyEntity, Currency>();
        CreateMap<Currency, CurrencyEditModel>();

        CreateMap<RecordEditModel, RecordEntity>();
        CreateMap<RecordEntity, Record>();

        CreateMap<UserDocumentEntity, UserDocument>();
        CreateMap<UserDocumentEntity, UserDocumentOutputModel>();

        CreateMap<UserEditModel, UserEntity>();
        CreateMap<UserEntity, User>();
        CreateMap<User, UserEditModel>();
    }
}
