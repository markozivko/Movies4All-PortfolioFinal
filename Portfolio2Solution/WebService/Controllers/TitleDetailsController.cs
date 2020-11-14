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
            try
            {
                if (Program.CurrentUser == null)
                {
                    return Unauthorized();
                }

                var rating = _dataService.GetTitleRating(id);
                var plot = _dataService.GetOmdbData(id);
                IList<PrincipalsDto> principals = _mapper.Map<IList<TitlePrincipal>, IList<PrincipalsDto>>(_dataService.GetTitlePrincipals(id));
                var tdo1 = _mapper.Map<TitleDetailsDto>(rating);
                var tdo2 = _mapper.Map(plot, tdo1);
                tdo2.Principals = principals;
                tdo2.EpisodeUrl = Url.Link(nameof(EpisodeController.GetEpisodeForSerie), new { Id = id });
                tdo2.SimilarTitleUrl = Url.Link(nameof(SimilarTitleController.GetTitleSuggestions), new { Id = id });

                if (rating == null || plot == null)
                {
                    return NotFound();
                }

                return Ok(tdo2);
            }
            catch (ArgumentException)
            {
                return Unauthorized();
            }
            
        }
    }
}
