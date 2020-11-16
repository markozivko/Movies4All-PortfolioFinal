using AutoMapper;
using DataServiceLibrary.FromSQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebService.Models.Profiles
{
    public class SimpleSearchProfile : Profile
    {
        public SimpleSearchProfile()
        {
            CreateMap<SimpleSearch, SimpleSearchDto>()
                .ForMember(dest => dest.Search, opt => opt.MapFrom(src => src.PrimaryTitle));

        }
    }
}
