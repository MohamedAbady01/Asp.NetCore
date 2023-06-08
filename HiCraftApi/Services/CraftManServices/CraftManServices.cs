using HiCraftApi.Migrations;
using HiCraftApi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using NHibernate.Util;
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
        public async Task EditCraft(string? CraftManId, Craftdto craftMan)
        {
            if (CraftManId == null)
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


                    craft.SpecializID = (int)craftMan.Specializ;
                }
                if (craftMan.UserName != null)
                {


                    craft.UserName = craftMan.UserName;
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
                if (!string.IsNullOrEmpty(craftMan.City))
                {
                    craft.City = craftMan.City;
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
            var craftman = await _context.CraftMens
             .Include(c => c.ImagesOfPastWorks)
            .FirstOrDefaultAsync(c => c.Id == CraftManId);
            if (craftman == null)
            {
                throw new ArgumentException("Craft not found with specified ID");
            }

            if (craftMan.ProfilePicture != null)
            {
                using var datastream = new MemoryStream();
                await craftMan.ProfilePicture.CopyToAsync(datastream);
                craftman.ProfilePicture = datastream.ToArray();
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

                craftman.ImagesOfPastWorks = newImages;
            }

            if (craftMan?.Specializ != null)
            {


                craftman.SpecializID = (int) craftMan.Specializ;
            }
            if (craftMan.UserName != null)
            {
                craftman.UserName = craftMan.UserName;
            }
            if (!string.IsNullOrEmpty(craftMan?.FirstName))
            {
                craftman.FirstName = craftMan.FirstName;
            }

            if (!string.IsNullOrEmpty(craftMan?.LastName))
            {
                craftman.LastName = craftMan.LastName;
            }

            if (!string.IsNullOrEmpty(craftMan?.Location))
            {
                craftman.Location = craftMan.Location;
            }
            if (!string.IsNullOrEmpty(craftMan.City))
            {
                craftman.City = craftMan.City;
            }

            if (!string.IsNullOrEmpty(craftMan?.PhonNumber))
            {
                craftman.PhoneNumber = craftMan.PhonNumber;
            }
            if (!string.IsNullOrEmpty(craftMan?.Bio))
            {
                craftman.Bios = craftMan.Bio;
            }
            await _context.SaveChangesAsync();
        }


        public async Task<List<CraftManModel>> GetAllCrafts(int catid,string City)
        {
            var crafts = await _context.CraftMens
                           .Include(e => e.Review)
                           .Where(e => e.SpecializID == catid && e.City == City)
                           .ToListAsync();

            foreach (var crafman in crafts)
            {
                crafman.OverAllRating = crafman.Review?.Any() == true
                    ? Math.Round(crafman.Review.Average(rev => rev.RateOFthisWork), 2)
                    : 0;
            }

            await _context.SaveChangesAsync();

            return crafts.OrderByDescending(s => s.OverAllRating).ToList();
        }
        public async Task<List<Specialization>> GetAllSpecializations()
        {
            var Specializationss = await _context.Specializations.ToListAsync();

            return Specializationss;
        }

        public  async Task<List<CraftManModel>> GetCraftbyCategoryId(int CategoryId,string City)
        {
            var crafts = await _context.CraftMens
                            .Include(e => e.Review)
                            .Where(e => e.SpecializID == CategoryId && e.City == City)
                            .ToListAsync();

            foreach (var crafman in crafts)
            {
                crafman.OverAllRating = crafman.Review?.Any() == true
                    ? Math.Round(crafman.Review.Average(rev => rev.RateOFthisWork), 2)
                    : 0;
            }

            await _context.SaveChangesAsync();

            return crafts.OrderByDescending(s => s.OverAllRating).ToList();
        }

        public async Task<CraftManModel> GetCraftbyId(string id)
        {
            var craftmanQuery = await _context.CraftMens
                .Include(craftman => craftman.ImagesOfPastWorks)
                .Include(craftman => craftman.Specializ)
                .FirstOrDefaultAsync(craftman => craftman.Id == id);

            if (craftmanQuery != null)
            {
                var reviewsQuery = await _context.Reviews
                    .Where(review => review.CraftmanId == id)
                    .ToListAsync();

                craftmanQuery.Review = reviewsQuery;
               
                craftmanQuery.OverAllRating = craftmanQuery.Review?.Any() == true
                    ? craftmanQuery.Review.Average(r => r.RateOFthisWork)
                    : 0;
                craftmanQuery.OverAllRating = Math.Round(craftmanQuery.OverAllRating, 2);
            }

            return craftmanQuery;
        }

        public async Task<List<Custmer>> GetCustmerById(string id)
        {
            var customer = await _context.Custmers.Where(c => c.Id == id).ToListAsync();
            return customer;
        }
        public async Task<List<Review>> GetAllReviews(string? UserId)
        {
            if (UserId == null)
            {
                // Get the current customer's ID from the HttpContextAccessor
                var craftmanid = _httpContextAccessor?.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
                var CraftMan = await _context.Custmers.FirstOrDefaultAsync(e => e.Id == craftmanid);
                if (CraftMan == null)
                {
                    throw new InvalidOperationException("CraftMan Not Found ");
                }
                var Reviews = await _context.Reviews.Where(c => c.CraftmanId == craftmanid).ToListAsync();
                return Reviews;
            }
            var reviews = await _context.Reviews.Where(c => c.CraftmanId == UserId).ToListAsync();
            return reviews;

        }
        public async Task<List<ServiceRequest>> GetAllRequests(string? UserId)
        {
            if (UserId == null)
            {
                // Get the current customer's ID from the HttpContextAccessor
                var craftmanid = _httpContextAccessor?.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
                var custmer = await _context.Custmers.FirstOrDefaultAsync(e => e.Id == craftmanid);
                if (custmer == null)
                {
                    throw new InvalidOperationException("Not Found ");
                }
                var Requests = await _context.ServiceRequests.Where(c => c.CustomerId == craftmanid).ToListAsync();
                return Requests;
            }
            var requests = await _context.ServiceRequests.Where(c => c.CraftmanId == UserId).ToListAsync();
            return requests;
        }
        public async Task<ServiceRequest> AcceptRequest(string? CraftManId, int RequestId)
        {
            if (CraftManId == null)
            {
                var craftManID = _httpContextAccessor?.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (string.IsNullOrEmpty(craftManID))
                {
                    throw new InvalidOperationException("Invalid CraftMan ID");
                }

                var serviceRequests = await _context.ServiceRequests
                    .FirstOrDefaultAsync(sr => sr.Id == RequestId && sr.CraftmanId == craftManID);

                if (serviceRequests == null)
                {
                    throw new InvalidOperationException("Service Request not found");
                }

                serviceRequests.Status = RequestStatus.Accepted;
                await _context.SaveChangesAsync();

                return serviceRequests;
            }
            var serviceRequest = await _context.ServiceRequests
            .FirstOrDefaultAsync(sr => sr.Id == RequestId && sr.CraftmanId == CraftManId);

            if (serviceRequest == null)
            {
                throw new InvalidOperationException("Service Request not found");
            }

            serviceRequest.Status = RequestStatus.Accepted;
            await _context.SaveChangesAsync();

            return serviceRequest;


        }
        public async Task<ServiceRequest> DeclineRequest(string? CraftManId, int RequestId)
        {
            if (CraftManId == null)
            {
                var craftManID = _httpContextAccessor?.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (string.IsNullOrEmpty(craftManID))
                {
                    throw new InvalidOperationException("Not Authorize");
                }

                var serviceRequests = await _context.ServiceRequests
                    .FirstOrDefaultAsync(sr => sr.Id == RequestId && sr.CraftmanId == craftManID);

                if (serviceRequests == null)
                {
                    throw new InvalidOperationException("Service Request not found");
                }

                serviceRequests.Status = RequestStatus.Declined;
                await _context.SaveChangesAsync();

                return serviceRequests;
            }

            var serviceRequest = await _context.ServiceRequests
                .FirstOrDefaultAsync(sr => sr.Id == RequestId && sr.CraftmanId == CraftManId);

            if (serviceRequest == null)
            {
                throw new InvalidOperationException("Service Request not found");
            }

            serviceRequest.Status = RequestStatus.Declined;
            await _context.SaveChangesAsync();
            return serviceRequest;

        }
        public static double CalculateOverallRating(List<Review> reviews)
        {
            if (reviews == null || reviews.Count == 0)
                return 0;

            double totalRating = 0;
            foreach (var review in reviews)
            {
                totalRating += review.RateOFthisWork;
            }

            return totalRating / reviews.Count;
        }

        public async Task<ImageOfPastWork> DeleteImage(int ImageId)
        {
            var Image = await _context.ImageOfPastWorks.SingleOrDefaultAsync(image => image.Id == ImageId);
            if (Image  == null)
            {
                throw new InvalidOperationException("Image Not Found");
            }
             _context.ImageOfPastWorks.Remove(Image);
            await _context.SaveChangesAsync();
            return Image;
        }
    }
}
