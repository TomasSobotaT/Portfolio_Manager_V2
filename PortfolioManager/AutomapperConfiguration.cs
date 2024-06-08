using AutoMapper;
using PortfolioManager.Base.Entities;
using PortfolioManager.Base.Enums;
using PortfolioManager.Models.Models;
using PortfolioManager.Models.Models.User;

namespace PortfolioManager;

public class AutomapperConfiguration : Profile
{
    public AutomapperConfiguration()
	{
        CreateMap<CommodityEntity, Commodity>().ReverseMap();

        CreateMap<RegisterUser, UserEntity>();
        CreateMap<ErrorLogEntity, ErrorLog>().ReverseMap();
        CreateMap<EventLogEntity, EventLog>().ReverseMap();
        CreateMap<RecordEditModel, RecordEntity>();
        CreateMap<RecordEntity, Record>()
            .ForMember(dest => dest.PriceCzck, opt => opt.MapFrom(src => src.Commodity.Price.PriceCzk))
            .ForMember(dest => dest.PriceUsd, opt => opt.MapFrom(src => src.Commodity.Price.PriceUsd));
    }
}
