using AutoMapper;
using MT.OnlineRestaurant.BuisnessEntities;
using MT.OnlineRestaurant.DataLayer.EntityFrameWorkModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace MT.OnlineRestaurant.BusinessLayer
{
   public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<RatingandReviewDetails, TblRatingandReviews>()
                .ForMember(dest => dest.Rating, opt => opt.MapFrom(src => src.Rating))
                .ForMember(dest => dest.Reviews, opt => opt.MapFrom(src => src.Reviews))
                .ForMember(dest => dest.TblRestaurantId, opt => opt.MapFrom(src => src.TblRestaurantId))
                .ForMember(dest => dest.TblCustomerId, opt => opt.MapFrom(src => src.TblCustomerId));


        }
        
    }
}
