using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.Build.Framework;

namespace HiCraftApi.Models
{
    public class ServiceRequest
    {
        public int Id { get; set; }
        [ForeignKey("Custmer")]
        public string CustomerId { get; set; }
        public Custmer Customer { get; set; }
        [ForeignKey("CraftManModel")]
        public int CraftmanId { get; set; }
        public CraftManModel Craftman { get; set; }
        [Column("Request Details")]
        [Required]
        public string Details { get; set; }
        public bool IsAccepted { get; set; }
    }
}
