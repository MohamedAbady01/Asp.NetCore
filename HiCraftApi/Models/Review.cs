using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HiCraftApi.Services.CraftManServices;
using Microsoft.Build.Framework;
using RequiredAttribute = System.ComponentModel.DataAnnotations.RequiredAttribute;

namespace HiCraftApi.Models
{
    public class Review
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string ClientID { get; set; }
        public string CraftmanId { get; set; }
        [Required]
        public string Details  { get; set; }
        

        public string? ClientName { get; set; }
        public string? CraftManName { get; set; }
        [System.ComponentModel.DataAnnotations.Required, Range(0, 5)]
        public float RateOFthisWork { get; set; }

        public Review() { }
    }

}
