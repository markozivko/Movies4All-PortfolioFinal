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
    [Route("api/search")]
    public class SimpleSearchController: ControllerBase
    {
        IDataService _dataService;
        IMapper _mapper;

        public SimpleSearchController(IDataService dataService, IMapper mapper)
        {
            _dataService = dataService;
            _mapper = mapper;
        }
        [HttpGet(Name = nameof(SimpleSearch))]
        public IActionResult SimpleSearch(string search)
        {
            try
            {
                if (Program.CurrentUser == null)
                {
                    return Unauthorized();
                }

                var simpleSearch = _dataService.StringSearch(Program.CurrentUser.UserId, search);

                if (simpleSearch == null)
                {
                    return NotFound();
                }
                SimpleSearchDto simpleSearchDto = new SimpleSearchDto();

                IList<SimpleSearchDto> items = new List<SimpleSearchDto>();

                foreach (var s in simpleSearch)
                {
                    simpleSearchDto.Search = Url.Link(nameof(TitleController.GetTitle), new { Id = s.TitleConst.Trim() });
                    items.Add(simpleSearchDto);
                }

                return Ok(new { items});

            } 
            catch (ArgumentException)
            {
                return Unauthorized();
            }
        }

       
    }
}
