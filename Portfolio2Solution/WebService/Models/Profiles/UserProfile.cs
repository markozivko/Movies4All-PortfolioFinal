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
                    dest.Address,
                    opt => opt.MapFrom(src => src.Address.StreetNumber + " " + src.Address.StreetName + " " + src.Address.ZipCode + " "+ src.Address.City + " " + src.Address.Country))
            .ForMember(dest =>
                    dest.Role,
                    opt => opt.MapFrom(src => src.IsStaff == true ? "staff" : "customer"));
            CreateMap<User, UserListDto>()
                .ForMember(dest =>
                    dest.FullName,
                    opt => opt.MapFrom(src => src.FirstName + " " + src.LastName))
                .ForMember(dest =>
                    dest.Role,
                    opt => opt.MapFrom(src => src.IsStaff == true ? "staff" : "customer"));
            CreateMap<UserForCreationOrUpdateDto, User>()
                .ForMember(dest => dest.BirthDay ,opt => opt.MapFrom(src => PrettyDate(Convert.ToDateTime(src.BirthDay))));

            CreateMap<UserForCreationOrUpdateDto, Address>();
        }

        public string PrettyDate(DateTime d)
        {
            var pretty = $"{d.Year}-{d.Month}-{d.Day}";
            return pretty;
        }
    }
}
