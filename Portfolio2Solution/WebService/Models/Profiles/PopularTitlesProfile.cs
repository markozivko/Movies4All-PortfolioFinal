using AutoMapper;
using DataServiceLibrary.Models;

namespace WebService.Models.Profiles
{
    public class PopularTitlesProfile: Profile
    {
        public PopularTitlesProfile()
        {
            CreateMap<TitleRating, PopularTitlesDto>()
               .ForMember(dest => dest.Rating, opt => opt.MapFrom(src => src.Average))
               .ForMember(dest => dest.NumVotes, opt => opt.MapFrom(src => src.NumVotes));

            CreateMap<OmdbData, PopularTitlesDto>()
                .ForMember(dest => dest.Plot, opt => opt.MapFrom(src => src.Plot));
        }
    }
}
