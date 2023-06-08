using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HiCraftApi.Models
{
    public class Reviewsdto
    {
        [Required]
        public string ClientID { get; set; }
        [Required]
        public string CraftmanId { get; set; }
        [Required]
        public string Details { get; set; }
        [Required]
        public float RateOFthisWork { get; set; }
    }
}
