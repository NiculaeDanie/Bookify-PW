using Bookify.Domain.Model;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Books.Commands.AddGenreToBook
{
    public class AddGenreToBookCommand: IRequest<Book>
    {
        public string BookId { get; set; }
        public string GenreId { get; set; }
    }
}
