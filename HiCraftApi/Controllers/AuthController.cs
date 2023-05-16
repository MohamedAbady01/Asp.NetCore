using HiCraftApi.Models;
using HiCraftApi.Services.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HiCraftApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuth _authService;

        public AuthController(IAuth authService)
        {
            _authService = authService;
        }
        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync([FromForm] RegisterModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _authService.RegisterAsync(model);

            if (!result.IsAuthenticated)
                return BadRequest(result.Message);

            return Ok(result);
        }
        [HttpPost("Login")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginModel model) {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _authService.LoginAsync(model);

            if (!result.IsAuthenticated)
                return BadRequest(result.Message);

            return Ok(result);
        
        
        }
        [HttpPost("ChangePassword")]
        public async Task<IActionResult> ChangePasswordAsync([FromBody] ForgetPasswordModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _authService.ForgetPasswordAsync(model);

            if (!result.IsAuthenticated)
                return BadRequest(result.Message);

            return Ok(result.Message);


        }
        [HttpPost("LogOut")]
        public async Task<IActionResult> LogOutAsync()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _authService.LogOutAsync();

            if (!result.IsAuthenticated)
                return Ok(result);
            return BadRequest(result.Message);

        }


    }

}
