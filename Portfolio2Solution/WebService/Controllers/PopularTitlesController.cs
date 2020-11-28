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
    [Route("api/detailpopulartitles")]
    public class PopularTitlesController: ControllerBase
    {
        IDataService _dataService;
        private readonly IMapper _mapper;
        public PopularTitlesController(IDataService dataService, IMapper mapper)
        {
            _dataService = dataService;
            _mapper = mapper;
        }

        [HttpGet("{id}", Name = nameof(GetPopularTitleDetails))]
        public IActionResult GetPopularTitleDetails(string id)
        {
            var rating = _dataService.GetTitleRating(id);
            var plot = _dataService.GetOmdbData(id);

            var tdo1 = _mapper.Map<PopularTitlesDetailsDto>(rating);
            var tdo2 = _mapper.Map(plot, tdo1);
            if (rating == null || plot == null)
            {
                return NotFound();
            }

            return Ok(tdo2);
        }
    }
}
