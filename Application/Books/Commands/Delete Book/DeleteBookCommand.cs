﻿using Bookify.Domain.Model;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Books.Commands.Delete_Book
{
    public class DeleteBookCommand: IRequest<Book>
    {
        public string BookId { get; set; }
    }
}
