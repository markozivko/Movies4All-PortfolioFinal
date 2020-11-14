using System;
using System.Collections;
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
    [Route("api/episodes")]
    public class EpisodeController: ControllerBase
    {
        IDataService _dataService;
        private readonly IMapper _mapper;
        public EpisodeController(IDataService dataService, IMapper mapper)
        {
            _dataService = dataService;
            _mapper = mapper;
        }

        [HttpGet("{id}", Name = nameof(GetEpisodeForSerie))]
        public IActionResult GetEpisodeForSerie(string id)
        {

            var episodes = _dataService.GetAllEpisodes(id);

            var result = CreateResult(episodes);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);

        }

        private object CreateResult(IList<Episode> episodes)
        {
            IList<EpisodeDto> items = new List<EpisodeDto>();

            foreach (var e in episodes)
            {

                //DOES NOT WORK
                var dto = _mapper.Map<EpisodeDto>(e);
                var plot = _dataService.GetOmdbData(e.TitleConst.Trim()).Plot;

                Console.WriteLine(e.TitleConst);
                Console.WriteLine(plot);
                dto.StoryLine = plot;
                items.Add(dto);
            }

            return new { items };

        }
    }
}
 