using System.ComponentModel.DataAnnotations;

namespace HiCraftApi.Models
{
    public class RegisterModel
    {

        [Required, StringLength(100)]
        public string FirstName { get; set; }

        [Required, StringLength(100)]
        public string LastName { get; set; }

        [Required, StringLength(50)]
        public string Username { get; set; }
        [Required, StringLength(50)]
        public string Location { get; set; }

        [Required, StringLength(128)]
        public string Email { get; set; }


        [Required, StringLength(256),DataType(DataType.Password)]
        public string Password { get; set; }
        [Required, Compare("Password"), DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
        [Required, StringLength(256),DataType(DataType
            .PhoneNumber)]
        public string  PhoneNumber { get; set; }
        public IFormFile? ProfilePicture { get; set; }


        [Required]
        public Roles Role { get; set; }
        public Specializationss? SpecializationId { get; set; }

    }
    
}
public enum Specializationss
{
    نجار, حداد, كهربائى, تعقيم, سباكه, نقاش, زجاج, محاره, ستائر, الكترونيات
}