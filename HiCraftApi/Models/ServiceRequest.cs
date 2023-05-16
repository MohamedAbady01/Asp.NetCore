using System.ComponentModel.DataAnnotations.Schema;
using FluentNHibernate.Conventions.Inspections;
using Microsoft.Build.Framework;

namespace HiCraftApi.Models
{
    public class ServiceRequest
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string CustomerId { get; set; }
        public string CraftmanId { get; set; }
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
