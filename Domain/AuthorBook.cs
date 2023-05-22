using Bookify.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class AuthorBook
    {
        public int Id { get; set; }
        public string AuthorId { get; set; }
        public string BookId { get; set; }
        public Author Author { get; set; }
        public Book Book { get; set; }
    }
}
