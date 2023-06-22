using Authors_MVC.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Text;

namespace NewsModels_MVC.Controllers
{
    public class NewsController : Controller
    {
        Uri BaseAddress = new Uri("https://localhost:7200/api");

        private readonly HttpClient _client;
        public NewsController()
        {
            _client = new HttpClient();
            _client.BaseAddress = BaseAddress;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var authors = await GetAuthors();




            List<NewsModel> News = new List<NewsModel>();

            HttpResponseMessage httpResponse = _client.GetAsync(_client.BaseAddress + "/News/GetAllNews").Result;
            if (httpResponse.IsSuccessStatusCode)
            {
                string data = httpResponse.Content.ReadAsStringAsync().Result;
                News = JsonConvert.DeserializeObject<List<NewsModel>>(data);

                foreach (var news in News)
                {
                    var author = authors.FirstOrDefault(a => a.Id == news.AuthorId);
                    if (author != null)
                    {
                        news.UserName = author.UserName;
                    }
                }

            }

            return View(News);
        }
        [HttpGet]
        public async Task<IActionResult> AddNews()
        {
            try
            {
                var authors = await GetAuthors();

                ViewBag.AuthorsForMappig = authors;


                ViewBag.Authors = new SelectList(authors, "Id", "UserName");
                ViewBag.AuthorId = "";

            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Failed to retrieve authors.");
                return View();
            }


            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddNews(Newsdto news, IFormFile Image)
        {

            try
            {
                var authors = await GetAuthors();
                ViewBag.Authors = new SelectList(authors, "Id", "UserName");

                MultipartFormDataContent formData = new MultipartFormDataContent();

                // Add the form data
                formData.Add(new StringContent(news.Title), "Title");
                formData.Add(new StringContent(news.NewsContent), "NewsContent");


                formData.Add(new StringContent(news.PublicationDate.ToString("yyyy-M-dd")), "PublicationDate");
                formData.Add(new StringContent(news.AuthorId.ToString()), "AuthorId");

                // Add the image file
                if (Image != null && Image.Length > 0)
                {
                    var imageContent = new StreamContent(Image.OpenReadStream());
                    formData.Add(imageContent, "Image", Image.FileName);
                }
                DateTime today = DateTime.Today;
                DateTime weekFromToday = today.AddDays(7);

                if (news.PublicationDate < today || news.PublicationDate > weekFromToday)
                {

                    ModelState.AddModelError("", "Publication date must be between today and a week from today.");
                    return View();
                }

                HttpResponseMessage httpResponse = await _client.PostAsync(BaseAddress + "/News/AddNews", formData);

                if (httpResponse.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    string responseContent = await httpResponse.Content.ReadAsStringAsync();

                    ModelState.AddModelError("", "Failed to add news: " + responseContent);
                    return View();
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "An error occurred.");
                return View();
            }
        }

        [HttpGet]
        public async Task<IActionResult> EditNews(int id)
        {

            try
            {

                var authors = await GetAuthors();

                ViewBag.Authors = new SelectList(authors, "Id", "UserName");
                ViewBag.AuthorId = "";

                NewsModel news = new NewsModel();
                HttpResponseMessage httpResponse = _client.GetAsync(_client.BaseAddress + "/News/GetDetails/" + id).Result;
                if (httpResponse.IsSuccessStatusCode)
                {
                    string data = httpResponse.Content.ReadAsStringAsync().Result;
                    news = JsonConvert.DeserializeObject<NewsModel>(data);
                    return View(news);
                }
                else
                {

                    return View();
                }
            }


            catch (Exception ex)
            {
                ModelState.AddModelError("", "Failed to retrieve authors.");
                return View();
            }

        }
        [HttpPost]
        public async Task<IActionResult> EditNews(int Id, Newsdto news, IFormFile? Image)
        {
            try
            {

                var authors = await GetAuthors();
                ViewBag.Authors = new SelectList(authors, "Id", "UserName");

                MultipartFormDataContent formData = new MultipartFormDataContent();

                // Add the form data
                formData.Add(new StringContent(news.Title), "Title");
                formData.Add(new StringContent(news.NewsContent), "NewsContent");
                formData.Add(new StringContent(news.PublicationDate.ToString("yyyy-M-dd")), "PublicationDate");
                formData.Add(new StringContent(news.AuthorId.ToString()), "AuthorId");

                if (Image != null && Image.Length > 0)
                {
                    var imageContent = new StreamContent(Image.OpenReadStream());
                    formData.Add(imageContent, "Image", Image.FileName);
                }
                DateTime today = DateTime.Today;
                DateTime weekFromToday = today.AddDays(7);

                if (news.PublicationDate < today || news.PublicationDate > weekFromToday)
                {

                    ModelState.AddModelError("", "Publication date must be between today and a week from today.");
                    return View();
                }

                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Put, $"{_client.BaseAddress}/News/EditNews/{Id}");
                request.Content = formData;

                HttpResponseMessage httpResponse = await _client.SendAsync(request);

                if (httpResponse.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    string responseContent = await httpResponse.Content.ReadAsStringAsync();
                    ModelState.AddModelError("", "Failed to edit news: " + responseContent);
                    return View();
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "An error occurred.");
                return View();
            }
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            try
            {

                var authors = await GetAuthors();
                HttpResponseMessage httpResponse = _client.GetAsync(_client.BaseAddress + "/News/GetDetails/" + id).Result;
                if (httpResponse.IsSuccessStatusCode)
                {
                    string data = httpResponse.Content.ReadAsStringAsync().Result;
                    var News = JsonConvert.DeserializeObject<NewsModel>(data);

                    var author = authors.FirstOrDefault(a => a.Id == News.AuthorId);
                    if (author != null)
                    {
                        News.UserName = author.UserName;


                    }
                    return View(News);
                }
                else
                {
                    return View();
                }
            }
            catch (Exception ex)
            {
            
                return View();
            }
        }
        [HttpGet]
        public async Task<IActionResult> DeleteNews(int id)
        {
            try
            {
                NewsModel news = new NewsModel();
                var authors = await GetAuthors();
                HttpResponseMessage httpResponse = _client.GetAsync(_client.BaseAddress + "/News/GetDetails/" + id).Result;
                if (httpResponse.IsSuccessStatusCode)
                {
                    string data = httpResponse.Content.ReadAsStringAsync().Result;
                    news = JsonConvert.DeserializeObject<NewsModel>(data);
                    var author = authors.FirstOrDefault(a => a.Id == news.AuthorId);
                    return View(news);
                }
                else
                {
                    TempData["errorMessage"] = "Failed to retrieve News details.";
                    return View();
                }
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = "An error occurred: " + ex.Message;
                return RedirectToAction("Index");
            }
        }

