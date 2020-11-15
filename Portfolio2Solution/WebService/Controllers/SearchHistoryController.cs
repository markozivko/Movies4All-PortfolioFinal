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
        private const int MaxPageSize = 25;

        public SearchHistoryController(IDataService dataService, IMapper mapper)
        {

            _dataService = dataService;
            _mapper = mapper;

        }

        [HttpGet("{id}", Name = nameof(GetSearchHistoryForUser))]
        public IActionResult GetSearchHistoryForUser(int id, int page = 0, int pageSize = 10)
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

                        var search = _dataService.GetSearchHistoryForUser(id, page, pageSize);
                        var result = CreateResult(search, id, page, pageSize);

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

        private object CreateResult(IList<SearchHistory> sh, int id, int page, int pageSize)
        {
            var items = sh.Select(CreateSearchHistoryElementDto);

            var count = _dataService.NumberOfSearchHistoryForUser(id);

            string prev = null;

            if (page > 0)
            {
                prev = Url.Link(nameof(GetSearchHistoryForUser), new { page = page - 1, pageSize });
            }

            string next = null;

            if (page < (int)Math.Ceiling((double)count / pageSize) - 1)
            {
                next = Url.Link(nameof(GetSearchHistoryForUser), new { page = page + 1, pageSize });
            }

            var cur = Url.Link(nameof(GetSearchHistoryForUser), new { page, pageSize });

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
