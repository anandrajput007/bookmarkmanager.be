using BookmarkManager.Application.Commands.Bookmarks;
using BookmarkManager.Application.Dto.Bookmarks;
using BookmarkManager.Application.Queries.Bookmarks;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BookmarkManager.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookmarksController : ControllerBase
    {
        private readonly IMediator _mediator;
        public BookmarksController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<BookmarkDto>>> GetAll()
        {
            var result = await _mediator.Send(new GetAllBookmarksQuery());
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<BookmarkDto>> Create([FromBody] CreateBookmarkCommand command)
        {
            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetAll), new { id = result.BookmarkId }, result);
        }
    }
} 