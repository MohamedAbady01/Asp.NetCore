using System.ComponentModel.DataAnnotations.Schema;

namespace HiCraftApi.Models
{
    public class ImageOfPastWork
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public Byte[] Images { get; set; }
        public string CraftManId { get; set; }
        public CraftManModel CraftMan { get; set; }
    }
}
