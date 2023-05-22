using Bookify.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class BookGenre
    {
        public int Id { get; set; }
        public string BookId { get; set; }
        public string GenreId { get; set; }
        public Book Book { get; set; }
        public Genre Genre { get; set; }
    }
}
