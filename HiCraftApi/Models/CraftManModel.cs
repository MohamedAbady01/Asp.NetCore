using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using Microsoft.EntityFrameworkCore;

namespace HiCraftApi.Models
{

    public class CraftManModel : ApplicationUser
    {

        public int ImagesOfPastWorksID { get; set; }
        public List<ImageOfPastWork> ImagesOfPastWorks { get; set; }
        public int SpecializID { get; set; }
        public Specialization Specializ { get; set; }



        public int CommentID { get; set; }
        public List<Review> UserComment { get; set; }
        [Range(0, 5)]
        public double OverAllRating => UserComment?.Any() ?? false ? UserComment.Average(r => r.RateOFthisWork) : 0;
        public CraftManModel()
        {
            UserComment = new List<Review>();
        }

    }

}
