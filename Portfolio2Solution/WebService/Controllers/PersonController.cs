﻿using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using DataServiceLibrary;
using DataServiceLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using WebService.Models;

namespace WebService.Controllers
{
    [ApiController]
    [Route("api/persons")]
    public class PersonController: ControllerBase
    {
        IDataService _dataService;
        IMapper _mapper;

        public PersonController(IDataService dataService, IMapper mapper)
        {
            _dataService = dataService;
            _mapper = mapper;
        }

        [HttpGet("{id}", Name = nameof(GetPerson))]
        public IActionResult GetPerson(string id)
        {

            var person = _dataService.GetPerson(id);

            if (person == null)
            {
                return NotFound();
            }

            var pDto = _mapper.Map<PersonDto>(person);
            return Ok(pDto);
        }
    }
}
