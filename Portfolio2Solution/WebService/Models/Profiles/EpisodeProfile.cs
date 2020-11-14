using System;
using AutoMapper;
using DataServiceLibrary.Models;

namespace WebService.Models.Profiles
{
    public class EpisodeProfile: Profile
    {
        public EpisodeProfile()
        {

            CreateMap<Episode, EpisodeDto>()
                .ForMember(dest => dest.EpisodeNumber, opt => opt.MapFrom(src => src.NumEpisode))
                .ForMember(dest => dest.SeasonNumber, opt => opt.MapFrom(src => src.Season))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title.PrimaryTitle));
        }
    }
}
