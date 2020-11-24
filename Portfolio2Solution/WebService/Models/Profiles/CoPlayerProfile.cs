using System;
using AutoMapper;
using DataServiceLibrary.FromSQL;

namespace WebService.Models.Profiles
{
    public class CoPlayerProfile: Profile
    {
        public CoPlayerProfile()
        {
            CreateMap<CoPlayers, CoPlayersDto>(); 
        }
    }
}
