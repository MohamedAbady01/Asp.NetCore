using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Task_2Api.Models
{
    public class ServiceModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public double Quantiity { get; set; }
        [JsonIgnore]
        public int ApprovalModelId { get; set; }
        [JsonIgnore]
        public ApprovalModel Approval { get; set; }
    }
}
