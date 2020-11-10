using System;
using AutoMapper;
using DataServiceLibrary;
using Microsoft.AspNetCore.Mvc;

namespace WebService.Controllers
{
    [ApiController]
    [Route("api/detailtitles")]
    public class TitleDetailsController : ControllerBase
    {

        IDataService _dataService;
        private readonly IMapper _mapper;
        public TitleDetailsController(IDataService dataService, IMapper mapper)
        {
            _dataService = dataService;
            _mapper = mapper;
        }

        [HttpGet("{id}", Name = nameof(GetTitleDetails))]
        public IActionResult GetTitleDetails(string id)
        {

            var titles = _dataService.GetTitleDetails(id);

            if (titles == null)
            {
                return NotFound();
            }

            return Ok(titles);
        }
    }
}
