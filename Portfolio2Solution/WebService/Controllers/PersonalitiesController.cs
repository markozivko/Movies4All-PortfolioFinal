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
            var result = CreateResult(pMarks);

            return Ok(result);
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

    }
}
