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
    [Route("api/knownfor")]
    public class KnownForController: ControllerBase
    {
        IDataService _dataService;
        private readonly IMapper _mapper;
        private const int MaxPageSize = 25;

        public KnownForController(IDataService dataService, IMapper mapper)
        {
            _dataService = dataService;
            _mapper = mapper;
        }

        [HttpGet("{id}", Name = nameof(GetKnownTitleForPerson))]
        public IActionResult GetKnownTitleForPerson(string id, int page = 0, int pageSize = 10)
        {

            try
            {
                if (Program.CurrentUser == null)
                {
                    return Unauthorized();
                }

                pageSize = pageSize > MaxPageSize ? MaxPageSize : pageSize;

                var knownForTitle = _dataService.GetKnownTitleForPersons(id, page, pageSize);
                IList<KnownForDto> knowForList = new List<KnownForDto>();
                var kf = new KnownForDto();


                foreach (var know in knownForTitle)
                {
                    kf.TitleUrl = Url.Link(nameof(TitleController.GetTitle), new { Id = know.TitleConst.Trim() });
                    knowForList.Add(kf);
                }

                var count = _dataService.NumberOfKnownTitlesForPerson(id);

                string prev = null;

                if (page > 0)
                {
                    prev = Url.Link(nameof(GetKnownTitleForPerson), new { page = page - 1, pageSize });
                }

                string next = null;

                if (page < (int)Math.Ceiling((double)count / pageSize) - 1)
                {
                    next = Url.Link(nameof(GetKnownTitleForPerson), new { page = page + 1, pageSize });
                }

                var cur = Url.Link(nameof(GetKnownTitleForPerson), new { page, pageSize });

                var result = new
                {
                    prev,
                    next,
                    cur,
                    count,
                    knowForList
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
