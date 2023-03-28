using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace HiCraftApi.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required, MaxLength(50)]
        public string FirstName { get; set; }

        [Required, MaxLength(50)]
        public string LastName { get; set; }
        [Required, MaxLength(50)]
        public string Location { get; set; }
        [Required]
        public Roles Role { get; set; }
        public Byte[]? ProfilePicture { get; set; }
    }

    public enum Roles
    {
        Customer , CraftMan
    } 
}
