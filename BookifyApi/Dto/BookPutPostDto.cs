using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
namespace Bookify.Dto
{
    public class BookPutPostDto
    {
        [Required]
        [MinLength(5)]
        public string Title { get; set; }

        [Required]
        public string RealeaseDate { get; set; }

        [Required]
        [MinLength(5)]
        public string Description { get; set; }

        [Required]
        public IFormFile Content { get; set; }
        [Required]
        public IFormFile Image { get; set; }
    }
}
