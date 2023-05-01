using System.ComponentModel.DataAnnotations;

namespace HiCraftApi.Models
{
    public class ForgetPassword
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
