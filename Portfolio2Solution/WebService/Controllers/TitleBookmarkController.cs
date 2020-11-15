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
    [Route("api/titlebookmarks")]
    public class TitleBookmarkController : ControllerBase
    {

        IDataService _dataService;
        IMapper _mapper;

        public TitleBookmarkController(IDataService dataService, IMapper mapper)
        {

            _dataService = dataService;
            _mapper = mapper;

        }

        [HttpGet("{id}", Name = nameof(GetTitleBookmarksForUser))]
        public IActionResult GetTitleBookmarksForUser(int id)
        {
            try
            {
                if (Program.CurrentUser == null)
                {
                    return Unauthorized();
                } else
                {
                    if (Program.CurrentUser.UserId == id)
                    {
                        var titleBookmarks = _dataService.GetTitleBookmarkForUser(id);
                        var result = CreateResult(titleBookmarks);

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

        private TitleBookmarkDto CreateTitleBookmarkElementDto(TitleBookmark tb)
        {
            var tbDto = _mapper.Map<TitleBookmarkDto>(tb);
            tbDto.FavoriteTitleUrl = Url.Link(nameof(TitleController.GetTitle), new { Id = tb.TitleConst.Replace(" ", string.Empty) });
            return tbDto;
        }

        private object CreateResult(IList<TitleBookmark> tb)
        {
            var items = tb.Select(CreateTitleBookmarkElementDto);

            return new { items };
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBookMarksForUser(int id, TitleBookmarkForCreationOrUpdateDto tb)
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
                        var tb1 = _mapper.Map<TitleBookmark>(tb);
                        Console.WriteLine($"======================{tb1.Notes}");


                        if (!_dataService.UserUpdateBookmarkNotes(id, tb1.TitleConst, tb1.Notes))
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

        [HttpDelete("{idU}/{idT}")]
        public IActionResult DeleteTitleBookmarkForUser(int idU, string idT)
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

                        if (!_dataService.DeleteBookmarkForUser(idU, idT))
                        {
                            return NotFound();
                        }

                        return NoContent();

                    }else
                    {
                        return Unauthorized();
                    }

                }
            }
            catch(ArgumentException)
            {
                return Unauthorized();
            }
        }


    }
}
