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
        private const int MaxPageSize = 25;

        public EpisodeController(IDataService dataService, IMapper mapper)
        {
            _dataService = dataService;
            _mapper = mapper;
        }

        [HttpGet("{id}", Name = nameof(GetEpisodeForSerie))]
        public IActionResult GetEpisodeForSerie(string id, int page = 0, int pageSize = 10)
        {
            try
            {
                if (Program.CurrentUser == null)
                {
                    return Unauthorized();
                }

                pageSize = pageSize > MaxPageSize ? MaxPageSize : pageSize;

                var episodes = _dataService.GetAllEpisodes(id, page, pageSize);

                var result = CreateResult(episodes, id, page, pageSize);

                if (result == null)
                {
                    return NotFound();
                }

                return Ok(result);
            }
            catch (ArgumentException)
            {
                return Unauthorized();
            }
            

        }

        private object CreateResult(IList<Episode> episodes,string id, int page, int pageSize)
        {
            IList<EpisodeDto> items = new List<EpisodeDto>();

            foreach (var e in episodes)
            {

                var dto = _mapper.Map<EpisodeDto>(e);
                var plot = _dataService.GetOmdbData(e.TitleConst.Trim());
                 
                if(plot != null)
                {
                    dto.StoryLine = plot.Plot;

                }
                items.Add(dto);
            }

            var count = _dataService.NumberOfEpisodesForSerie(id);

            string prev = null;

            if (page > 0)
            {
                prev = Url.Link(nameof(GetEpisodeForSerie), new { page = page - 1, pageSize });
            }

            string next = null;

            if (page < (int)Math.Ceiling((double)count / pageSize) - 1)
            {
                next = Url.Link(nameof(GetEpisodeForSerie), new { page = page + 1, pageSize });
            }

            var cur = Url.Link(nameof(GetEpisodeForSerie), new { page, pageSize });

            var result = new
            {
                prev,
                next,
                cur,
                count,
                items
            };

            return result;

        }
    }
}
 