using System;
using System.Collections.Generic;
using AutoMapper;
using DataServiceLibrary;
using Microsoft.AspNetCore.Mvc;
using WebService.Models;

namespace WebService.Controllers
{
    [ApiController]
    [Route("api/personalities")]
    public class PersonalitiesController: ControllerBase
    {

        IDataService _dataService;
        IMapper _mapper;

        public PersonalitiesController(IDataService dataService, IMapper mapper)
        {
            _dataService = dataService;
            _mapper = mapper;
        }

        [HttpGet("{id}", Name = nameof(GetPersonalitiesForUser))]
        public IActionResult GetPersonalitiesForUser(int id)
        {

            var pMarks = _dataService.GetPersonalitiesForUser(id);
            var pDto = _mapper.Map<IEnumerable<PersonalitiesDto>>(pMarks);

            return Ok(pDto);
        }

    }
}
