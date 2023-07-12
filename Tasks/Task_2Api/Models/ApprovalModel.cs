using Microsoft.CodeAnalysis;
using System.Net.Mail;

namespace Task_2Api.Models
{
    public class ApprovalModel
    {

            public int Id { get; set; }
            public String ApprovalDate { get; set; }
            public TimeSpan ApprovalTime { get; set; }
            public List<ServiceModel> Services { get; set; }
            public List<DiagonsisModel> Diagnoses { get; set; }
            public List<AttachmentModel> Attachments { get; set; }
            public ApprovalStatus Status { get; set; }
        
    }
    public enum ApprovalStatus
    {
        Pending, Approved,Rejected, Deleted


    }

}
