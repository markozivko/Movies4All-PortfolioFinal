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
    [Route("api/personalities")]
    public class PersonalitiesController : ControllerBase
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
            try
            {
                if (Program.CurrentUser == null)
                {
                    return Unauthorized();
                }
                else
                {
                    if (Program.CurrentUser.UserId == id)
                    {
                        var pMarks = _dataService.GetPersonalitiesForUser(id);
                        var result = CreateResult(pMarks);

                        return Ok(result);
                    }
                    else
                    {
                        return Unauthorized();
                    }
                }
            }
            catch (ArgumentException)
            {
                return Unauthorized();
            }

        }

        private PersonalitiesDto CreatePersonalitiesElementDto(Personalities p)
        {
            var pDto = _mapper.Map<PersonalitiesDto>(p);
            pDto.FavoritePersonUrl = Url.Link(nameof(PersonController.GetPerson), new { Id = p.NameConst.Replace(" ", String.Empty) });
            return pDto;

        }

        private object CreateResult(IList<Personalities> p)
        {
            var items = p.Select(CreatePersonalitiesElementDto);

            return new { items };
        }

        [HttpPut("{id}")]
        public IActionResult UpdatePersonalityForUser(int id, PersonForCreateOrUpdateDto pfc)
        {

            try
            {
                if (Program.CurrentUser == null)
                {
                    return Unauthorized();
                }
                else
                {
                    if (Program.CurrentUser.UserId == id)
                    {

                        var pfc1 = _mapper.Map<Personalities>(pfc);

                        if (!_dataService.UserUpdatePersonality(id, pfc1.NameConst, pfc1.Notes))
                        {
                            return NotFound();
                        }

                        return NoContent();

                    }
                    else
                    {
                        return Unauthorized();
                    }
                }
            }
            catch (ArgumentException)
            {
                return Unauthorized();
            }

        }

    }
}
