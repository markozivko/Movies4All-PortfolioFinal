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
    [Route("api/genres")]
    public class GenreController: ControllerBase
    {
        IDataService _dataService;
        IMapper _mapper;

        public GenreController(IDataService dataService, IMapper mapper)
        {
            _dataService = dataService;
            _mapper = mapper;
        }

        [HttpGet(Name = nameof(GetAllGenres))]
        public IActionResult GetAllGenres(int id)
        {

            Console.WriteLine($"######## started");

            try
            {
                if (Program.CurrentUser == null)
                {
                   
                    return Unauthorized();
                    
                }
                else
                {

                    var genre = _dataService.GetAllGenres();
                    Console.WriteLine($"########{genre}");

                    var result = CreateResult(genre);
                    Console.WriteLine($"########{result}");


                    return Ok(result);
                }
            }
            catch (ArgumentException)
            {
                return Unauthorized();
            }

        }

        private GenresDto CreateGenreElementDto(Genre g)
        {
            var gDto = _mapper.Map<GenresDto>(g);
            gDto.genreUrl = Url.Link(nameof(GenreController.GetAllGenres), new { g.Id });
            Console.WriteLine($"########{gDto.genre}");
            return gDto;

        }

        private object CreateResult(IList<Genre> g)
        {
            var items = g.Select(CreateGenreElementDto);


            return items;
        }

    }
}
