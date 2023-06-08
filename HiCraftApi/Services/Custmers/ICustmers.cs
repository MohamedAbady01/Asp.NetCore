using HiCraftApi.Models;
using HiCraftApi.Services.CraftManServices;

namespace HiCraftApi.Services.Custmers
{
    public interface ICustmers
    {
        public Task<Custmer> GetCustmerById(string Id);
        public Task<List<CraftManModel>> GetCraftbyCategoryNameOrCraftName(String Name, string City);
        public Task EditCustmer( string? CustomerId,Custmrdto Custmers);
        public Task<List<Specialization>> GetAllSpecializations();
        public Task<List<CraftManImageModel>> GetCraftbyId(String id);
        public Task <Review>CreateReview(Reviewsdto model);
        public Task<ServiceRequest> MakeRequest(Requestsdto model);
        public Task<List<ServiceRequest>> GetallRequests(string? UserId);
        public Task<AuthModel> DeleteRequest(string? CustomerId, int reqid);

    }
}
