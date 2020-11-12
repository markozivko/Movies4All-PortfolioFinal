using AutoMapper;
using DataServiceLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebService.Models.Profiles
{
    public class PrincipalsProfile: Profile
    {
        public PrincipalsProfile()
        {
            CreateMap<TitlePrincipal, PrincipalsDto>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Person.Name));
        }
    }
}
