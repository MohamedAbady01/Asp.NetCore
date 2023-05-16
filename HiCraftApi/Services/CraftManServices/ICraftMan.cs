using HiCraftApi.Models;

namespace HiCraftApi.Services.CraftManServices
{
    public interface ICraftMan
    {
        public Task<List<CraftManModel>> GetAllCrafts(int catid);
        public Task<List<CraftManImageModel>> GetCraftbyId(String id);
        public Task<List<CraftManModel>> GetCraftbyCategoryName(String categotyName );
        public Task EditCraft(Craftdto craftMan);
        public Task<List<Specialization>>GetAllSpecializations() ;
        public Task<List<Custmer>> GetCustmerById(string Id);
        public Task<List<ServiceRequest>> GetAllRequests();
        public Task<ServiceRequest> AcceptRequest(int  request);
        public Task<ServiceRequest> DeclineRequest(int request);
    }
}
