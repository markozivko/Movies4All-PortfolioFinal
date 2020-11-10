using System;
using AutoMapper;
using DataServiceLibrary.Models;
namespace WebService.Models.Profiles
{
    public class TitleProfile: Profile
    {
        public TitleProfile()
        {

            CreateMap<TitleBasics, TitleDto>();
        }
    }
}
