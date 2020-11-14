using AutoMapper;
using DataServiceLibrary.Models;
namespace WebService.Models.Profiles
{
    public class SearchHistoryProfile: Profile
    {
        public SearchHistoryProfile()
        {
            CreateMap<SearchHistory, SearchHistoryDto>();
        }

    }
}
