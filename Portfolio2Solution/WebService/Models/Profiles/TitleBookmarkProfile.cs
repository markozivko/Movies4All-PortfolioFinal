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
        }
    }
}
