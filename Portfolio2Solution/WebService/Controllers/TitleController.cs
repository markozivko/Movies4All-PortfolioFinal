using System;
using AutoMapper;
using DataServiceLibrary;
using Microsoft.AspNetCore.Mvc;
using WebService.Models;

namespace WebService.Controllers
{
    [ApiController]
    [Route("api/titles")]
    public class TitleController : ControllerBase
    {

        IDataService _dataService;
        private readonly IMapper _mapper;
        public TitleController(IDataService dataService, IMapper mapper)
        {
            _dataService = dataService;
            _mapper = mapper;
        }

        [HttpGet("{id}", Name = nameof(GetTitlesGenres))]
        public IActionResult GetTitlesGenres(string id)
        {

            var titles = _dataService.GetTitleGenres(id);

            if (titles == null)
            {
                return NotFound();
            }

            var dto = _mapper.Map<TitleDto>(titles);
            dto.Url = Url.Link(nameof(GetTitlesGenres), new { id });
            dto.DetailsUrl = Url.Link(nameof(TitleDetailsController.GetTitleDetails), new { Id = titles.TitleConst });

            //WE LEFT HERE

            return Ok(titles);
        }
    }
}
