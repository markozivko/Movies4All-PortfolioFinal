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

            var rating = _dataService.GetTitleRating(id);
            var plot = _dataService.GetOmdbData(id);
            var director = _dataService.GetTitleDirectors(id);
            var actors = _dataService.GetActors(id);
            var tdo1 = _mapper.Map<TitleDetailsDto>(rating);
            var tdo2= _mapper.Map(plot, tdo1);
            var tdo3 = _mapper.Map(director.Select(CreateDirectorsElementDto), tdo2);
            if (rating == null || plot == null) 
            {
                return NotFound();
            }

            return Ok(tdo2);
        }

        private PrincipalsDto CreateDirectorsElementDto(TitleDetailsDto tdo, TitlePrincipal tp)
        {
            var tpDto = _mapper.Map<PrincipalsDto>(tp);
            return tpDto;
        }

        private object CreateResult(IList<TitlePrincipal> tp)
        { 
            var items = tp.Select(CreateDirectorsElementDto);
            return new { items };
        }
    }
}
