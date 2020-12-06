using AutoMapper;
using DataServiceLibrary.Models;

namespace WebService.Models.Profiles
{
    public class GenreProfile: Profile
    {
        public GenreProfile()
        {

            CreateMap<Genre, GenresDto>()
                .ForMember(dest => dest.genre, opt => opt.MapFrom(src => src.Name));

        }
    }
}
