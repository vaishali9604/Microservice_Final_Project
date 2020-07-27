using AutoMapper;
using MT.OnlineRestaurant.BusinessEntities;
using MT.OnlineRestaurant.DataLayer.EntityFrameWorkModel;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace MT.OnlineRestaurant.BusinessLayer
{
    [ExcludeFromCodeCoverage]
    public class MappingProfile : Profile
    {

        public MappingProfile()
        {
            CreateMap<UpdatePriceEntity, TblOffer>()
                .ForMember(dest => dest.TblRestaurantId, opt => opt.MapFrom(src => src.TblRestaurantId))
                .ForMember(dest => dest.TblMenuId, opt => opt.MapFrom(src => src.TblMenuId))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price));

            CreateMap<OrderMenus, OrderMenuMapping>()
                .ForMember(dest => dest.MenuId, opt => opt.MapFrom(src => src.MenuId));
           
        }
    }
}
