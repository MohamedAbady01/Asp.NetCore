using HiCraftApi.Models;
using HiCraftApi.Services.CraftManServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net.WebSockets;
using System.Security.Claims;

namespace HiCraftApi.Services.Custmers
{
    public class CustmersServices : ICustmers
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CustmersServices(ApplicationDbContext context, UserManager<ApplicationUser> userManager,IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;

        }
        public async Task EditCustmer(Custmrdto Custmer)
        {

           
            var customerid = _httpContextAccessor?.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var cust = await _context.Custmers.FirstOrDefaultAsync(c => c.Id == customerid);
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

        public async Task<List<CraftManModel>> GetCraftbyCategoryNameOrCraftName(string NaME)
        {
            var crafts = await _context.CraftMens.Where(e => e.Specializ.Name == NaME).ToListAsync();
            if (crafts.Count == 0)
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
           public async Task<Review> CreateReview(Review model)
        {
            // Get the current customer's ID from the HttpContextAccessor
            var customerId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var custmer = await _context.Custmers.FirstOrDefaultAsync(e => e.Id == customerId);
            if (custmer == null)
            {
                throw new InvalidOperationException("Craftsmens are not allowed to create reviews.");
            }
            model.ClientID = customerId;
            await _context.Reviews.AddAsync(model);
            await _context.SaveChangesAsync();
            return model;
        }
        public async Task<ServiceRequest> MakeRequest(ServiceRequest model)
        {
            // Get the current customer's ID from the HttpContextAccessor
            var customerId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var custmer = await _context.Custmers.FirstOrDefaultAsync(e => e.Id == customerId);
            if (custmer == null)
            {
                throw new InvalidOperationException("Craftsmens are not allowed to create Request.");
            }
            model.CustomerId = customerId;
            model.Status = RequestStatus.Pending;
            await _context.ServiceRequests.AddAsync(model);
            await _context.SaveChangesAsync();
            return model;
        }

        public async Task<List<ServiceRequest>> GetallRequests()
        {
            // Get the current customer's ID from the HttpContextAccessor
            var customerId = _httpContextAccessor?.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var custmer = await _context.Custmers.FirstOrDefaultAsync(e => e.Id == customerId);
            if (custmer == null)
            {
                throw new InvalidOperationException("Not Found ");
            }
           var requests = await _context.ServiceRequests.Where(c=> c.CustomerId == customerId).ToListAsync();
            return requests;
        }
        public async Task<ServiceRequest> DeleteRequest(int RequestId)
        {
            // Get the current customer's ID from the HttpContextAccessor
            var customerId = _httpContextAccessor?.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var custmer = await _context.Custmers.FirstOrDefaultAsync(e => e.Id == customerId);
            if (custmer == null)
            {
                throw new InvalidOperationException("Craftmens are not allowed Delete this Request");
            }
            var request = await _context.ServiceRequests.FindAsync(RequestId);
            if (request == null)
            {
                throw new InvalidOperationException("Request Not Found ");

            }
            _context.ServiceRequests.Remove(request);
            await _context.SaveChangesAsync();
            return request;
        }




    }
}
