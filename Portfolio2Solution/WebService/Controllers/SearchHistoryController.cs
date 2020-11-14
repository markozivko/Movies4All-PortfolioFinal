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
    [Route("api/searchHistory")]
    public class SearchHistoryController: ControllerBase
    {
        IDataService _dataService;
        private readonly IMapper _mapper;
        public SearchHistoryController(IDataService dataService, IMapper mapper)
        {

            _dataService = dataService;
            _mapper = mapper;

        }

        [HttpGet("{id}", Name = nameof(GetSearchHistoryForUser))]
        public IActionResult GetSearchHistoryForUser(int id)
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
                        var search = _dataService.GetSearchHistoryForUser(id);
                        var result = CreateResult(search);

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

        private SearchHistoryDto CreateSearchHistoryElementDto(SearchHistory sh)
        {

            var shDto = _mapper.Map<SearchHistoryDto>(sh);
            return shDto;

        }

        private object CreateResult(IList<SearchHistory> sh)
        {
            var items = sh.Select(CreateSearchHistoryElementDto);
            return new { items };
        }

    }
}
