using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace HiCraftApi.Models
{
    public class AuthModel
    {

        public string Message { get; set; }
        public bool IsAuthenticated { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public Roles Roles { get; set; }
        public string Token { get; set; }
        public DateTime ExpiresOn { get; set; } 
        public string ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Location { get; set; }
        public string City { get; set; }
        public string PhoneNumber { get; set; }
        public byte[] ProfilePicture { get; set; }

    }
}
