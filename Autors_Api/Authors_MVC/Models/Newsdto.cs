using System.ComponentModel.DataAnnotations;


    namespace Authors_MVC.Models
    {
        public class Newsdto
        {
            [Required(ErrorMessage = "Title is required.")]
            public string Title { get; set; }

            [Required(ErrorMessage = "News content is required.")]
            public string NewsContent { get; set; }

            [Required(ErrorMessage = "Image is required.")]
            public IFormFile Image { get; set; }

            public DateTime PublicationDate { get; set; }

            [Required(ErrorMessage = "Author ID is required.")]
            public int AuthorId { get; set; }
        }
   }

