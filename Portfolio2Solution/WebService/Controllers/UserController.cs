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
        private readonly string _connectionString;
        public UserController(IDataService dataService)
        {
            var config = new ConfigurationBuilder()
                  .AddJsonFile("config.json")
                  .Build();
            _connectionString = config["connectionString"];
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
