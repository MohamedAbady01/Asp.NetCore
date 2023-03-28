using System.ComponentModel.DataAnnotations;
using Microsoft.Build.Framework;
using RequiredAttribute = System.ComponentModel.DataAnnotations.RequiredAttribute;

namespace HiCraftApi.Models
{
    public class Review
    {
        public int Id { get; set; }
        public string ClientID { get; set; }
        public Custmer ClientName { get; set; }
        [Required]
        public string Details  { get; set; }
        [System.ComponentModel.DataAnnotations.Required, Range(0, 5)]
        
        public float RateOFthisWork { get; set; }
    }

}
