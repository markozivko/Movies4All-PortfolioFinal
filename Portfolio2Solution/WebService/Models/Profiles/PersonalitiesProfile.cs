using System;
using AutoMapper;
using DataServiceLibrary.Models;

namespace WebService.Models.Profiles
{
    public class PersonalitiesProfile: Profile
    {
        public PersonalitiesProfile()
        {

            CreateMap<Personalities, PersonalitiesDto>();
            CreateMap<PersonForCreateOrUpdateDto, Personalities>()
                .ForMember(dest => dest.NameConst, opt => opt.MapFrom(src => src.Name));
        }
    }
}
