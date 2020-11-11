using AutoMapper;
using DataServiceLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebService.Models.Profiles
{
    public class UserProfile: Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDto>()
                .ForMember(dest =>
                    dest.FName,
                    opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest =>
                    dest.LName,
                    opt => opt.MapFrom(src => src.LastName))
                .ForMember(dest =>
                    dest.UName,
                    opt => opt.MapFrom(src => src.UserName));
        }
    }
}
