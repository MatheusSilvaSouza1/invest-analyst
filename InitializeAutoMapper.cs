using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using invest_analyst.Domain;
using invest_analyst.Infra.ClientsHttp.Models;

namespace invest_analyst
{
    public static class InitializeAutoMapper
    {
        public static MapperConfiguration Initialize()
        {
            return new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Dy, AssetEarningsYearlyModelDTO>()
                    .ForMember(dest => dest.Rank, opt => opt.MapFrom(src => src.Rank))
                    .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.Value))
                    .ReverseMap()
                ;

                cfg.CreateMap<Prices, PricesDTO>()
                    .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Date))
                    .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
                    .ReverseMap()
                ;
            });
        }
    }
}