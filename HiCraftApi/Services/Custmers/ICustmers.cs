using HiCraftApi.Models;
using HiCraftApi.Services.CraftManServices;

namespace HiCraftApi.Services.Custmers
{
    public interface ICustmers
    {
        public Task<Custmer> GetCustmerById(string Id);
        public Task<List<CraftManModel>> GetCraftbyCategoryNameOrCraftName(String Name);
        public Task EditCustmer( Custmrdto Custmers);
        public Task<List<Specialization>> GetAllSpecializations();
        public Task<List<CraftManImageModel>> GetCraftbyId(String id);
        public Task <Review>CreateReview(Review model);
        public Task<ServiceRequest> MakeRequest(ServiceRequest model);
        public Task<List<ServiceRequest>> GetallRequests();
        public Task<ServiceRequest> DeleteRequest(int reqid);

    }
}
