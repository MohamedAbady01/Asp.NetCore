using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Task_2Api.Models
{
    public class DiagonsisModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [JsonIgnore]
        public int ApprovalModelId { get; set; }
        [JsonIgnore]
        public ApprovalModel Approval { get; set; }
    }
}
