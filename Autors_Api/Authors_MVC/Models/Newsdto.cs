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

        [Display(Name = "Publication Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-M-dd}", ApplyFormatInEditMode = true)]
        public DateTime PublicationDate { get; set; }
            [Required(ErrorMessage = "Author Name is required.")]
            public int AuthorId { get; set; }


    }
}

