﻿using Bookify.Domain.Model;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Authors.Commands.AddBookToAuthor
{
    public class AddBookToAuthorCommand: IRequest<Book>
    {
        public string AuthorId { get; set; }
        public string BookId { get; set; }
    }
}
