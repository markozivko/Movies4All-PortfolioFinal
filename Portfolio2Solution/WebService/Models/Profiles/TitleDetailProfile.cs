using DataServiceLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;

namespace WebService.Models.Profiles
{
    public class TitleDetailProfile : Profile
    {
        public TitleDetailProfile()
        {
            CreateMap<TitleRating, TitleDetailsDto>()
                .ForMember(dest => dest.Rating, opt => opt.MapFrom(src => src.Average))
                .ForMember(dest => dest.NumVotes, opt => opt.MapFrom(src => src.NumVotes));

            CreateMap<OmdbData, TitleDetailsDto>()
                .ForMember(dest => dest.Plot, opt => opt.MapFrom(src => src.Plot));

            



        }
    }
}
