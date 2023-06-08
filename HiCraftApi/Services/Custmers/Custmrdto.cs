using System.ComponentModel.DataAnnotations;

namespace HiCraftApi.Services.Custmers
{
    public class Custmrdto
    {
        [StringLength(100)]
        public string FirstName { get; set; }

        [StringLength(100)]
        public string LastName { get; set; }
        [StringLength(50)]
        public string UserName { get; set; }
        public string Location { get; set; }
        public string City { get; set; }
        public string PhonNumber
        { get; set;}
        public IFormFile? ProfilePicture { get; set; }
    }
}
