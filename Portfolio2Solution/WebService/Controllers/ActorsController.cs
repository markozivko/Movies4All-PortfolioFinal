using AutoMapper;
using DataServiceLibrary;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebService.Models;

namespace WebService.Controllers
{
    [ApiController]
    [Route("api/search/actors")]
    public class ActorsController : ControllerBase
    {

        IDataService _dataService;
        IMapper _mapper;
        private const int MaxPageSize = 25;

        public ActorsController(IDataService dataService, IMapper mapper)
        {
            _dataService = dataService;
            _mapper = mapper;
        }
        [HttpGet(Name = nameof(SearchActors))]
        public IActionResult SearchActors(string SearchTitle, string SearchPlot, string SearchCharacter, string SearchPersonName, int page =0, int pageSize =10)
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
                var actorsSearch = _dataService.FindActors(Program.CurrentUser.UserId, SearchTitle, SearchPlot, SearchCharacter, SearchPersonName, page, pageSize);

                Console.WriteLine($"{actorsSearch.FirstOrDefault().Name}");
                if (actorsSearch == null)
                {
                    return NotFound();
                }
                IList<ActorsDto> items = new List<ActorsDto>();


                foreach (var a in actorsSearch)
                {
                    var actors = _mapper.Map<ActorsDto>(a);
                    actors.SearchTitle = SearchTitle;
                    actors.SearchPlot = SearchPlot;
                    actors.SearchCharacter = SearchCharacter;
                    actors.SearchPersonName = SearchPersonName;
                    actors.ActorUrl = Url.Link(nameof(PersonController.GetPerson), new { Id = a.NameConst.Trim() });
                    items.Add(actors);
                }

                var count = _dataService.NumberOfActorsSearchMatched(Program.CurrentUser.UserId, SearchTitle, SearchPlot, SearchCharacter, SearchPersonName);

                string prev = null;

                if (page > 0)
                {
                    prev = Url.Link(nameof(SearchActors), new { page = page - 1, pageSize });
                }

                string next = null;

                if (page < (int)Math.Ceiling((double)count / pageSize) - 1)
                {
                    next = Url.Link(nameof(SearchActors), new { page = page + 1, pageSize });
                }

                var cur = Url.Link(nameof(SearchActors), new { page, pageSize });

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
