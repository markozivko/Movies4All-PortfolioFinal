using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using DataServiceLibrary;
using DataServiceLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using WebService.Models;
namespace WebService.Controllers
{
    [ApiController]
    [Route("api/suggestions")]
    public class SimilarTitleController: ControllerBase
    {
        IDataService _dataService;
        IMapper _mapper;
        private const int MaxPageSize = 25;

        public SimilarTitleController(IDataService dataService, IMapper mapper)
        {
            _dataService = dataService;
            _mapper = mapper;
        }

        [HttpGet("{id}", Name = nameof(GetTitleSuggestions))]
        public IActionResult GetTitleSuggestions(string id, int page = 0, int pageSize = 10)
        {

            try
            {
                if (Program.CurrentUser == null)
                {
                    return Unauthorized();
                }

                var titleSuggestion = _dataService.RecommendTitles(id, page, pageSize);

                IList<SimilarTitleDto> items = new List<SimilarTitleDto>();

                pageSize = pageSize > MaxPageSize ? MaxPageSize : pageSize;


                foreach (var s in titleSuggestion)
                {
                    SimilarTitleDto title = new SimilarTitleDto();

                    title.TitleUrl = Url.Link(nameof(TitleController.GetTitle), new { Id = s.TitleConst.Trim() });
                    items.Add(title);
                }


                var count = _dataService.NumberOfRecommendedTitles(id);

                string prev = null;

                if (page > 0)
                {
                    prev = Url.Link(nameof(GetTitleSuggestions), new { page = page - 1, pageSize });
                }

                string next = null;

                if (page < (int)Math.Ceiling((double)count / pageSize) - 1)
                {
                    next = Url.Link(nameof(GetTitleSuggestions), new { page = page + 1, pageSize });
                }

                var cur = Url.Link(nameof(GetTitleSuggestions), new { page, pageSize });

                var result = new
                {
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
