using Autors_Api.Helpers;
using Autors_Api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Autors_Api.Services.Auth
{
    public class AuthServices : IAuth
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly JWT _jwt;
        private readonly ApplicationDBContext _context;


        public AuthServices(UserManager<ApplicationUser> userManager,ApplicationDBContext dBContext , JWT jWT)
        {
            _userManager = userManager;
            _jwt = jWT; 
            _context = dBContext;
        }

        public async Task<AuthModel> LogInAsync(LoginModel model)
        {

                var AuthModel = new AuthModel();
                var user = await _userManager.FindByEmailAsync(model.UserName);
                if (user == null || !await _userManager.CheckPasswordAsync(user, model.Password))
                {
                    AuthModel.Message = "UserName or Password is incorrect!";
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

                AuthModel.Message = "Login Successed !";
                return AuthModel;
            
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
