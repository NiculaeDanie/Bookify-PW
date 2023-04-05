
using Microsoft.AspNetCore.Mvc;
using Bookify.Dto;
using Bookify.Middleware;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Globalization;
using Microsoft.AspNetCore.Http;
using Bookify.Domain.Model;
using Domain;
using AutoMapper;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Bookify.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BookController : ControllerBase
    {
        private ICollection<Book> books = new List<Book>();
        private readonly ILogger<BookController> _logger;
        private readonly IMapper _mapper;
        public BookController(ILogger<BookController> logger, IMapper mapper)
        {
            _logger = logger;
            _mapper = mapper;
            books.Add(new Book() { Id= "1"});
        }

        /// <summary>
        /// Gets all existing books.
        /// </summary>
        /// <returns>All the Books that exists</returns>
        /// <response code="200">Returns all Books</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Get()
        {
            var mappedResult = _mapper.Map<List<BookGetDto>>(books);
            return Ok(mappedResult);
        }

        /// <summary>
        /// Gets an existing book by its id.
        /// </summary>
        /// <returns>The Book with that id</returns>
        /// <param id = "id" ></param>
        /// <response code="200">Returns the Book with that id</response>
        /// <response code="404">If the item does not exist</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(string id)
        {
            Book book = books.FirstOrDefault(x => x.Id == id);
            if (book == null)
            {
                return NotFound();
            }
            var mappedResult = _mapper.Map<BookGetDto>(book);
            return Ok(mappedResult);
        }

        /// <summary>
        /// Creates a new Book.
        /// </summary>
        /// <returns>A newly created Book item</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /Book
        ///     {
        ///        "title": "Item #1",
        ///        "description": "Item #2",
        ///        "releaseDate": "Item #3",
        ///        "content": "Item #4",
        ///        "image": "Item #5"
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">Validation errors</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post([FromForm] BookPutPostDto book)
        {
            Book newBook = new Book()
            {
                Id = Guid.NewGuid().ToString(),
                Title = book.Title,
                BookGenre = new List<BookGenre>(),
                AuthorBook = new List<AuthorBook>(),
                Description = book.Description,
                ReleaseDate = DateTime.Parse(book.RealeaseDate),
                ViewCount = 0
            };
            books.Add(newBook);
            var mappedResult = _mapper.Map<BookGetDto>(newBook);
            return CreatedAtAction(nameof(Get), new { id = mappedResult.Id }, mappedResult);
        }

        /// <summary>
        /// Updates an existing Book.
        /// </summary>
        /// <returns>The Updated Book</returns>
        /// <param id = "id" ></param>
        /// <response code="200">Returns the updated book</response>
        /// <response code="404">If the item does not exist</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PutAsync(string id, [FromForm] BookPostDto value)
        {
            Book book = books.FirstOrDefault(x => x.Id == id);
            if (book == null)
            {
                return NotFound();
            }
            book.Title = value.Title;
            book.Description = value.Description;
            var mappedResult = _mapper.Map<BookGetDto>(book);
            return Ok(mappedResult);
        }

        /// <summary>
        /// Deletes an existing Book.
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
            Book book = books.FirstOrDefault(x => x.Id == id);
            if (book == null)
            {
                return NotFound();
            }
            books.Remove(book);
            return NoContent();
        }

        /// <summary>
        /// Gets all books linked with this genre id.
        /// </summary>
        /// <returns>The Book with that genre id</returns>
        /// <param name = "id" ></param>
        /// <response code="200">Returns the Books with that genre id</response>
        /// /// <response code="404">If the genre does not exist</response>
        [HttpGet("Genre/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByGenre(string id)
        {
            return Ok(books);
        }

        /// <summary>
        /// Gets the content of the book with that id.
        /// </summary>
        /// <returns>The content of the book with that id</returns>
        /// <param name = "id" ></param>
        /// <response code="200">Returns the content of the book with that id</response>
        /// /// <response code="404">If the book or content does not exist</response>
        [HttpGet("Content/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetContent(string id)
        {
            return Ok();
        }

        /// <summary>
        /// Gets the image of the book with that id.
        /// </summary>
        /// <returns>The image of the book with that id</returns>
        /// <param name = "id" ></param>
        /// <response code="200">Returns the image of the book with that id</response>
        /// /// <response code="404">If the book or image does not exist</response>
        [HttpGet("Image/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetImage(string id)
        {
            return Ok();
        }

        /// <summary>
        /// Gets all books linked with this author id.
        /// </summary>
        /// <returns>The Book with that author id</returns>
        /// <param name = "id" ></param>
        /// <response code="200">Returns the Books with that author id</response>
        /// /// <response code="404">If the author does not exist</response>
        [HttpGet("Author/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByAuthor(string id)
        {
            return Ok();
        }

        /// <summary>
        /// Gets all books in favorites.
        /// </summary>
        /// <returns>The Books in favorites</returns>
        /// <param name = "id" ></param>
        /// <response code="200">Returns the Books in favorites</response>
        /// /// <response code="404">If the user does not exist</response>
        [HttpGet("Favorites/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetFavorites(string id)
        {
            return Ok();
        }

        /// <summary>
        /// Gets all books in history.
        /// </summary>
        /// <returns>The Books in history</returns>
        /// <param name = "id" ></param>
        /// <response code="200">Returns the Books in the history</response>
        /// /// <response code="404">If the user does not exist</response>
        [HttpGet("History/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetHistory(string id)
        {
            return Ok();
        }

        /// <summary>
        /// Generates an author book connection.
        /// </summary>
        /// <returns>Ok if successfull</returns>
        /// <param name = "id" ></param>
        /// <response code="200">Ok if successfull</response>
        /// /// <response code="404">If the author or the book does not exist</response>
        [HttpPut("Author/{id}/{bookid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> PutAsyncAuthorToBook(string id, string bookid)
        {
            return Ok();
        }

        /// <summary>
        /// Generates an genre book connection.
        /// </summary>
        /// <returns>Ok if successfull</returns>
        /// <param name = "id" ></param>
        /// <response code="200">Ok if successfull</response>
        /// /// <response code="404">If the genre or the book does not exist</response>
        [HttpPut("Genre/{id}/{bookid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> PutAsyncGenreToBook(string id, string bookid)
        {
            return Ok();
        }

        /// <summary>
        /// Adds a book to favorites.
        /// </summary>
        /// <returns>Ok if successfull</returns>
        /// <param name = "id" ></param>
        /// <response code="200">Ok if successfull</response>
        /// /// <response code="404">If the book does not exist</response>
        [HttpPut("Favorites/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> PutFavorites(string id, string bookid)
        {
            return Ok();
        }

        /// <summary>
        /// Finds a list of books that match the search  string.
        /// </summary>
        /// <returns>All books that match search string</returns>
        /// <param name = "id" ></param>
        /// <response code="200">All books that match search string</response>
        /// /// <response code="404">If none of the books match the search string</response>
        [HttpGet("Search")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Search([FromQuery(Name = "searchstring")] string searchstring)
        {
            return Ok();
        }

    }
}
