using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Autors_Api.Models
{
    public class NewsModel
    {
        public int Id { get; set; }
        [System.ComponentModel.DataAnnotations.Required]
        public string Title { get; set; }
        [System.ComponentModel.DataAnnotations.Required]
        public string NewsContent { get; set; }


        [System.ComponentModel.DataAnnotations.Required]
        public Byte[] Image { get; set; }


        [DataType(DataType.DateTime)]
        public DateTime PublicationDate { get; set; }

        public DateTime CreationDate { get; set; }

        public int AuthorId { get; set; }

        [ForeignKey("AuthorId")]
        public Author Author { get; set; }
    }

}
