using System.ComponentModel.DataAnnotations;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Films.WebSite.Models
{
    public class FilmModel
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        public int Year { get; set; }

        [Required]
        public string Director { get; set; }

        [FromForm]
        public IFormFile Poster { get; set; }
    }
}
