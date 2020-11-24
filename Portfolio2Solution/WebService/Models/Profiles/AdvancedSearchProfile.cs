using System;
using AutoMapper;
using DataServiceLibrary.FromSQL;

namespace WebService.Models.Profiles
{
    public class AdvancedSearchProfile: Profile
    {
        public AdvancedSearchProfile()
        {
            CreateMap<StructuredSearch, AdvancedSearchDto>()
                .ForMember(dest => dest.PrimaryTitle, opt => opt.MapFrom(src => src.PrimaryTitle));
        }
    }
}
