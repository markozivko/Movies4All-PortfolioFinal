using System;
using AutoMapper;
namespace WebService.Models.Profiles
{
    public class TitleProfile: Profile
    {
        public TitleProfile()
        {

            CreateMap<TitleDto, TitleDetailsDto>();
        }
    }
}
