using System;
using AutoMapper;
using DataServiceLibrary.Models;

namespace WebService.Models.Profiles
{
    public class UserRatingsProfile: Profile
    {
       public UserRatingsProfile()
        {
            CreateMap<UserRates, UserRatingsDto>()
                .ForMember(dest => dest.Rating, opt => opt.MapFrom(src => src.NumericR))
                .ForMember(dest => dest.RatingDescription, opt => opt.MapFrom(src => src.VerbalR))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title.Title.PrimaryTitle));


        }
    }
}
