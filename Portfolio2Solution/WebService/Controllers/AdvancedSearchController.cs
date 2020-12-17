using AutoMapper;
using DataServiceLibrary;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using WebService.Models;
namespace WebService.Controllers
{
    [ApiController]
    [Route("api/search/advanced")]
    public class AdvancedSearchController: ControllerBase
    {

        IDataService _dataService;
        IMapper _mapper;
        private const int MaxPageSize = 25;

        public AdvancedSearchController(IDataService dataService, IMapper mapper)
        {
            _dataService = dataService;
            _mapper = mapper;
        }

        [HttpGet(Name = nameof(AdvancedSearch))]
        public IActionResult AdvancedSearch(string SearchTitle, string SearchPlot, string SearchCharacter, string SearchPersonName, int page = 0, int pageSize = 10)
        {
            try
            {
                if (Program.CurrentUser == null)
                {
                    return Unauthorized();
                }

                pageSize = pageSize > MaxPageSize ? MaxPageSize : pageSize;
                SearchPlot ??= "";
                SearchTitle ??= "";
                SearchCharacter ??= "";
                SearchPersonName ??= "";
                var advancedSearch = _dataService.StructuredStringSearch(Program.CurrentUser.UserId, SearchTitle, SearchPlot, SearchCharacter, SearchPersonName, page, pageSize);
                if (advancedSearch == null)
                {
                    return NotFound();
                }

                IList<AdvancedSearchDto> items = new List<AdvancedSearchDto>();


                foreach (var a in advancedSearch)
                {
                    AdvancedSearchDto advanceDto = new AdvancedSearchDto();

                    advanceDto.SearchTitle = SearchTitle;
                    advanceDto.SearchPlot = SearchPlot;
                    advanceDto.SearchCharacter = SearchCharacter;
                    advanceDto.SearchPersonName = SearchPersonName;
                    advanceDto.PrimaryTitle = a.PrimaryTitle;
                    advanceDto.TitleUrl = Url.Link(nameof(TitleController.GetTitle), new { Id = a.TitleConst.Trim() });
                    items.Add(advanceDto);
                }

                var count = _dataService.NumberOfStructuredSearchMatched(Program.CurrentUser.UserId, SearchTitle, SearchPlot, SearchCharacter, SearchPersonName);

                string prev = null;

                if (page > 0)
                {
                    prev = Url.Link(nameof(AdvancedSearch), new { SearchTitle, SearchPlot, SearchCharacter, SearchPersonName, page = page - 1, pageSize });
                }

                string next = null;

                if (page < (int)Math.Ceiling((double)count / pageSize) - 1)
                {
                    next = Url.Link(nameof(AdvancedSearch), new { SearchTitle, SearchPlot, SearchCharacter, SearchPersonName, page = page + 1, pageSize });
                }

                var cur = Url.Link(nameof(AdvancedSearch), new { SearchTitle, SearchPlot, SearchCharacter, SearchPersonName, page, pageSize });

                var result = new
                {
                    pageSizes = new int[] { 6, 12, 24, 48 },
                    prev,
                    next,
                    cur,
                    count,
                    items
                };

                return Ok(result);


            }
            catch (ArgumentException)
            {
                return Unauthorized();
            }
        }
    }
}
