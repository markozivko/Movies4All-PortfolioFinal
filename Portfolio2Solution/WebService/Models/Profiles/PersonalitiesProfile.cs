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
        }
    }
}
