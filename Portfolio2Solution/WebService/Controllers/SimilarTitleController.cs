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
        public IActionResult GetTitleSuggestions(string id)
        {

            try
            {
                if (Program.CurrentUser == null)
                {
                    return Unauthorized();
                }

                var titleSuggestion = _dataService.RecommendTitles(id);

                IList<SimilarTitleDto> items = new List<SimilarTitleDto>();


                foreach (var s in titleSuggestion)
                {
                    SimilarTitleDto title = new SimilarTitleDto();

                    title.TitleUrl = Url.Link(nameof(TitleController.GetTitle), new { Id = s.TitleConst.Trim() });
                    items.Add(title);
                }

                return Ok(new { items });
            }
            catch (ArgumentException)
            {
                return Unauthorized();
            }
        }
    }
}
