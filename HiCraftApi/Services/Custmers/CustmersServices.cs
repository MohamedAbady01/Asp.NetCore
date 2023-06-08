using HiCraftApi.Migrations;
using HiCraftApi.Models;
using HiCraftApi.Services.CraftManServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
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
        public async Task EditCustmer(string? CustomerId, Custmrdto Custmer)
        {

            if (CustomerId == null)
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
                if (!string.IsNullOrEmpty(Custmer.City))
                {
                    cust.City = Custmer.City;
                }
                if (!string.IsNullOrEmpty(Custmer.PhonNumber))
                {
                    cust.PhoneNumber = Custmer.PhonNumber;
                }
                if (!string.IsNullOrEmpty(Custmer.UserName))
                {
                    cust.UserName = Custmer.UserName;
                }


                await _context.SaveChangesAsync();
            }
            var customer = await _context.Custmers.FirstOrDefaultAsync(c => c.Id == CustomerId);
            if (customer == null)
            {
                throw new ArgumentException("Craft not found with specified ID");
            }
            if (Custmer.ProfilePicture != null)
            {
                using var datastream = new MemoryStream();
                await Custmer.ProfilePicture.CopyToAsync(datastream);
                customer.ProfilePicture = datastream.ToArray();
            }


            if (!string.IsNullOrEmpty(Custmer.FirstName))
            {
                customer.FirstName = Custmer.FirstName;
            }

            if (!string.IsNullOrEmpty(Custmer.LastName))
            {
                customer.LastName = Custmer.LastName;
            }

            if (!string.IsNullOrEmpty(Custmer.Location))
            {
                customer.Location = Custmer.Location;
            }
            if (!string.IsNullOrEmpty(Custmer.City))
            {
                customer.City = Custmer.City;
            }


            if (!string.IsNullOrEmpty(Custmer.PhonNumber))
            {
                customer.PhoneNumber = Custmer.PhonNumber;
            }
            if (!string.IsNullOrEmpty(Custmer.UserName))
            {
                customer.UserName = Custmer.UserName;
            }


            await _context.SaveChangesAsync();
        }

        public async Task<List<Specialization>> GetAllSpecializations()
        {
            var Specializationss = await _context.Specializations.ToListAsync();

            return Specializationss;
        }

        public async Task<List<CraftManModel>> GetCraftbyCategoryNameOrCraftName(string NaME, string City)
        {
            var crafts = await _context.CraftMens.Where(e => e.Specializ.Name == NaME).ToListAsync();
            if (crafts.Count == 0)
            {
                crafts = await _context.CraftMens.Where(e => e.UserName == NaME && e.City == City).ToListAsync();
            }

            foreach (var crafman in crafts)
            {
                crafman.Review = await _context.Reviews.Where(review => review.CraftmanId == crafman.Id).ToListAsync();
                crafman.OverAllRating = crafman.Review?.Any() == true ? crafman.Review.Average(rev => rev.RateOFthisWork) : 0;
                crafman.OverAllRating = Math.Round(crafman.OverAllRating, 2);
               await _context.SaveChangesAsync();
            }
            return crafts.OrderByDescending(s => s.OverAllRating).ToList();
        }

        public async Task<List<CraftManImageModel>> GetCraftbyId(string id)
        {

            var result = await (
                from u in _context.Users
                join i in _context.ImageOfPastWorks on u.Id equals i.CraftManId
                join r in _context.Reviews on u.Id equals r.CraftmanId into reviews

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
                        Review = reviews.DefaultIfEmpty().ToList() // Retrieve the associated reviews
                    },
                    Image = new ImageOfPastWork
                    {
                        Images = i.Images
                    }
                }
            ).ToListAsync();

            return result;
        }
        public async Task<Custmer> GetCustmerById(string id)
        {
            var customer = await _context.Custmers.FirstOrDefaultAsync(c => c.Id == id);
            return customer;
        }
        public async Task<Review> CreateReview(Reviewsdto model)
        {
            Review review;
            if (model.ClientID == null)
            {
                var customerId = _httpContextAccessor?.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
                var custmers = await _context.Custmers.FirstOrDefaultAsync(e => e.Id == customerId);
                var craftMans = await _context.CraftMens.FirstOrDefaultAsync(e => e.Id == model.CraftmanId);

                if (custmers == null || craftMans == null)
                {
                    throw new InvalidOperationException("Customers or Craftsmen are not allowed to create reviews.");
                }

                model.ClientID = customerId;
                review = new Review()
                {
                    ClientID = model.ClientID,
                    CraftmanId = model.CraftmanId,
                    ClientName = custmers.UserName,
                    CraftManName = craftMans.UserName,
                    RateOFthisWork = model.RateOFthisWork
                };
            }
            else
            {
                var custmer = await _context.Custmers.SingleOrDefaultAsync(e => e.Id == model.ClientID);
                var craftMan = await _context.CraftMens.SingleOrDefaultAsync(e => e.Id == model.CraftmanId);

                if (custmer == null || craftMan == null)
                {
                    throw new InvalidOperationException("Customers or Craftsmen are not allowed to create reviews.");
                }

                review = new Review()
                {
                    ClientID = model.ClientID,
                    CraftmanId = model.CraftmanId,
                    ClientName = custmer.UserName,
                    CraftManName = craftMan.UserName,
                    Details
                    =model.Details,
                    RateOFthisWork = model.RateOFthisWork
                };
            }

            _context.Reviews.Add(review);
            await _context.SaveChangesAsync();
            return review;
        }

        public async Task<ServiceRequest> MakeRequest(Requestsdto model)
        {
            ServiceRequest request;
            if (model.CustomerId == null)
            {
                // Get the current customer's ID from the HttpContextAccessor
                var customerId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
                var custmer = await _context.Custmers.FirstOrDefaultAsync(e => e.Id == customerId);
                if (custmer == null)
                {
                    throw new InvalidOperationException("Craftsmens are not allowed to create Request.");
                }
                var craftMan = await _context.CraftMens.SingleOrDefaultAsync(e => e.Id == model.CraftmanId);
                request = new ServiceRequest()
                {
                CustomerId = customerId,
                Status = RequestStatus.Pending,
                CraftmanId = model.CraftmanId,
                CraftName=craftMan.UserName,
                CustomerName=custmer.UserName,
                Details=model.Details


            };

                _context.ServiceRequests.Add(request);
                await _context.SaveChangesAsync();
                return request;

            }
            var craftMans = await _context.CraftMens.SingleOrDefaultAsync(e => e.Id == model.CraftmanId);
            var Customers = await _context.Custmers.SingleOrDefaultAsync(e => e.Id == model.CustomerId);
            if (Customers == null || craftMans == null)
            {
                throw new InvalidOperationException("Customers or Craftsmen are not allowed to create reviews.");
            }
            request = new ServiceRequest()
            {
                CustomerId = model.CustomerId,
                Status = RequestStatus.Pending,
                CraftmanId = model.CraftmanId,
                CraftName = craftMans.UserName,
                CustomerName = Customers.UserName,
                Details = model.Details


            };


            _context.ServiceRequests.Add(request);
            await _context.SaveChangesAsync();
            return request;
        }

        public async Task<List<ServiceRequest>> GetallRequests(string? UserId)
        {
            if (UserId == null)
            {
                // Get the current customer's ID from the HttpContextAccessor
                var customerId = _httpContextAccessor?.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
                var custmer = await _context.Custmers.FirstOrDefaultAsync(e => e.Id == customerId);
                if (custmer == null)
                {
                    throw new InvalidOperationException("Not Found ");
                }
                var requests1 = await _context.ServiceRequests.Where(c => c.CustomerId == customerId).ToListAsync();
                return requests1;
            }
            var requests = await _context.ServiceRequests.Where(c => c.CustomerId == UserId || c.CraftmanId==UserId).ToListAsync();
            return requests;
        }
        public async Task<AuthModel> DeleteRequest(string? CustomerId,int RequestId)
        {
            if (CustomerId == null)
            {
                // Get the current customer's ID from the HttpContextAccessor
                var customerId = _httpContextAccessor?.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
                var custmer = await _context.Custmers.FirstOrDefaultAsync(e => e.Id == customerId);
                if (custmer == null)
                {
                    throw new InvalidOperationException("Craftmens are not allowed Delete this Request");
                }
                var request1 = await _context.ServiceRequests.FindAsync(RequestId);
                if (request1 == null)
                {
                    throw new InvalidOperationException("Request Not Found ");

                }
                _context.ServiceRequests.Remove(request1);
                await _context.SaveChangesAsync();
                return new AuthModel { Message="Deleted"}
                    ;
            }
            var customer = await _context.Custmers.FirstOrDefaultAsync(e => e.Id == CustomerId);
            if (customer == null)
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
            return new AuthModel { Message = "Deleted" }; 

        }




    }
}