        [HttpPost, ActionName("DeleteNews")]
        public async Task<IActionResult> DeleteNewsConfirmed(int id)
        {
            try
            {
                HttpResponseMessage httpResponse = await _client.DeleteAsync($"{_client.BaseAddress}/News/DeleteNews/{id}");

                if (httpResponse.IsSuccessStatusCode)
                {
                    TempData["successMessage"] = "News deleted";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["errorMessage"] = "Failed to delete News.";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = "An error occurred: " + ex.Message;
                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetNewsByAuthorName(string AuthorName)
        {
            var authors = await GetAuthors();

            List<NewsModel> News = new List<NewsModel>();

            HttpResponseMessage httpResponse = await _client.GetAsync(_client.BaseAddress + "/News/GetNews/" + AuthorName);
            if (httpResponse.IsSuccessStatusCode)
            {
                string data = await httpResponse.Content.ReadAsStringAsync();
                News = JsonConvert.DeserializeObject<List<NewsModel>>(data);

                foreach (var news in News)
                {
                    var author = authors.FirstOrDefault(a => a.Id == news.AuthorId);
                    if (author != null)
                    {
                        news.UserName = author.UserName;
                    }
                }

                return View(News);
            }
            ModelState.AddModelError("", "Failed to retrieve News.");
            return View();
        }

        private async Task<List<Author>> GetAuthors()
        {
            List<Author> authors = new List<Author>();
            HttpResponseMessage httpResponse = _client.GetAsync(_client.BaseAddress + "/authors/GetAllAuthors").Result;
            if (httpResponse.IsSuccessStatusCode)
            {
                try
                {
                    string data = httpResponse.Content.ReadAsStringAsync().Result;


                    authors = JsonConvert.DeserializeObject<List<Author>>(data);



                    return authors;
                }
                catch (Exception ex)
                {
                    return authors;

                }

            }

            return authors;
        }

        }

    
}