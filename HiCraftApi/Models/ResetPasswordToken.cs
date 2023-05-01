using MessagePack;
using System.ComponentModel.DataAnnotations;

namespace HiCraftApi.Controllers
{
    public class ResetPasswordToken
    {
        [System.ComponentModel.DataAnnotations.Key]
        public string Id { get; set; }
        public string Token { get; set; }
        public string Email { get; set; }
        public DateTime ExpirationTime { get; set; }
    }
}
