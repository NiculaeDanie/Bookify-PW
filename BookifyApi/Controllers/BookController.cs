
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
using MediatR;
using Application.Books.Queries.GetBookList;
using Application.Books.Queries.GetBookById;
using Application.Books.Commands.CreateBook;
using Application.Books.Commands.UpdateBook;
using Application.Books.Commands.Delete_Book;
using Application.Books.Queries.GetBookByGenre;
using Application.Users.Queries.GetBookContent;
using Application.Books.Queries.GetBookImage;
using Application.Authors.Queries.GetAuthorBooks;
using Application.Users.Queries.GetUserFavorites;
using Application.Users.Queries.GetUserHistory;
using Application.Books.Commands.AddGenreToBook;
using Application.Authors.Commands.AddBookToAuthor;
using Application.Users.Commands.AddBookToFavorites;
using Application.Users.Commands.DeleteBookFromFavorit;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Bookify.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BookController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<BookController> _logger;
        private readonly IMapper _mapper;
        private readonly UserManager<User> userManager;
        public BookController(ILogger<BookController> logger, IMapper mapper, IMediator mediator, UserManager<User> userManager)
        {
            _logger = logger;
            _mapper = mapper;
            _mediator = mediator;
            this.userManager = userManager;
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
            var result = await _mediator.Send(new GetBookListQuery());
            var mappedResult = _mapper.Map<List<BookGetDto>>(result);
            return Ok(mappedResult);
        }

        /// <summary>
        /// Gets an existing book by its id.
        /// </summary>
        /// <returns>The Book with that id</returns>
        /// <param id = "id" ></param>
        /// <response code="200">Returns the Book with that id</response>
        /// <response code="404">If the item does not exist</response>
        [HttpGet("{id}/{userid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(string id,string userid)
        {
            var result = await _mediator.Send(new GetBookByIdQuery
            {
                BookId = id
            });
            if (result == null)
            {
                _logger.LogWarning(LogEvents.GetItemNotFound, "Book id {id} not found", id);
                return NotFound();
            }
            var mappedResult = _mapper.Map<FullBookDto>(result);
            
            if (result.UserFavorites != null)
            {
                if (result.UserFavorites.Any(x => x.UserId == userid))
                {
                    mappedResult.IsFavorited = true;
                }
            }

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
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Post Book bad request");
                return BadRequest(ModelState);
            }
            var command = new CreateBookCommand
            {
                Title = book.Title,
                Description = book.Description,
                Content = book.Content,
                Image = book.Image
            };
            var result = await _mediator.Send(command);
            var mappedResult = _mapper.Map<BookGetDto>(result);

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
            var command = new UpdateBookCommand
            {
                BookId = id,
                Title = value.Title,
                Content = value.Content,
                Description = value.Description,
                Created = value.RealeaseDate
            };
            var result = await _mediator.Send(command);

            if (result == null)
            {
                _logger.LogWarning(LogEvents.UpdateItemNotFound, "Book id {id} not found", id);
                return NotFound();
            }

            return NoContent();
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
            var command = new DeleteBookCommand { BookId = id };
            var result = await _mediator.Send(command);

            if (result == null)
            {
                _logger.LogWarning(LogEvents.DeleteItemNotFound, "Book id {id} not found", id);
                return NotFound();
            };

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
            var result = await _mediator.Send(new GetBookByGenreQuery
            {
                GenreId = id
            });
            if (result == null)
            {
                _logger.LogWarning(LogEvents.GetItemNotFound, "Genre id {id} not found", id);
                return NotFound();
            }

            var mappedResult = _mapper.Map<List<FullBookDto>>(result);

            return Ok(mappedResult);
        }

        /// <summary>
        /// Gets the content of the book with that id.
        /// </summary>
        /// <returns>The content of the book with that id</returns>
        /// <param name = "id" ></param>
        /// <response code="200">Returns the content of the book with that id</response>
        /// /// <response code="404">If the book or content does not exist</response>
        [HttpGet("Content/{id}/{userid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetContent(string id, string userid)
        {
            var user = await userManager.FindByIdAsync(userid);
            var result = await _mediator.Send(new GetBookContentQuery
            {
                UserId = user.Email,
                BookId = id
            });
            if (result == null)
            {
                _logger.LogWarning(LogEvents.GetItemNotFound, "Book id {id} not found", id);
                return NotFound();
            }
            return File(result.Content, "application/pdf", result.Name); ;
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
            var result = await _mediator.Send(new GetBookImageQuery
            {
                BookId = id
            });
            if (result == null)
            {
                _logger.LogWarning(LogEvents.GetItemNotFound, "Book id {id} not found", id);
                return NotFound();
            }
            return File(result.Content, result.ContentType, result.Name); ;
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
            var result = await _mediator.Send(new GetAuthorBooksQuery
            {
                AuthorId = id
            });

            if (result == null)
            {
                _logger.LogWarning(LogEvents.GetItemNotFound, "Author id {id} not found", id);
                return NotFound();
            };
            var mappedResult = _mapper.Map<List<FullBookDto>>(result);
            return Ok(mappedResult);
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
            var user = await userManager.FindByIdAsync(id);
            var result = await _mediator.Send(new GetUserFavoritesQuery
            {
                UserId = user.Email
            });
            if (result == null)
            {
                return NotFound();
            }
            var mappedResult = _mapper.Map<List<FullBookDto>>(result);
            return Ok(mappedResult); ;
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
            var user = await userManager.FindByIdAsync(id);
            var result = await _mediator.Send(new GetUserHistoryQuery
            {
                UserId = user.Email
            });
            if (result == null)
            {
                return NotFound();
            }
            var mappedResult = _mapper.Map<List<FullBookDto>>(result);
            return Ok(mappedResult);
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
            var command = new AddBookToAuthorCommand
            {
                AuthorId = id,
                BookId = bookid
            };
            var result = await _mediator.Send(command);

            if (result == null)
                return NotFound();

            return NoContent();
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
            var command = new AddGenreToBookCommand
            {
                GenreId = id,
                BookId = bookid
            };
            var result = await _mediator.Send(command);

            if (result == null)
                return NotFound();

            return NoContent();
        }

        /// <summary>
        /// Adds a book to favorites.
        /// </summary>
        /// <returns>Ok if successfull</returns>
        /// <param name = "id" ></param>
        /// <response code="200">Ok if successfull</response>
        /// /// <response code="404">If the book does not exist</response>
        [HttpPut("Favorites/{bookid}/{userid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> PutFavorites(string userid, string bookid)
        {
            var user = await userManager.FindByIdAsync(userid);
            var result = await _mediator.Send(new AddBookToFavoritesCommand
            {
                UserId = user.Email,
                BookId = bookid
            });
            if (result == null)
                return NoContent();
            var mappedResult = _mapper.Map<FullBookDto>(result);
            return Ok(mappedResult);
        }

        [HttpDelete("Favorites/{bookid}/{userid}")]
        public async Task<IActionResult> Delete(string userid, string bookid)
        {
            var user = await userManager.FindByIdAsync(userid);
            var command = new DeleteBookFromFavoritesCommand { UserId = user.Email, BookId = bookid };
            var result = await _mediator.Send(command);

            if (result == null)
                return NotFound();
            var mappedResult = _mapper.Map<BookGetDto>(result);
            return Ok(mappedResult);
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
