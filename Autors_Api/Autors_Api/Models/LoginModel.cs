using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;

namespace Autors_Api.Models
{
    public class LoginModel
    {
        [System.ComponentModel.DataAnnotations.Required]
        public string UserName { get; set; }
        [System.ComponentModel.DataAnnotations.Required,DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
