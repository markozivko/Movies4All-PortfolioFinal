using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using DataServiceLibrary;
using DataServiceLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using WebService.Models;

namespace WebService.Controllers
{
    [ApiController]
    [Route("api/ratings")]
    public class UserRatingsController : ControllerBase
    {
        IDataService _dataService;
        IMapper _mapper;

        public UserRatingsController(IDataService dataService, IMapper mapper)
        {
            _dataService = dataService;
            _mapper = mapper;
        }

        [HttpGet("{id}", Name = nameof(GetRatingsForUser))]
        public IActionResult GetRatingsForUser(int id)
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

                        var user = _dataService.GetUserRatings(id);

                        var result = CreateResult(user);

                        return Ok(result);

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

        private object CreateuserRatesElementDto(UserRates ur)
        {

            var dto = _mapper.Map<UserRatingsDto>(ur);
            return dto;

        }

        private object CreateResult(IList<UserRates> users)
        {
            var items = users.Select(CreateuserRatesElementDto);
            return new { items };
        }
    }
}
