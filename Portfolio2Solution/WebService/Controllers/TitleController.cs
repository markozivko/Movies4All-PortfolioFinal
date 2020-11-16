using System;
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
    [Route("api/titles")]
    public class TitleController : ControllerBase
    {
        readonly IDataService _dataService;
        private readonly IMapper _mapper;
        private const int MaxPageSize = 25;
        public TitleController(IDataService dataService, IMapper mapper)
        {
            _dataService = dataService;
            _mapper = mapper;
        }

 
        [HttpGet("{id}", Name = nameof(GetTitle))]
        public IActionResult GetTitle(string id)
        {

            try
            {
                if (Program.CurrentUser == null)
                {
                    return Unauthorized();
                } 

                var titles = _dataService.GetTitle(id);

                if (titles == null)
                {
                    return NotFound();
                }

                var dto = _mapper.Map<TitleDto>(titles);
                dto.DetailsUrl = Url.Link(nameof(TitleDetailsController.GetTitleDetails), new { Id = titles.Const.Replace(" ", String.Empty) });

                return Ok(dto);
            } catch (ArgumentException)
            {
                return Unauthorized();
            }
            
        }
        [HttpGet("category/{id}", Name = nameof(GetTitlesByCategory))]
        public IActionResult GetTitlesByCategory(int id, int page = 0, int pageSize = 10)
        {
            try
            {
                if (Program.CurrentUser == null)
                {
                    return Unauthorized();
                }

                pageSize = pageSize > MaxPageSize ? MaxPageSize : pageSize;

                var titles = _dataService.GetTitleByGenre(id, page, pageSize);

                if (titles == null)
                {
                    return NotFound();
                }

                var result = CreateResult(titles, id, page, pageSize);
                return Ok(result);
            }
            catch (ArgumentException)
            {
                return Unauthorized();
            }
            
        }
        private TitleListDto CreateTitleElementDto(TitleGenre title)
        {
            var dto = _mapper.Map<TitleListDto>(title);
            return dto;
        }


        private object CreateResult(IList<TitleGenre> titles, int id, int page, int pageSize)
        {
            var items = titles.Select(CreateTitleElementDto);

            var count = _dataService.NumberOfTitleByGenre(id);

            string prev = null;

            if (page > 0)
            {
                prev = Url.Link(nameof(GetTitlesByCategory), new { page = page - 1, pageSize });
            }

            string next = null;

            if (page < (int)Math.Ceiling((double)count / pageSize) - 1)
            {
                next = Url.Link(nameof(GetTitlesByCategory), new { page = page + 1, pageSize });
            }

            var cur = Url.Link(nameof(GetTitlesByCategory), new { page, pageSize });

            var result = new
            {
                prev,
                next,
                cur,
                count,
                items
            };

            return result;
        }


        [HttpPost("{id}")]
        public IActionResult AddTitleBookmarksForUser(TitleBookmarkForCreationOrUpdateDto td)
        {
            try
            {
                if (Program.CurrentUser == null)
                {
                    return Unauthorized();
                } 

                _dataService.UserAddTitleBookmark(Program.CurrentUser.UserId, td.TitleId, td.Notes);

                var bookmark = _mapper.Map<TitleBookmark>(td);
                var result = _mapper.Map<TitleBookmarkDto>(bookmark);
                result.FavoriteTitleUrl = Url.Link(nameof(TitleController.GetTitle), new { Id = bookmark.TitleConst.Trim()});

                return Created("", result);
            }
            catch (ArgumentException)
            {
                return Unauthorized();
            } 
        }
        [HttpPost("Ratings/{id}")]
        public IActionResult AddRatingsForTitle(UserRatingForCreationOrUpdateDto ur) 
        {
            try
            {
                if (Program.CurrentUser == null)
                {
                    return Unauthorized();
                }
                _dataService.UserRatesTitles(ur.TitleId, Program.CurrentUser.UserId, ur.Rating);
                var uRating = _mapper.Map<UserRates>(ur);
                uRating.Date = _dataService.GetUserSpecificRating(Program.CurrentUser.UserId, ur.TitleId).Date;
                uRating.VerbalR = _dataService.GetUserSpecificRating(Program.CurrentUser.UserId, ur.TitleId).VerbalR;
                var result = _mapper.Map<UserRatingsDto>(uRating);
                result.Title = _dataService.GetTitleRating(ur.TitleId).Title.PrimaryTitle;
                
                return Created("", result);
            }
            catch (ArgumentException)
            {
                return Unauthorized();
            }
        }


    }
}
