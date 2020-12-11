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

        [HttpGet("{id}", Name = nameof(GetUser))]
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
                        userDto.UserUrl = Url.Link(nameof(UserController.GetUser), new { Id = id });
                        userDto.TitleBookMarksUrl = Url.Link(nameof(TitleBookmarkController.GetTitleBookmarksForUser), new { Id = user.UserId });
                        userDto.PersonalitiesUrl = Url.Link(nameof(PersonalitiesController.GetPersonalitiesForUser), new { Id = user.UserId });
                        userDto.SearchHistoryUrl = Url.Link(nameof(SearchHistoryController.GetSearchHistoryForUser), new { Id = user.UserId });
                        userDto.UserRatingsUrl = Url.Link(nameof(UserRatingsController.GetRatingsForUser), new { Id = user.UserId });
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

        [HttpGet(Name = nameof(GetUsers))]
        public IActionResult GetUsers(int page = 0, int pageSize = 10)
        {
            try
            {

                if (Program.CurrentUser == null)
                {
                    return Unauthorized();
                }else
                {

                    if (!_dataService.CheckUserRole(Program.CurrentUser.UserId).IsStaff)
                    {
                        return Unauthorized();
                    }

                    pageSize = pageSize > MaxPageSize ? MaxPageSize : pageSize;

                    var users = _dataService.GetUsers(page, pageSize);
                    var result = CreateResult(users, page, pageSize);
                    return Ok(result);
                }

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


        private object CreateResult(IList<User> users, int page, int pageSize)
        {
            var items = users.Select(CreateUserElementDto);

            var count = _dataService.NumberOfUsers();

            string prev = null;

            if (page > 0)
            {
                prev = Url.Link(nameof(GetUsers), new { page = page - 1, pageSize });
            }

            string next = null;

            if (page < (int)Math.Ceiling((double)count / pageSize) - 1)
            {
                next = Url.Link(nameof(GetUsers), new { page = page + 1, pageSize });
            }

            var cur = Url.Link(nameof(GetUsers), new { page, pageSize });

            var result = new
            {
                pageSizes = new int[] { 6, 12, 24, 48 },
                prev,
                next,
                cur,
                count,
                items
            };
            return result;
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
