using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace HiCraftApi.Models
{
    public class ImageOfPastWork
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public Byte[] Images { get; set; }
        public string CraftManId { get; set; }
        [JsonIgnore]
        public CraftManModel CraftMan { get; set; }
        public ImageOfPastWork() { }
    }
    
}
