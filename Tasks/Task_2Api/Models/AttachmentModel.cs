using Newtonsoft.Json;

namespace Task_2Api.Models
{
    public class AttachmentModel
    {
        public int Id { get; set; } 
        public byte[] Images { get; set; }
        public int ApprovalModelId { get; set; }

        [JsonIgnore]
        public ApprovalModel Approval{ get; set; }
    }
}
