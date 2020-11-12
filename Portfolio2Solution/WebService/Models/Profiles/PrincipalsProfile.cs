using System;
using AutoMapper;
using DataServiceLibrary.Models;

namespace WebService.Models.Profiles
{
    public class PrincipalsProfile: Profile
    {
        public PrincipalsProfile()
        {

            CreateMap<TitlePrincipal, PrincipalsDto>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Person.Name))
                .ForMember(dest => dest.CharacterName, opt => opt.MapFrom(src => src.Characters));
        }
    }
}
