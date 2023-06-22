using Authors_MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace Authors_MVC.Controllers
{
    public class AuthController : Controller
    {
        Uri BaseAddress = new Uri("https://localhost:7200/api");

        private readonly HttpClient _client;
        public AuthController()
        {
            _client = new HttpClient();
            _client.BaseAddress = BaseAddress;
        }
        [HttpGet]
        public  ActionResult Login()
        {
            return View();
        }
    
        [HttpPost]
         public async Task<ActionResult> Login(LoginModel model )
        {
            var loginData = new { Username = model.UserName, Password = model.Password };
            var content = new StringContent(JsonConvert.SerializeObject(loginData), Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _client.PostAsync(BaseAddress+ "/Auth/Login", content);

            if (response.IsSuccessStatusCode)
            {
                string token = await response.Content.ReadAsStringAsync();
                return RedirectToAction("Index", "Author"); // Replace "Home" with the appropriate action
            }
            else
            {
                // Handle login failure, e.g., display an error message
                string errorMessage = await response.Content.ReadAsStringAsync();
                ModelState.AddModelError("", "Failed to retrieve News."+ errorMessage);

                return View();
                
            }
        }
    }
}
