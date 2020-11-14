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
            try
            {
                if (Program.CurrentUser == null)
                {
                    return Unauthorized();
                }
                else
                {
                    if (Program.CurrentUser.UserId == id)
                    {
                        var user = _dataService.GetUser(id);

                        var userDto = _mapper.Map<UserDto>(user);
                        userDto.TitleBookMarksUrl = Url.Link(nameof(TitleBookmarkController.GetTitleBookmarksForUser), new { Id = user.UserId });
                        userDto.PersonalitiesUrl = Url.Link(nameof(PersonalitiesController.GetPersonalitiesForUser), new { Id = user.UserId });
                        userDto.SearchHistoryUrl = Url.Link(nameof(SearchHistoryController.GetSearchHistoryForUser), new { Id = user.UserId });

                        return Ok(userDto);
                    }
                    else
                    {
                        return Unauthorized();
                    }
                }

            }
            catch (ArgumentException)
            {
                return Unauthorized();
            }
            
        }

        [HttpGet]
        public IActionResult GetUsers()
        {
            try
            {
                if (!_dataService.CheckUserRole(Program.CurrentUser.UserId).IsStaff)
                {
                    return Unauthorized();
                }

                var users = _dataService.GetUsers();
                var result = CreateResult(users);
                return Ok(result);
            }
            catch (ArgumentException)
            {
                return Unauthorized();
            }
          
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

        [HttpPut("{id}")]
        public IActionResult UpdateUser(int id, UserForCreationOrUpdateDto UserOrUpdateDto)
        {

            try
            {
                if (Program.CurrentUser == null)
                {
                    return Unauthorized();
                }

                var user = _mapper.Map<User>(UserOrUpdateDto);
                var address = _mapper.Map<Address>(UserOrUpdateDto);

                if (!_dataService.UserUpdate(id, user))
                {
                    return NotFound();
                }
                else
                {
                    if (!_dataService.UserUpdateAddress(id, address))
                    {
                        return NotFound();

                    }
                }

                return NoContent();
            }
            catch (ArgumentException)
            {
                return Unauthorized();
            }
        }

        [HttpDelete("{id}")]
        public IActionResult UnsubscribeUser(int id)
        {

            try
            {
                if (Program.CurrentUser == null)
                {
                    return Unauthorized();
                }

                if (!_dataService.UnsubsribeUser(id))
                {
                    return NotFound();
                }

                return NoContent();
            }
            catch (ArgumentException)
            {
                return Unauthorized();
            }
            
        }

    }
}
