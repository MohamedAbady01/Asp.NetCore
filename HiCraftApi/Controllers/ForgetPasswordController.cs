using HiCraftApi.Helpers;
using HiCraftApi.Models;
using HiCraftApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;

namespace HiCraftApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ForgotPasswordController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;
        private readonly IMemoryCache _cache;
        private readonly IEmailService _emailService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ForgotPasswordController(IHttpContextAccessor httpContextAccessor, IEmailService emailService, UserManager<ApplicationUser> userManager, IMemoryCache cache, IOptions<JWT> jwt, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
            _cache = cache;
            _emailService = emailService;
            _httpContextAccessor = httpContextAccessor;
        }
        [HttpPost("ForgotPassword")]
        public async Task<IActionResult> ForgotPasswordAsync([FromBody] ForgetPassword model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
                return BadRequest("Invalid email");

            // Generate a random code
            var code = GenerateRandomCode(6);

            // Store the code in the cache with an expiration time
            var cacheOptions = new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5) // Change to a suitable expiration time
            };
            _cache.Set(user.Id, code, cacheOptions);

            // Send the code to the user's email address
            await _emailService.SendEmailAsync(user.Email, "Reset your password", $"Your verification code is: {code}");

            return Ok("Verification code has been sent to your email address.");
        }
        [HttpPost("ResetPassword")]
        public async Task<IActionResult> ResetPasswordAsync([FromBody] ResetPasswordModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
                return BadRequest("Invalid email");

            // Check if the verification code is valid
            if (!_cache.TryGetValue(user.Id, out string cachedCode) || cachedCode != model.Code)
                return BadRequest("Invalid verification code");

            // Reset the user's password
            var resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);
            var result = await _userManager.ResetPasswordAsync(user, resetToken, model.Password);
            if (!result.Succeeded)
                return BadRequest(result.Errors);

            // Clear the verification code from the cache
            _cache.Remove(user.Id);

            return Ok("Password has been reset successfully.");
        }

        private string GenerateRandomCode(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
