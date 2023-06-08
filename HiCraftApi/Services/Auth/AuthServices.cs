using System.IdentityModel.Tokens.Jwt;
using HiCraftApi.Helpers;
using System.Security.Claims;
using System.Text;
using HiCraftApi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Options;
using System.Linq.Expressions;
using HiCraftApi.Services.CraftManServices;
using System.IO;

namespace HiCraftApi.Services.Auth
{
    public class AuthServices : IAuth
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly JWT _jwt;
        private readonly ApplicationDbContext _context;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AuthServices(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IOptions<JWT> jwt, ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _jwt = jwt.Value;
            _context = context;
            _httpContextAccessor = httpContextAccessor; 
        }

        public async Task<AuthModel> ForgetPasswordAsync(ForgetPasswordModel model)
        {
            var authModel = new AuthModel();
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                authModel.Message = "Email not found.";
                return authModel;
            }
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            var result = await _userManager.ResetPasswordAsync(user, token, model.Password);
            if (result.Succeeded)
            {
                authModel.IsAuthenticated = true;
                authModel.Message = "Password changed.";
            }
            else
            {
                authModel.IsAuthenticated = false;
                authModel.Message = "Failed to change password.";
            }
            return authModel;
        }

        public async Task<AuthModel> LoginAsync(LoginModel model)
        {
            var AuthModel = new AuthModel();
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null || !await _userManager.CheckPasswordAsync(user, model.Password))
            {
                AuthModel.Message = "Email or Password is incorrect!";
                return AuthModel;
            }
            var jwtSecurityToken = await CreateJwtToken(user);
            var rolesList = await _userManager.GetRolesAsync(user);

            AuthModel.IsAuthenticated = true;
            AuthModel.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            AuthModel.Email = user.Email;
            AuthModel.UserName = user.UserName;
            AuthModel.ExpiresOn = jwtSecurityToken.ValidTo;
            AuthModel.ID = user.Id;
            AuthModel.Roles = user.Role;
            AuthModel.FirstName = user.FirstName;
            AuthModel.LastName = user.LastName;
            AuthModel.Location = user.Location;
            AuthModel.PhoneNumber = user.PhoneNumber;
            AuthModel.ProfilePicture = user.ProfilePicture;

            AuthModel.Message = "Login Successed !";
            return AuthModel;
        }

        public async Task<AuthModel> LogOutAsync()
        {
            // Get the authenticated user's ID
            var userId = _httpContextAccessor?.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId != null)
            {
                // Sign out the user
                await _signInManager.SignOutAsync();
            }

            return new AuthModel { IsAuthenticated = false ,Message="LogOut Succssfuly" };
        }


        public async Task<AuthModel> RegisterAsync(RegisterModel model)
        {
            if (await _userManager.FindByEmailAsync(model.Email) != null)
            {
                return new AuthModel { Message = "Email is already registered!" };
            }

            if (await _userManager.FindByNameAsync(model.Username) != null)
            {
                return new AuthModel { Message = "Username is already registered!" };
            }

            byte[] profilePictureBytes = null;
            if (model.ProfilePicture != null)
            {
                using var stream = new MemoryStream();
                await model.ProfilePicture.CopyToAsync(stream);
                profilePictureBytes = stream.ToArray();
            }

            ApplicationUser user;
            if (model.Role == 0)
            {
                user = new Custmer
                {
                    UserName = model.Username,
                    Email = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Location = model.Location,
                    City =model.City,
                    ProfilePicture = profilePictureBytes,
                    Role = Roles.Customer,
                    PhoneNumber = model.PhoneNumber
                  
                };
            }
            else
            {
                user = new CraftManModel
                {
                    UserName = model.Username,
                    Email = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Location = model.Location,
                    City = model.City,
                    ProfilePicture = profilePictureBytes,
                    Role = Roles
                    .CraftMan,
                    SpecializID = (int)model.SpecializationId,
                    PhoneNumber = model.PhoneNumber


                };
            }

            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                return new AuthModel { Message = errors };
            }

            if (model.Role == 0)
            {
                await _context.Custmers.AddAsync((Custmer)user);
                await _userManager.AddToRoleAsync(user, "Custmer");
            }
            else
            {
                await _context.CraftMens.AddAsync((CraftManModel)user);
                await _userManager.AddToRoleAsync(user, "CraftMan");
            }

            await _context.SaveChangesAsync();

            var jwtSecurityToken = await CreateJwtToken(user);
            var roles = await _userManager.GetRolesAsync(user);
            return new AuthModel
            {
                Email = model.Email,
                ExpiresOn = jwtSecurityToken.ValidTo,
                IsAuthenticated = true,
                Roles = model.Role,
                Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
                UserName = model.Username,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Location = model.Location,
                City=model.City,
                PhoneNumber = model.PhoneNumber,
                ID = user.Id,
                Message = "Register"
                ,
                ProfilePicture = user.ProfilePicture

            };
        }


        private async Task<JwtSecurityToken> CreateJwtToken(ApplicationUser user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);
            var roleClaims = new List<Claim>();

            foreach (var role in roles)
                roleClaims.Add(new Claim("roles", role));

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim("uid", user.Id)
            }
            .Union(userClaims)
            .Union(roleClaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
            issuer: _jwt.Issuer,
                audience: _jwt.Audience,
                claims: claims,
                expires: DateTime.Now.AddDays(_jwt.DurationInDays),
                signingCredentials: signingCredentials);

            return jwtSecurityToken;
        }
    }

}
