
using Application.Genres.Commands.CreateGenre;
using Application.Genres.Commands.DeleteGenre;
using Application.Genres.Queries.GetGenreById;
using Application.Genres.Queries.GetGenreList;
using AutoMapper;
using Bookify.Dto;
using Bookify.Middleware;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Bookify.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class GenreController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ILogger<GenreController> _logger;
        private readonly IMediator _mediator;
        public GenreController(ILogger<GenreController> logger, IMapper mapper, IMediator mediator)
        {
            _logger = logger;
            _mapper = mapper;
            _mediator = mediator;
        }

        /// <summary>
        /// Gets all existing genres.
        /// </summary>
        /// <returns>All the Genres that exists</returns>
        /// <response code="200">Returns all Genres</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Get()
        {
            var result = await _mediator.Send(new GetGenreListQuery());
            var mappedResult = _mapper.Map<List<GenreGetDto>>(result);
            return Ok(mappedResult);
        }

        /// <summary>
        /// Gets an existing genre by its id.
        /// </summary>
        /// <returns>The Genre with that id</returns>
        /// <param id = "id" ></param>
        /// <response code="200">Returns the Genre with that id</response>
        /// <response code="404">If the item does not exist</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(string id)
        {
            var result = await _mediator.Send(new GetGenreByIdQuery
            {
                Id = id
            });
            if (result == null)
            {
                _logger.LogWarning(LogEvents.GetItemNotFound, "Genre id {id} not found", id);
                return NotFound();
            }
            var mappedResult = _mapper.Map<GenreGetDto>(result);
            return Ok(mappedResult);
        }

        /// <summary>
        /// Creates a new Genre.
        /// </summary>
        /// <returns>A newly created Genre item</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /Genre
        ///     {
        ///        "title": "Item #1"
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">If the item is null</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post([FromForm] GenrePutPostDto value)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Post Book bad request");
                return BadRequest(ModelState);
            }
            var command = new CreateGenreCommand
            {
                Genre = value.Title
            };
            var result = await _mediator.Send(command);
            var mappedResult = _mapper.Map<GenreGetDto>(result);

            return CreatedAtAction(nameof(Get), new { id = mappedResult.Id }, mappedResult);
        }

        /// <summary>
        /// Deletes an existing Genre.
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
            var command = new DeleteGenreCommand { GenreId = id };
            var result = await _mediator.Send(command);

            if (result == null)
            {
                _logger.LogWarning(LogEvents.DeleteItemNotFound, "Genre id {id} not found", id);
                return NotFound();
            };

            return NoContent();
        }
    }
    
}
