namespace Task_2Api.Models
{
    public class Approvaldto
    {
        public string ServicesName { get; set; }
        public double ServicesQuantitty{ get; set; }
        public string DiagnoseName { get; set; }
        public List<IFormFile> Attachments { get; set; }
        public ApprovalStatus Status { get; set; }
    }
}
