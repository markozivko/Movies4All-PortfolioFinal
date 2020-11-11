using System;
using System.Collections.Generic;
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
            return Ok(userDto);
        }

        //private UserListDto CreateProductElementDto(User user)
        //{
        //    var dto = _mapper.Map<UserListDto>(user);
        //    dto.Url = Url.Link(nameof(GetUser), new { user.Id });
        //    return dto;
        //}
        ///*
        // *
        // * Helpers
        // */

        //private int CheckPageSize(int pageSize)
        //{
        //    return pageSize > MaxPageSize ? MaxPageSize : pageSize;
        //}

        //private (string prev, string cur, string next) CreatePagingNavigation(int page, int pageSize, int count)
        //{
        //    string prev = null;

        //    if (page > 0)
        //    {
        //        prev = Url.Link(nameof(GetUsers), new { page = page - 1, pageSize });
        //    }

        //    string next = null;

        //    if (page < (int)Math.Ceiling((double)count / pageSize) - 1)
        //        next = Url.Link(nameof(GetUsers), new { page = page + 1, pageSize });

        //    var cur = Url.Link(nameof(GetUsers), new { page, pageSize });

        //    return (prev, cur, next);
        //}

        //private object CreateResult(int page, int pageSize, IList<User> users)
        //{
        //    var items = users.Select(CreateProductElementDto);

        //    var count = _dataService.NumberOfUsers();

        //    var navigationUrls = CreatePagingNavigation(page, pageSize, count);


        //    var result = new
        //    {
        //        navigationUrls.prev,
        //        navigationUrls.cur,
        //        navigationUrls.next,
        //        count,
        //        items
        //    };
        //    return result;
        //}

    }
}
