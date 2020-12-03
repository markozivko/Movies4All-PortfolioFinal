using System;
using AutoMapper;
using DataServiceLibrary.Models;

namespace WebService.Models.Profiles
{
    public class LatestTitlesProfile: Profile
    {
        public LatestTitlesProfile()
        {
            CreateMap<TitleRating, LatestTitlesDetailsDto>()
              .ForMember(dest => dest.Rating, opt => opt.MapFrom(src => src.Average))
              .ForMember(dest => dest.NumVotes, opt => opt.MapFrom(src => src.NumVotes));

            CreateMap<OmdbData, LatestTitlesDetailsDto>()
                .ForMember(dest => dest.Plot, opt => opt.MapFrom(src => src.Plot));
        }
    }
}
