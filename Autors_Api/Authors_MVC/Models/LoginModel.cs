using System.ComponentModel.DataAnnotations;

namespace Authors_MVC.Models
{
    public class LoginModel
    {
        [System.ComponentModel.DataAnnotations.Required]
        public string UserName { get; set; }
        [System.ComponentModel.DataAnnotations.Required, DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
