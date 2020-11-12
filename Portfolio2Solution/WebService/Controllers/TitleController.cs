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
    [Route("api/titles")]
    public class TitleController : ControllerBase
    {
        readonly IDataService _dataService;
        private readonly IMapper _mapper;
        public TitleController(IDataService dataService, IMapper mapper)
        {
            _dataService = dataService;
            _mapper = mapper;
        }

 
        [HttpGet("{id}", Name = nameof(GetTitle))]
        public IActionResult GetTitle(string id)
        {

            var titles = _dataService.GetTitle(id);
                         

            if (titles == null)
            {
                return NotFound();
            }

            var dto = _mapper.Map<TitleDto>(titles);
            dto.DetailsUrl = Url.Link(nameof(TitleDetailsController.GetTitleDetails), new { Id = titles.Const.Replace(" ", String.Empty) });

            return Ok(dto);
        }
        [HttpGet("category/{id}", Name = nameof(GetTitlesByCategory))]
        public IActionResult GetTitlesByCategory(int id)
        {
            var titles = _dataService.GetTitleByGenre(id);

            if (titles == null)
            {
                return NotFound();
            }
            var result = CreateResult(titles);
            return Ok(result);
        }
        private TitleListDto CreateTitleElementDto(TitleGenre title)
        {
            var dto = _mapper.Map<TitleListDto>(title);
            return dto;
        }


        private object CreateResult(IList<TitleGenre> titles)
        {
            var items = titles.Select(CreateTitleElementDto);

            return new { items };
        }
    }
}
