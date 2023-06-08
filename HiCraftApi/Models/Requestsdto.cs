using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HiCraftApi.Models
{
    public class Requestsdto
    {

        public string CustomerId { get; set; }
        public string CraftmanId { get; set; }
        [Required]
        public string Details { get; set; }
        public RequestStatus Status { get; set; }
    }
}
