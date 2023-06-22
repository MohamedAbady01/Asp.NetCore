using System.ComponentModel.DataAnnotations;

namespace Autors_Api.Models
{
    public class Author
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Username is required.")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Username must be between 3 and 20 characters.")]
        public string UserName { get; set; }

    }
}
