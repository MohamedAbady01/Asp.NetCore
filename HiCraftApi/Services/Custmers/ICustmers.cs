using HiCraftApi.Models;
using HiCraftApi.Services.CraftManServices;

namespace HiCraftApi.Services.Custmers
{
    public interface ICustmers
    {
        public Task<List<Custmer>> GetCustmerById(string Id);
        public Task<List<CraftManModel>> GetCraftbyCategoryNameOrCraftName(String Name);
        public Task EditCustmer(String id, Custmrdto Custmers);
        public Task<List<Specialization>> GetAllSpecializations();
        public Task<List<CraftManImageModel>> GetCraftbyId(String id);
       
    }
}
