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
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title.Title.PrimaryTitle))
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => PrettyDate(src.Date)));
            CreateMap<UserRatingForCreationOrUpdateDto, UserRates>()
                .ForMember(dest => dest.NumericR, opt => opt.MapFrom(src => src.Rating))
                .ForMember(dest => dest.TitleConst, opt => opt.MapFrom(src => src.TitleId));


        }

        public string PrettyDate(DateTime d)
        {
            var pretty = $"{d.Year}-{d.Month}-{d.Day} {d.Hour}:{d.Minute}:{d.Second}";
            return pretty;
        }
    }
}
