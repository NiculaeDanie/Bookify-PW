
using Application.Authors.Commands.CreateAuthor;
using Application.Authors.Commands.DeleteAuthor;
using Application.Authors.Commands.UpdateAuthor;
using Application.Authors.Queries.GetAuthorById;
using Application.Authors.Queries.GetAuthorList;
using AutoMapper;
using Bookify.Domain.Model;
using Bookify.Dto;
using Bookify.Middleware;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging.EventSource;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Bookify.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AuthorController : ControllerBase
    {
        private readonly ILogger<GenreController> _logger;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        public AuthorController(ILogger<GenreController> logger, IMapper mapper, IMediator mediator)
        {
            _logger = logger;
            _mapper = mapper;
            _mediator = mediator;
        }

        /// <summary>
        /// Gets all existing authors.
        /// </summary>
        /// <returns>All the Authors that exists</returns>
        /// <response code="200">Returns all Authors</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Get()
        {
            var result = await _mediator.Send(new GetAuthorListQuery());
            var mappedResult = _mapper.Map<List<AuthorGetDto>>(result);
            return Ok(mappedResult);
        }

        /// <summary>
        /// Gets an existing author by its id.
        /// </summary>
        /// <returns>The Author with that id</returns>
        /// <param id = "id" ></param>
        /// <response code="200">Returns the Author with that id</response>
        /// <response code="404">If the item does not exist</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(string id)
        {
            var result = await _mediator.Send(new GetAuthorByIdQuery { Id = id });
            if (result == null)
            {
                _logger.LogWarning(LogEvents.GetItemNotFound, "Author id {id} not found", id);
                return NotFound();
            }
            var mappedResult = _mapper.Map<AuthorGetDto>(result);
            return Ok(mappedResult);
        }

        /// <summary>
        /// Creates a new Author.
        /// </summary>
        /// <returns>A newly created Author item</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /Author
        ///     {
        ///        "name": "Item #1",
        ///        "description": "Item #2"
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">Validation errors</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post([FromForm] AuthorPutPostDto value)
        {
            if (!ModelState.IsValid)
            {
                
                return BadRequest(ModelState);
            }
            var command = new CreateAuthorCommand
            {
                Name = value.Name,
                Description = value.Description
            };
            var result = await _mediator.Send(command);
            var mappedResult = _mapper.Map<AuthorGetDto>(result);

            return CreatedAtAction(nameof(Get), new { id = mappedResult.Id }, mappedResult);
        }

        /// <summary>
        /// Updates an existing Author.
        /// </summary>
        /// <returns>The Updated Author</returns>
        /// <param id = "id" ></param>
        /// <response code="200">Returns the updated author</response>
        /// <response code="404">If the item does not exist</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Put(string id, [FromBody] AuthorPutPostDto value)
        {
            var command = new UpdateAuthorCommand
            {
                AuthorId = id,
                Name = value.Name,
                Description = value.Description,
            };
            var result = await _mediator.Send(command);

            if (result == null)
            {
                _logger.LogWarning(LogEvents.UpdateItemNotFound, "Author id {id} not found", id);
                return NotFound();
            }

            return NoContent();
        }

        /// <summary>
        /// Deletes an existing Author.
        /// </summary>
        /// <returns>No content</returns>
        /// <param id = "id" ></param>
        /// <response code="204">Returns nothing if the operation succeds</response>
        /// <response code="404">If the item does not exist</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(string id)
        {
            var command = new DeleteAuthorCommand { AuthorId = id };
            var result = await _mediator.Send(command);

            if (result == null)
            {
                _logger.LogWarning(LogEvents.DeleteItemNotFound, "Author id {id} not found", id);
                return NotFound();
            };

            return NoContent();
        }
    }
}
