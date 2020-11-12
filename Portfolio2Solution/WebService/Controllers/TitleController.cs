﻿using System;
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

            return Ok(dto);
        }
        [HttpGet]
        public IActionResult GetTitlesByCategory(int genre)
        {
            var titles = _dataService.GetTitleByGenre(genre);
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
