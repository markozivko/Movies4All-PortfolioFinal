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
                //TODO get back
                //if (Program.CurrentUser == null)
                //{
                //    return Unauthorized();
                //}

                var rating = _dataService.GetTitleRating(id);
                var plot = _dataService.GetOmdbData(id);
                var principals = _dataService.GetTitlePrincipals(id);

                IList<PrincipalsDto> pList = new List<PrincipalsDto>();

                foreach (var i in principals)
                {
                    var dto = _mapper.Map<PrincipalsDto>(i);

                    dto.PersonUrl = Url.Link(nameof(PersonController.GetPerson), new { Id = i.NameConst.Trim() });

                    pList.Add(dto);
                }

                var tdo1 = _mapper.Map<TitleDetailsDto>(rating);
                
                if (rating == null && plot == null)
                {
                    return NotFound();
                }

                if (plot == null)
                {
                    tdo1.Plot = "";
                    tdo1.Poster = "";
                    tdo1.Principals = pList;
                    tdo1.EpisodeUrl = Url.Link(nameof(EpisodeController.GetEpisodeForSerie), new { Id = id });
                    tdo1.SimilarTitleUrl = Url.Link(nameof(SimilarTitleController.GetTitleSuggestions), new { Id = id });
                    return Ok(tdo1);
                }

                if (rating == null)
                {
                    var tdo2 = _mapper.Map<TitleDetailsDto>(plot);

                    tdo2.NumVotes = 0;
                    tdo2.Rating = 0;
                    tdo2.Principals = pList;
                    tdo2.EpisodeUrl = Url.Link(nameof(EpisodeController.GetEpisodeForSerie), new { Id = id });
                    tdo2.SimilarTitleUrl = Url.Link(nameof(SimilarTitleController.GetTitleSuggestions), new { Id = id });
                    return Ok(tdo2);
                }else
                {
                    var tdo2 = _mapper.Map(plot, tdo1);
                    tdo2.Principals = pList;
                    tdo2.EpisodeUrl = Url.Link(nameof(EpisodeController.GetEpisodeForSerie), new { Id = id });
                    tdo2.SimilarTitleUrl = Url.Link(nameof(SimilarTitleController.GetTitleSuggestions), new { Id = id });
                    return Ok(tdo2);
                }

            }
            catch (ArgumentException)
            {
                return Unauthorized();
            }
            
        }
    }
}
