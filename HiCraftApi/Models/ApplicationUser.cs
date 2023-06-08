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
        [Required, MaxLength(50)]
        public string City { get; set; }
        [Required]
        public Roles Role { get; set; }
        public Byte[]? ProfilePicture { get; set; }
        public string? Bios  { get; set; }
        public double OverAllRating { get; set; }

        public List<Review>? Reviews { get; set; }
    }

    public enum Roles
    {
        Customer , CraftMan
    } 
}
