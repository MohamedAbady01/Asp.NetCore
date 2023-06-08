using HiCraftApi.Models;

namespace HiCraftApi.Services.CraftManServices
{
    public interface ICraftMan
    {
        public Task<List<CraftManModel>> GetAllCrafts(int catid,string City);
        public Task<CraftManModel> GetCraftbyId(String id);
        public Task<List<CraftManModel>> GetCraftbyCategoryId(int CategotyId,string City );
        public Task EditCraft(string? CraftManId, Craftdto craftMan);
        public Task<List<Specialization>>GetAllSpecializations() ;
        public Task<List<Custmer>> GetCustmerById(string Id);
        public Task<List<Review>> GetAllReviews(string? UserId);
        public Task<List<ServiceRequest>> GetAllRequests(string? UserId);
        public Task<ServiceRequest> AcceptRequest(string? CraftManId, int  request);
        public Task<ServiceRequest> DeclineRequest(string? CraftManId, int request);
        public Task<ImageOfPastWork> DeleteImage( int ImageId);
    }
}
