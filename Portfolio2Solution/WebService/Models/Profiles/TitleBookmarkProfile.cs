using System;
using AutoMapper;
using DataServiceLibrary.Models;

namespace WebService.Models.Profiles
{
    public class TitleBookmarkProfile: Profile
    {
        public TitleBookmarkProfile()
        {

            CreateMap<TitleBookmark, TitleBookmarkDto>();
            CreateMap<TitleBookmarkForCreationOrUpdateDto, TitleBookmark>()
                .ForMember(dest => dest.TitleConst, opt => opt.MapFrom(src => src.Title));
         
        }
    }
}
