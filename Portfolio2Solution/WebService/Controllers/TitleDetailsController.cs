﻿using System;
using AutoMapper;
using DataServiceLibrary;
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

            var titles = _dataService.GetTitleDetails(id);
            var rating = _dataService.GetTitleRating(id);
            var plot = _dataService.GetOmdbData(id).Plot;

            TitleDetailsDto tdo = new TitleDetailsDto(rating.Average, rating.NumVotes, plot, null);

            var tdo1 = _mapper.Map<TitleDetailsDto>(rating);
            var tdo2 = _mapper.Map<TitleDetailsDto>(plot);

            if (titles == null)
            {
                return NotFound();
            }

            return Ok(titles);
        }
    }
}
