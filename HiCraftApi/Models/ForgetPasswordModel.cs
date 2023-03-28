
using Microsoft.Build.Framework;

namespace HiCraftApi.Models
{
    public class ForgetPasswordModel
    {
        [Required]
        public String Email { get; set; }
        [Required]

        public String Password { get; set; }

        [Required]
        [System.ComponentModel.DataAnnotations.Compare("Password")]
        public String ConfirmPassword { get; set; }
    }
}
