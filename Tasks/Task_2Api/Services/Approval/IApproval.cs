using Task_2Api.Models;

namespace Task_2Api.Services.ApprovalServices
{
    public interface IApproval
    {
        public Task<ApprovalModel> GetApprovalById(int Id);
        public List<ApprovalModel> GetAllApprovals();
        public Task CreateApprovalModel(Approvaldto model);
        public Task UpdateApproval(int ApprovalId, Approvaldto  approvaldto);    
        public Task DeleteApproval(int ApprovalId);
    }
}
