using System.ComponentModel.DataAnnotations;

namespace HiCraftApi.Models
{
    public class Specialization
    {
        

       
        public int Id { get; set; }
        [StringLength(50),RegularExpression(@"^[\u0600-\u06FF\s]+$", ErrorMessage = "Please enter a valid Arabic name.")]
        public string Name { get; set; }


    }
    
}
