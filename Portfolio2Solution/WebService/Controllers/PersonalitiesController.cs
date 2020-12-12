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
        private const int MaxPageSize = 25;

        public PersonalitiesController(IDataService dataService, IMapper mapper)
        {
            _dataService = dataService;
            _mapper = mapper;
        }

        [HttpGet("{id}", Name = nameof(GetPersonalitiesForUser))]
        public IActionResult GetPersonalitiesForUser(int id, int page = 0, int pageSize = 10)
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

                        pageSize = pageSize > MaxPageSize ? MaxPageSize : pageSize;

                        var pMarks = _dataService.GetPersonalitiesForUser(id, page, pageSize);
                      
                       

                        if (pMarks == null)
                        {
                            return NotFound();
                        }
                        else
                        {
                            var result = CreateResult(pMarks, id, page, pageSize);

                            return Ok(result);
                        }

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

        private object CreateResult(IList<Personalities> p, int id, int page, int pageSize)
        {
            var items = p.Select(CreatePersonalitiesElementDto);

            var count = _dataService.NumberOfPersonalitiesForUser(id);

            string prev = null;

            if (page > 0)
            {
                prev = Url.Link(nameof(GetPersonalitiesForUser), new { page = page - 1, pageSize });
            }

            string next = null;

            if (page < (int)Math.Ceiling((double)count / pageSize) - 1)
            {
                next = Url.Link(nameof(GetPersonalitiesForUser), new { page = page + 1, pageSize });
            }

            var cur = Url.Link(nameof(GetPersonalitiesForUser), new { page, pageSize });

            var result = new
            {
                pageSizes = new int[] { 6, 12, 24, 48 },
                prev,
                next,
                cur,
                count,
                items
            };

            return result;
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

        [HttpDelete("{idU}/{idP}")]
        public IActionResult DeletePersonalityForUser(int idU, string idP)
        {
            try
            {
                if (Program.CurrentUser == null)
                {
                    return Unauthorized();
                }
                else
                {

                    if (Program.CurrentUser.UserId == idU)
                    {
                        if (!_dataService.DeletePersonalityForUser(idU, idP))
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
