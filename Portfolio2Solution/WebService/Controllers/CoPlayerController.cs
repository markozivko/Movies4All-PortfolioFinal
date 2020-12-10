using AutoMapper;
using DataServiceLibrary;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using WebService.Models;

namespace WebService.Controllers
{
    [ApiController]
    [Route("api/search/coplayers")]
    public class CoPlayerController: ControllerBase
    {

        IDataService _dataService;
        IMapper _mapper;
        private const int MaxPageSize = 25;

        public CoPlayerController(IDataService dataService, IMapper mapper)
        {
            _dataService = dataService;
            _mapper = mapper;
        }


        [HttpGet(Name = nameof(SearchCoPlayers))]
        public IActionResult SearchCoPlayers(string name, int page = 0, int pageSize = 10)
        {

            try
            {
                if (Program.CurrentUser == null)
                {
                    return Unauthorized();
                }

                pageSize = pageSize > MaxPageSize ? MaxPageSize : pageSize;

                var coPlayersSearch = _dataService.FindCoPlayers(Program.CurrentUser.UserId, name, page, pageSize);

                if (coPlayersSearch == null)
                {
                    return NotFound();
                }

                IList<CoPlayersDto> items = new List<CoPlayersDto>();

                foreach (var s in coPlayersSearch)
                {
                    CoPlayersDto coPlayerSearchDto = new CoPlayersDto();
                    coPlayerSearchDto.SearchPerson = name;
                    coPlayerSearchDto.Name = s.Name;
                    coPlayerSearchDto.Frequency = s.Frequency;
                    coPlayerSearchDto.PersonUrl = Url.Link(nameof(PersonController.GetPerson), new { Id = s.NameConst.Trim() });
                    items.Add(coPlayerSearchDto);
                }

                var count = _dataService.NumberOfCoPlayers(name, Program.CurrentUser.UserId);

                string prev = null;

                if (page > 0)
                {
                    prev = Url.Link(nameof(SearchCoPlayers), new { page = page - 1, pageSize });
                }

                string next = null;

                if (page < (int)Math.Ceiling((double)count / pageSize) - 1)
                {
                    next = Url.Link(nameof(SearchCoPlayers), new { page = page + 1, pageSize });
                }

                var cur = Url.Link(nameof(SearchCoPlayers), new { page, pageSize });

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
