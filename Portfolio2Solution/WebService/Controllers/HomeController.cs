using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using DataServiceLibrary;
using DataServiceLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using WebService.Models;

namespace WebService.Controllers
{
    [ApiController]
    [Route("api")]
    public class HomeController: ControllerBase
    {

        IDataService _dataService;
        IMapper _mapper;
        private const int MaxPageSize = 25;

        public HomeController(IDataService dataService, IMapper mapper)
        {
            _dataService = dataService;
            _mapper = mapper;
        }

        [HttpGet("popular/{id}", Name = nameof(GetPopularTitle))]
        public IActionResult GetPopularTitle(string id)
        {
                var titles = _dataService.GetTitle(id);

                if (titles == null)
                {
                    return NotFound();
                }

                var dto = _mapper.Map<TitleDto>(titles);
                dto.DetailsUrl = Url.Link(nameof(PopularTitlesController.GetPopularTitleDetails), new { Id = titles.Const.Replace(" ", String.Empty) });

                return Ok(dto);
        }


        [HttpGet("popular", Name = nameof(GetPopularTitles))]
        public IActionResult GetPopularTitles(int page = 0, int pageSize = 12)
        {
            var titles = _dataService.GetPopularTitles(page, pageSize);
            if (titles == null)
            {
                return NotFound();
            }

            var result = CreateResult(titles, page, pageSize);
            return Ok(result);
        }
        private TitleDto CreateTitleElementDto(TitleBasics title)
        {
            var dto = _mapper.Map<TitleDto>(title);
            dto.DetailsUrl = Url.Link(nameof(PopularTitlesController.GetPopularTitleDetails), new { Id = title.Const.Replace(" ", String.Empty) });
            return dto;
        }

        private object CreateResult(IList<TitleBasics> titles,int page, int pageSize)
        {
            var items = titles.Select(CreateTitleElementDto);

            var count = _dataService.NumberOfPopularTitles();

            string prev = null;

            if (page > 0)
            {
                prev = Url.Link(nameof(GetPopularTitles), new { page = page - 1, pageSize });
            }

            string next = null;

            if (page < (int)Math.Ceiling((double)count / pageSize) - 1)
            {
                next = Url.Link(nameof(GetPopularTitles), new { page = page + 1, pageSize });
            }

            var cur = Url.Link(nameof(GetPopularTitles), new { page, pageSize });

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

        [HttpPost]
        public IActionResult CreateUser(UserForCreationOrUpdateDto newuser)
        {
            var user = _mapper.Map<User>(newuser);
            var address = _mapper.Map<Address>(newuser);
            var isEmailAvailable = _dataService.IsEmailAvailable(newuser.Email);
            if (isEmailAvailable)
            {
                _dataService.CreateNewUser(user, address);
                user.Address = address;
                var result = _mapper.Map<UserDto>(user);
                return Created("", result);
            }
            return BadRequest();
        }


        [HttpGet("login", Name = nameof(Login))]
        public IActionResult Login(string email, string password)
        {

            var user = _dataService.Login(email, password);
            if (user == null)
            {
                return NotFound();
            }
            var userDto = _mapper.Map<UserDto>(user);
            userDto.TitleBookMarksUrl = Url.Link(nameof(TitleBookmarkController.GetTitleBookmarksForUser), new { Id = user.UserId });
            userDto.PersonalitiesUrl = Url.Link(nameof(PersonalitiesController.GetPersonalitiesForUser), new { Id = user.UserId });
            userDto.SearchHistoryUrl = Url.Link(nameof(SearchHistoryController.GetSearchHistoryForUser), new { Id = user.UserId });
            userDto.UserRatingsUrl = Url.Link(nameof(UserRatingsController.GetRatingsForUser), new { Id = user.UserId });
            return Ok(userDto);

        }
    }
}
