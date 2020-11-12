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
    [Route("api/users")]
    public class UserController : ControllerBase
    {
        IDataService _dataService;
        IMapper _mapper;
        private const int MaxPageSize = 25;

        public UserController(IDataService dataService, IMapper mapper)
        {
            _dataService = dataService;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public IActionResult GetUser(int id)
        {

            var user = _dataService.GetUser(id);

            if (user == null) {
                return NotFound();
            }
            var userDto = _mapper.Map<UserDto>(user);
            userDto.TitleBookMarksUrl = Url.Link(nameof(TitleBookmarkController.GetTitleBookmarksForUser), new { Id = user.UserId});
            userDto.PersonalitiesUrl = Url.Link(nameof(PersonalitiesController.GetPersonalitiesForUser), new { Id = user.UserId });

            return Ok(userDto);
        }

        [HttpGet]
        public IActionResult GetUsers() 
        {
            var users = _dataService.GetUsers();
            var result = CreateResult(users);
            return Ok(result);
        }
        private UserListDto CreateUserElementDto(User user)
        {
            var dto = _mapper.Map<UserListDto>(user);
            return dto;
        }

  
        private object CreateResult(IList<User> users)
        {
            var items = users.Select(CreateUserElementDto);

            return new { items };
        }

    }
}
