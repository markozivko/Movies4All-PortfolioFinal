using System;
using DataServiceLibrary;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace WebService.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UserController: ControllerBase
    {

        IDataService _dataService;
        public UserController(IDataService dataService)
        {
            _dataService = dataService;
        }

        [HttpGet]
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
