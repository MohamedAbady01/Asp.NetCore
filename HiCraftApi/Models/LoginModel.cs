using Microsoft.Build.Framework;

namespace HiCraftApi.Models
{
    public class LoginModel
    {
        [Required]
        public String Email { get; set; }
        [Required]  
        public String Password { get; set; }
    }
}
