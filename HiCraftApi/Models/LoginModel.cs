using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;

namespace HiCraftApi.Models
{
    public class LoginModel
    {
        [System.ComponentModel.DataAnnotations.Required,]
        public String Email { get; set; }
        [System.ComponentModel.DataAnnotations.Required, DataType(DataType.Password)] 
        public String Password { get; set; }
    }
}
