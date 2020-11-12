using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using DataServiceLibrary;
using DataServiceLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using WebService.Models;

namespace WebService.Controllers
{
    [ApiController]
    [Route("api/titlebookmarks")]
    public class TitleBookmarkController:  ControllerBase
    {

        IDataService _dataService;
        IMapper _mapper;

        public TitleBookmarkController(IDataService dataService, IMapper mapper)
        {

            _dataService = dataService;
            _mapper = mapper;

        }

        [HttpGet("{id}", Name = nameof(GetTitleBookmarksForUser))]
        public IActionResult GetTitleBookmarksForUser(int id)
        {

            var titleBookmarks = _dataService.GetTitleBookmarkForUser(id);
            var result = CreateResult(titleBookmarks);

            return Ok(result);
        }

        private TitleBookmarkDto CreateTitleBookmarkElementDto(TitleBookmark tb)
        {
            var tbDto = _mapper.Map<TitleBookmarkDto>(tb);
            tbDto.FavoriteTitleUrl = Url.Link(nameof(TitleController.GetTitle), new { Id = tb.TitleConst });
            return tbDto;
        }

        private object CreateResult(IList<TitleBookmark> tb)
        {
            var items = tb.Select(CreateTitleBookmarkElementDto);

            return new { items };
        }
    }
}
