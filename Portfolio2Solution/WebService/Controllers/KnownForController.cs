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
        public KnownForController(IDataService dataService, IMapper mapper)
        {
            _dataService = dataService;
            _mapper = mapper;
        }

        [HttpGet("{id}", Name = nameof(GetKnownTitleForPerson))]
        public IActionResult GetKnownTitleForPerson(string id)
        {

            try
            {
                if (Program.CurrentUser == null)
                {
                    return Unauthorized();
                }

                var knownForTitle = _dataService.GetKnownTitleForPersons(id);
                IList<KnownForDto> knowForList = new List<KnownForDto>();
                var kf = new KnownForDto();


                foreach (var know in knownForTitle)
                {
                    kf.TitleUrl = Url.Link(nameof(TitleController.GetTitle), new { Id = know.TitleConst.Trim() });
                    knowForList.Add(kf);
                }

                return Ok(new { knowForList });

            }
            catch (ArgumentException)
            {
                return Unauthorized();
            }

        }
    }
}
