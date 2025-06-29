using BookmarkManager.Application.Commands.Collections;
using BookmarkManager.Application.Dto.Collections;
using BookmarkManager.Application.Queries.Collections;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BookmarkManager.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CollectionsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public CollectionsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CollectionDto>>> GetAll()
        {
            var result = await _mediator.Send(new GetAllCollectionsQuery());
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<CollectionDto>> Create([FromBody] CreateCollectionCommand command)
        {
            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetAll), new { id = result.CollectionId }, result);
        }
    }
} 