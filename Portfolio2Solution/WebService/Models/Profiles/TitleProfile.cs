using System;
using System.Linq;
using AutoMapper;
using DataServiceLibrary.Models;
namespace WebService.Models.Profiles
{
    public class TitleProfile : Profile
    {
        public TitleProfile()
        {

            CreateMap<TitleGenre, TitleDto>()
                .ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre.Name));


            CreateMap<TitleBasics, TitleDto>();
            //.ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.TitleGenres.FirstOrDefault().Genre.Name));

            CreateMap<TitleGenre, TitleListDto>()
            .ForMember(dest => dest.PrimaryTitle, opt => opt.MapFrom(src => src.Title.PrimaryTitle))
            .ForMember(dest => dest.StartYear, opt => opt.MapFrom(src => src.Title.StartYear));
            //.ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre.Name));
        }
    }
}
