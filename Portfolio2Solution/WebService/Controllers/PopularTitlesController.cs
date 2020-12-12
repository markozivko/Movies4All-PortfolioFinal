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
            if (rating == null && plot == null)
            {
                return NotFound();
            }

            if (rating == null)
            {
                var tdo2 = _mapper.Map<TitleDetailsDto>(plot);

                tdo2.NumVotes = 0;
                tdo2.Rating = 0;
                tdo2.EpisodeUrl = Url.Link(nameof(EpisodeController.GetEpisodeForSerie), new { Id = id });
                tdo2.SimilarTitleUrl = Url.Link(nameof(SimilarTitleController.GetTitleSuggestions), new { Id = id });
                return Ok(tdo2);
            }

            if (plot == null)
            {
                tdo1.Plot = "";
                tdo1.Poster = "";
                return Ok(tdo1);
            }
            else
            {
                var tdo2 = _mapper.Map(plot, tdo1);
                return Ok(tdo2);
            }

        }
    }
}
