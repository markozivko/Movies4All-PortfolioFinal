using System;
using AutoMapper;
using DataServiceLibrary;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using WebService.Models;

namespace WebService.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UserController: ControllerBase
    {

        IDataService _dataService;
        private readonly IMapper _mapper;

        public UserController(IDataService dataService, IMapper mapper)
        {
            _dataService = dataService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetUsers()
        {

            var users = _dataService.GetUsers();

            if (users == null) {
                return NotFound();
            }
            var userDto = _mapper.Map<UserDto>(users);
            return Ok(users);
        }
    }
}
