using HiCraftApi.Models;
using HiCraftApi.Services.CraftManServices;
using Microsoft.EntityFrameworkCore;

namespace HiCraftApi.Services.Custmers
{
    public class CustmersServices : ICustmers
    {
        private readonly ApplicationDbContext _context;

        public CustmersServices(ApplicationDbContext context)
        {
            _context = context;


        }
        public async Task EditCustmer(string id, Custmrdto Custmer)
        {
            
            var cust = await _context.Custmers.FirstOrDefaultAsync(c => c.Id == id);

            if (cust == null)
            {
                throw new ArgumentException("Craft not found with specified ID");
            }
            if (Custmer.ProfilePicture != null)
            {
                using var datastream = new MemoryStream();
                await Custmer.ProfilePicture.CopyToAsync(datastream);
                cust.ProfilePicture = datastream.ToArray();
            }


            if (!string.IsNullOrEmpty(Custmer.FirstName))
            {
                cust.FirstName = Custmer.FirstName;
            }

            if (!string.IsNullOrEmpty(Custmer.LastName))
            {
                cust.LastName = Custmer.LastName;
            }

            if (!string.IsNullOrEmpty(Custmer.Location))
            {
                cust.Location = Custmer.Location;
            }

            if (!string.IsNullOrEmpty(Custmer.PhonNumber))
            {
                cust.PhoneNumber = Custmer.PhonNumber;
            }

            await _context.SaveChangesAsync();
        }

        public async Task<List<Specialization>> GetAllSpecializations()
        {
            var Specializationss = await _context.Specializations.ToListAsync();

            return Specializationss;
        }

        public async  Task<List<CraftManModel>> GetCraftbyCategoryNameOrCraftName(string NaME)
        {
            var crafts = await _context.CraftMens.Where(e => e.Specializ.Name == NaME).ToListAsync();
            if(crafts.Count  == 0)
            {
                 crafts = await _context.CraftMens.Where(e => e.UserName == NaME).ToListAsync();
            }
            return crafts.OrderBy(s => s.OverAllRating).ToList();
        }

        public async Task<List<CraftManImageModel>> GetCraftbyId(string id)
        {

            var result = await (
                from u in _context.Users
                join i in _context.ImageOfPastWorks on u.Id equals i.CraftManId
                where u.Id == id
                select new CraftManImageModel
                {
                    CraftMan = new CraftManModel
                    {
                        FirstName = u.FirstName,
                        LastName = u.LastName,
                        Location = u.Location,
                        UserName = u.UserName,
                        Email = u.Email,
                        ProfilePicture = u.ProfilePicture
                        ,
                        PhoneNumber = u.PhoneNumber,
                    },
                    Image = new ImageOfPastWork
                    {
                        Images = i.Images
                    }
                }
            ).ToListAsync();

            return result;
        }
        public async Task<List<Custmer>> GetCustmerById(string id)
        {
            var customer = await _context.Custmers.Where(c => c.Id == id).ToListAsync();
            return customer;
        }

    }
}
