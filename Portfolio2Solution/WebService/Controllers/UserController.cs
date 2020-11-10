using System;
using DataServiceLibrary;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace WebService.Controllers
{
    [ApiController]
    public class UserController: ControllerBase
    {
        private readonly string _connectionString;
        public UserController()
        {
            var config = new ConfigurationBuilder()
                  .AddJsonFile("config.json")
                  .Build();
            _connectionString = config["connectionString"];
        }

        [HttpGet("api/users")]
        public JsonResult GetUsers()
        {

            var dataService = new DataService(_connectionString);

            var users = dataService.GetUsers();
            return new JsonResult(users);
        }
    }
}
