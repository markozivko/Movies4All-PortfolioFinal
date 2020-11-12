using System;
using AutoMapper;
using DataServiceLibrary.Models;

namespace WebService.Models.Profiles
{
    public class PersonProfile: Profile
    {
        public PersonProfile()
        {
            CreateMap<Person, PersonDto>();
        }
    }
}
