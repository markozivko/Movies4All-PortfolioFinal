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
        private const int MaxPageSize = 25;


        public TitleBookmarkController(IDataService dataService, IMapper mapper)
        {

            _dataService = dataService;
            _mapper = mapper;

        }

        [HttpGet("{id}", Name = nameof(GetTitleBookmarksForUser))]
        public IActionResult GetTitleBookmarksForUser(int id, int page = 0, int pageSize = 10)
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
                        pageSize = pageSize > MaxPageSize ? MaxPageSize : pageSize;


                        var titleBookmarks = _dataService.GetTitleBookmarkForUser(id, page, pageSize);

                        var result = CreateResult(titleBookmarks, id, page, pageSize);

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

        private object CreateResult(IList<TitleBookmark> tb, int id, int page, int pageSize)
        {
            var items = tb.Select(CreateTitleBookmarkElementDto);

            var count = _dataService.NumberOfBookmarksForUser(id);

            string prev = null;

            if (page > 0)
            {
                prev = Url.Link(nameof(GetTitleBookmarksForUser), new { page = page - 1, pageSize });
            }

            string next = null;

            if (page < (int)Math.Ceiling((double)count / pageSize) - 1)
            {
                next = Url.Link(nameof(GetTitleBookmarksForUser), new { page = page + 1, pageSize });
            }

            var cur = Url.Link(nameof(GetTitleBookmarksForUser), new { page, pageSize });

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
