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
    [Route("api/latestTitles")]
    public class LatestTitlesController: ControllerBase
    {

        IDataService _dataService;
        IMapper _mapper;
        private const int MaxPageSize = 25;

        public LatestTitlesController(IDataService dataService, IMapper mapper)
        {
            _dataService = dataService;
            _mapper = mapper;
        }

        [HttpGet("{id}", Name = nameof(GetLatestTitleDetails))]
        public IActionResult GetLatestTitleDetails(string id)
        {
            var rating = _dataService.GetTitleRating(id);
            var omdb = _dataService.GetOmdbData(id);
            var tdo1 = _mapper.Map<LatestTitlesDetailsDto>(rating);
            if (rating == null && omdb == null)
            {
                return NotFound();
            }
            if (omdb == null) {
                tdo1.Plot = "";
                tdo1.Poster = "";
                return Ok(tdo1);
            }
            if (rating == null)
            {
                var tdo2 = _mapper.Map<LatestTitlesDetailsDto>(omdb);
                tdo2.NumVotes = 0;
                tdo2.Rating = 0;
                return Ok(tdo2);
            }
            else
            {
                var tdo2 = _mapper.Map(omdb, tdo1);
                return Ok(tdo2);
            }



        }
    }
}
