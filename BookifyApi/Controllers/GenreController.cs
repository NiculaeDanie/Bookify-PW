
using AutoMapper;
using Bookify.Dto;
using Bookify.Middleware;
using Domain;
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
        private ICollection<Genre> genres = new List<Genre>();
        private readonly ILogger<GenreController> _logger;
        private readonly IMapper _mapper;
        public GenreController(ILogger<GenreController> logger, IMapper mapper)
        {
            _logger = logger;
            _mapper = mapper;
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
            var mappedResult = _mapper.Map<List<GenreGetDto>>(genres);
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
            Genre genre = genres.FirstOrDefault(x => x.Id == id);
            if(genre == null)
            {
                return NotFound();
            }
            var mappedResult = _mapper.Map<GenreGetDto>(genre);
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
            Genre genre = new Genre()
            {
                Id = Guid.NewGuid().ToString(),
                Name = value.Title,
                BookGenre = new List<BookGenre>()
            };
            genres.Add(genre);
            var mappedResult = _mapper.Map<GenreGetDto>(genre);
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
            Genre genre = genres.FirstOrDefault(x => x.Id == id);
            if(genre == null)
            {
                return NotFound();
            }
            genres.Remove(genre);
            return NoContent();
        }
    }
}
