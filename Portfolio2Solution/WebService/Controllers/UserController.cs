using System;
using DataServiceLibrary;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace WebService.Controllers
{
    [ApiController]
    public class UserController: ControllerBase
    {

        IDataService _dataService;
        public UserController(IDataService dataService)
        {
            _dataService = dataService;
        }

        [HttpGet("api/users")]
        public IActionResult GetUsers()
        {

            var users = _dataService.GetUsers();

            if (users == null) {
                return NotFound();
            }

            return Ok(users);
        }
    }
}
