using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using DataServiceLibrary.Models;
namespace WebService.Models.Profiles
{
    public class TitleProfile : Profile
    {
        public TitleProfile()
        {

            CreateMap<TitleBasics, TitleDto>()
                .ForMember(dest => dest.Genre, opt => opt.MapFrom(src => Flatten(src.TitleGenres)));


            CreateMap<TitleGenre, TitleListDto>()
            .ForMember(dest => dest.PrimaryTitle, opt => opt.MapFrom(src => src.Title.PrimaryTitle))
            .ForMember(dest => dest.StartYear, opt => opt.MapFrom(src => src.Title.StartYear));
            //.ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre.Name));
        }

        public string Flatten(ICollection<TitleGenre> genres)
        {
            List<string> str = new List<string>();

            foreach (var i in genres)
            {
                str.Add(i.Genre.Name);
            }

            return string.Join(", ", str);
        }
    }
}
