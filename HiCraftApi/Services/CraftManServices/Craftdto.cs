using HiCraftApi.Models;
using System.ComponentModel.DataAnnotations;

namespace HiCraftApi.Services.CraftManServices
{
    public class Craftdto
    {
        [ StringLength(100)]
        public string FirstName { get; set; }

        [StringLength(100)]
        public string LastName { get; set; }
        [StringLength(50)]
        public  string UserName { get; set; }
        public string Location { get; set; }
        public string PhonNumber { get;set
                ;
        }   
        public IFormFile? ProfilePicture { get; set; }
        public List<IFormFile> ImagesOfPastWork { get; set; }
        public Specializationss Specializ { get; set; }

        public Craftdto()
        {
        }
    }
}
