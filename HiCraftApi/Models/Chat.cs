using static HiCraftApi.Models.Massage;

namespace HiCraftApi.Models
{
    public class Chat
    {
        public int Id { get; set; }
        public string CustomerId { get; set; }
        public Custmer Customer { get; set; }
        public string CraftmanId { get; set; }
        public CraftManModel Craftman { get; set; }
        public List<Massage> Messages { get; set; }

        public Chat()
        {
            Messages = new List<Massage>();
        }
    }
}
