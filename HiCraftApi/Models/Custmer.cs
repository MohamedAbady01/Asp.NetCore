namespace HiCraftApi.Models
{
    public class Custmer : ApplicationUser
    {

        public List<Review> Reviews { get; set; }
        public List<ServiceRequest> ServiceRequests { get; set; }
    }
}
