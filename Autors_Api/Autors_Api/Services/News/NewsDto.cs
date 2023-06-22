using Autors_Api.Models;
using System.ComponentModel.DataAnnotations;

namespace Autors_Api.Services.News
{
    public class NewsDto
    {
        [System.ComponentModel.DataAnnotations.Required]
        public string Title { get; set; }
        [System.ComponentModel.DataAnnotations.Required]

        public string NewsContent { get; set; }
        public IFormFile? Image { get; set; }
        public DateTime PublicationDate { get; set; }
        [System.ComponentModel.DataAnnotations.Required]
        public int AuthorId { get; set; }

    }
}
