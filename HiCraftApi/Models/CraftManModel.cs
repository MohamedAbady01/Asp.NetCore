using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using FluentNHibernate.Conventions.Helpers;
using Microsoft.EntityFrameworkCore;

namespace HiCraftApi.Models
{

    public class CraftManModel : ApplicationUser
    {

        public int ImagesOfPastWorksID { get; set; }
        public List<ImageOfPastWork> ImagesOfPastWorks { get; set; }
        public int SpecializID { get; set; }
        public Specialization Specializ { get; set; }


        public List<ServiceRequest> ServiceRequests { get; set; }
        
        public int? RevuewId { get; set; }
        [ForeignKey("ReviewId")]
        public List<Review> Review { get; set; }
        public CraftManModel()
        {

        }

    }

}
