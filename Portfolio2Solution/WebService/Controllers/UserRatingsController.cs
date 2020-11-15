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
        private const int MaxPageSize = 25;

        public UserRatingsController(IDataService dataService, IMapper mapper)
        {
            _dataService = dataService;
            _mapper = mapper;
        }

        [HttpGet("{id}", Name = nameof(GetRatingsForUser))]
        public IActionResult GetRatingsForUser(int id, int page = 0, int pageSize = 10)
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

                        pageSize = pageSize > MaxPageSize ? MaxPageSize : pageSize;

                        var user = _dataService.GetUserRatings(id, page, pageSize);

                        var result = CreateResult(user, id, page, pageSize);

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

        private object CreateResult(IList<UserRates> users, int id, int page, int pageSize)
        {
            var items = users.Select(CreateuserRatesElementDto);
            var count = _dataService.NumberOfUserRatings(id);

            string prev = null;

            if (page > 0)
            {
                prev = Url.Link(nameof(GetRatingsForUser), new { page = page - 1, pageSize });
            }

            string next = null;

            if (page < (int)Math.Ceiling((double)count / pageSize) - 1)
            {
                next = Url.Link(nameof(GetRatingsForUser), new { page = page + 1, pageSize });
            }

            var cur = Url.Link(nameof(GetRatingsForUser), new { page, pageSize });

            var result = new
            {
                prev,
                next,
                cur,
                count,
                items
            };

            return result;
        }
    }
}
