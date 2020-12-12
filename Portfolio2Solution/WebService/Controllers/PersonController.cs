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
            try
            {
                if (Program.CurrentUser == null)
                {
                    return Unauthorized();
                }

                var person = _dataService.GetPerson(id);

                if (person == null)
                {
                    return NotFound();
                }

                var pDto = _mapper.Map<PersonDto>(person);
                pDto.KnownForUrl = Url.Link(nameof(KnownForController.GetKnownTitleForPerson), new { Id = id });
                return Ok(pDto);
            }
            catch (ArgumentException)
            {
                return Unauthorized();
            }  
        }

        [HttpPost("{id}")]
        public IActionResult AddFavoritePersonForUser(PersonForCreateOrUpdateDto pfc)
        {
            try
            {
                if(Program.CurrentUser == null)
                {
                    return Unauthorized();
                }

                if (_dataService.CheckIfPersonalitiesExistsForUser(Program.CurrentUser.UserId, pfc.Name) != null)
                {
                    return BadRequest();
                }
                else
                {
                    _dataService.UserAddPersonality(Program.CurrentUser.UserId, pfc.Name, pfc.Notes);

                    var personality = _mapper.Map<Personalities>(pfc);
                    var result = _mapper.Map<PersonalitiesDto>(personality);
                    result.FavoritePersonUrl = Url.Link(nameof(PersonController.GetPerson), new { Id = personality.NameConst.Trim() });
                    return Created("", result);
                }
            }
            catch (ArgumentException)
            {
                return Unauthorized();
            }
        }
    }
}
