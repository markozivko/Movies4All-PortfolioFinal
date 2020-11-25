using AutoMapper;
using DataServiceLibrary.FromSQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebService.Models.Profiles
{
    public class ActorsProfile : Profile
    {
        public ActorsProfile()
        {
            CreateMap<Actors, ActorsDto>();
        }
    }
}
