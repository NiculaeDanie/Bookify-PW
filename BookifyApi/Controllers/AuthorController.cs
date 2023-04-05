
using AutoMapper;
using Bookify.Domain.Model;
using Bookify.Dto;
using Bookify.Middleware;
using Domain;
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
        private ICollection<Author> authors = new List<Author>();
        private readonly ILogger<GenreController> _logger;
        private readonly IMapper _mapper;
        public AuthorController(ILogger<GenreController> logger, IMapper mapper)
        {
            _logger = logger;
            _mapper = mapper;
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
            return Ok(authors);
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
            Author author = authors.FirstOrDefault(x => x.Id == id);
            if (author == null)
            {
                return NotFound();
            }
            var mappedResult = _mapper.Map<AuthorGetDto>(author);
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
            Author author = new Author()
            {
                Id = Guid.NewGuid().ToString(),
                Name = value.Name,
                AuthorBook = new List<AuthorBook>(),
                Description = value.Description,
            };
            authors.Add(author);
            var mappedResult = _mapper.Map<AuthorGetDto>(author);
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
            Author author = authors.FirstOrDefault(x => x.Id == id);
            if (author == null)
            {
                return NotFound();
            }
            author.Name = value.Name;
            author.Description = value.Description;
            var mappedResult = _mapper.Map<AuthorGetDto>(author);
            return Ok(mappedResult);
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
            Author author = authors.FirstOrDefault(x => x.Id == id);
            if (author == null)
            {
                return NotFound();
            }
            authors.Remove(author);
            return NoContent();
        }
    }
}
