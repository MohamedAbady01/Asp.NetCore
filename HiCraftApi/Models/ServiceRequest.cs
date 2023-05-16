using System.ComponentModel.DataAnnotations.Schema;
using FluentNHibernate.Conventions.Inspections;
using Microsoft.Build.Framework;

namespace HiCraftApi.Models
{
    public class ServiceRequest
    {
        public int Id { get; set; }
        [ForeignKey("CraftManModel")]
        public string CustomerId { get; set; }
        public Custmer Customer { get; set; }
        [ForeignKey("CraftManModel")]
        public string CraftmanId { get; set; }
        public CraftManModel Craftman { get; set; }
        [Column("Request Details")]
        [Required]
        public string Details { get; set; }
        public RequestStatus Status { get; set; }
    }
    public enum RequestStatus
    {
        Pending,
        Accepted,
        Declined
    }

}
