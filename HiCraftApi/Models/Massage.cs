using MessagePack;

namespace HiCraftApi.Models
{
    public class Massage
    {


            public int Id { get; set; }
            public string SenderId { get; set; }
            public ApplicationUser Sender { get; set; }
            public string Content { get; set; }
            public DateTime TimeStamp { get; set; }
        
    }
}
