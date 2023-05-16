using HiCraftApi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace HiCraftApi.Services.CraftManServices
{
    public class CraftManServices : ICraftMan
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public CraftManServices(ApplicationDbContext context, UserManager<ApplicationUser> userManager, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task EditCraft(Craftdto craftMan)
        {
            var CraftId = _httpContextAccessor?.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var craft = await _context.CraftMens
                .Include(c => c.ImagesOfPastWorks)
                .FirstOrDefaultAsync(c => c.Id == CraftId);

            if (craft == null)
            {
                throw new ArgumentException("Craft not found with specified ID");
            }

            if (craftMan.ProfilePicture != null)
            {
                using var datastream = new MemoryStream();
                await craftMan.ProfilePicture.CopyToAsync(datastream);
                craft.ProfilePicture = datastream.ToArray();
            }

            if (craftMan.ImagesOfPastWork != null)
            {
                var newImages = new List<ImageOfPastWork>();
                foreach (var newImage in craftMan.ImagesOfPastWork)
                {
                    using var stream = new MemoryStream();
                    await newImage.CopyToAsync(stream);
                    var imageBytes = stream.ToArray();
                    var image = new ImageOfPastWork
                    {
                        Images = imageBytes
                    };
                    newImages.Add(image);
                }

                craft.ImagesOfPastWorks = newImages;
            }

            if (craftMan?.Specializ != null)
            {


                craft.SpecializID = (int) craftMan.Specializ;
            }

            if (!string.IsNullOrEmpty(craftMan?.FirstName))
            {
                craft.FirstName = craftMan.FirstName;
            }

            if (!string.IsNullOrEmpty(craftMan?.LastName))
            {
                craft.LastName = craftMan.LastName;
            }

            if (!string.IsNullOrEmpty(craftMan?.Location))
            {
                craft.Location = craftMan.Location;
            }

            if (!string.IsNullOrEmpty(craftMan?.PhonNumber))
            {
                craft.PhoneNumber = craftMan.PhonNumber;
            }
            if (!string.IsNullOrEmpty(craftMan?.Bio))
            {
                craft.Bios = craftMan.Bio;
            }
            await _context.SaveChangesAsync();
        }


        public async Task<List<CraftManModel>> GetAllCrafts(int catid)
        {
            var crafts = await _context.CraftMens.Where(e => e.SpecializID == catid).ToListAsync();

            return crafts.OrderBy(s=>s.OverAllRating).ToList();
        }

        public async Task<List<Specialization>> GetAllSpecializations()
        {
            var Specializationss = await _context.Specializations.ToListAsync();

            return Specializationss;
        }

        public  async Task<List<CraftManModel>> GetCraftbyCategoryId(int CategoryId)
        {
            var crafts = await _context.CraftMens.Where(e => e.Specializ.Id == CategoryId).ToListAsync();

            return crafts.OrderBy(s => s.OverAllRating).ToList();
        }

        public async Task<List<CraftManModel>> GetCraftbyId(string id)
        {
            var result =await  _context.CraftMens.Where(u => u.Id == id).ToListAsync();

           /*
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
            ).ToListAsync();*/

            return result;
        }

        public async Task<List<Custmer>> GetCustmerById(string id)
        {
            var customer = await _context.Custmers.Where(c => c.Id == id).ToListAsync();
            return customer;
        }
        public async Task<List<ServiceRequest>> GetAllRequests()
        {
            // Get the current customer's ID from the HttpContextAccessor
            var customerId = _httpContextAccessor?.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var custmer = await _context.Custmers.FirstOrDefaultAsync(e => e.Id == customerId);
            if (custmer == null)
            {
                throw new InvalidOperationException("Not Found ");
            }
            var requests = await _context.ServiceRequests.Where(c => c.CustomerId == customerId).ToListAsync();
            return requests;
        }
        public async Task<ServiceRequest> AcceptRequest(int RequestId)
        {

            var craftManID = _httpContextAccessor?.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(craftManID))
            {
                throw new InvalidOperationException("Invalid CraftMan ID");
            }

            var serviceRequest = await _context.ServiceRequests
                .Include(sr => sr.CraftmanId)
                .FirstOrDefaultAsync(sr => sr.Id == RequestId && sr.CraftmanId == craftManID);

            if (serviceRequest == null)
            {
                throw new InvalidOperationException("Service Request not found");
            }

            serviceRequest.Status = RequestStatus.Accepted;
            await _context.SaveChangesAsync();

            return serviceRequest;
        }
        public async Task<ServiceRequest> DeclineRequest(int RequestId)
        {

            var craftManID = _httpContextAccessor?.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(craftManID))
            {
                throw new InvalidOperationException("Not Authorize");
            }

            var serviceRequest = await _context.ServiceRequests
                .Include(sr => sr.CraftmanId)
                .FirstOrDefaultAsync(sr => sr.Id == RequestId && sr.CraftmanId == craftManID);

            if (serviceRequest == null)
            {
                throw new InvalidOperationException("Service Request not found");
            }

            serviceRequest.Status = RequestStatus.Declined;
            await _context.SaveChangesAsync();

            return serviceRequest;
        }

    }
}
