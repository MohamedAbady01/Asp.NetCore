using Authors_MVC.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
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
        public IActionResult Index()
        {
            List<NewsModel> News = new List<NewsModel>();
            HttpResponseMessage httpResponse = _client.GetAsync(_client.BaseAddress + "/News/GetAllNews").Result;
            if (httpResponse.IsSuccessStatusCode)
            {
                string data = httpResponse.Content.ReadAsStringAsync().Result;
                News = JsonConvert.DeserializeObject<List<NewsModel>>(data);
            }

            return View(News);
        }
        [HttpGet]
        public async Task<IActionResult> AddNews()
        {
            await SetAuthorsViewBag();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddNews(Newsdto news, IFormFile image)
        {
            try
            {
                HttpResponseMessage authorsResponse = await _client.GetAsync("/authors/GetAllAuthors");
                if (authorsResponse.IsSuccessStatusCode)
                {
                    MultipartFormDataContent formData = new MultipartFormDataContent();

                    var authorsData = await authorsResponse.Content.ReadAsStringAsync();
                    var authors = JsonConvert.DeserializeObject<List<Author>>(authorsData);
                    var selectedAuthor = authors.FirstOrDefault(a => a.Id == news.AuthorId);
                    int selectedAuthorId = selectedAuthor?.Id ?? 0;

                    if (image != null && image.Length > 0)
                    {
                        using (var memoryStream = new MemoryStream())
                        {
                            await image.CopyToAsync(memoryStream);
                            byte[] imageData = memoryStream.ToArray();
                            formData.Add(new ByteArrayContent(imageData), "image", image.FileName);
                        }
                    }

                    news.AuthorId = selectedAuthorId;
                    news.Image = image;

                    var jsonData = JsonConvert.SerializeObject(news);
                    var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

                    HttpResponseMessage httpResponse = await _client.PostAsync("/News/AddNews", content);

                    if (httpResponse.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Failed to add news.");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Failed to retrieve authors.");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "An error occurred: " + ex.Message);
            }

            await SetAuthorsViewBag();

            return View(news);
        }

        private async Task SetAuthorsViewBag()
        {
            try
            {
                HttpResponseMessage authorsResponse = await _client.GetAsync("/authors/GetAllAuthors");
                if (authorsResponse.IsSuccessStatusCode)
                {
                    var authorsData = await authorsResponse.Content.ReadAsStringAsync();
                    var authors = JsonConvert.DeserializeObject<List<Author>>(authorsData);
                    var selectListItems = authors.Select(a => new SelectListItem { Value = a.Id.ToString(), Text = a.UserName }).ToList();
                    ViewBag.Authors = selectListItems;
                }
                else
                {
                    ModelState.AddModelError("", "Failed to retrieve authors.");
                    ViewBag.Authors = new List<SelectListItem>();
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "An error occurred: " + ex.Message);
                ViewBag.Authors = new List<SelectListItem>();
            }
        }


        [HttpGet]
        public IActionResult Details(int id)
        {
            try
            {
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
                    TempData["errorMessage"] = "Failed to retrieve News details.";
                    return View();
                }
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = "An error occurred: " + ex.Message;
                return View();
            }
        }
        [HttpGet]
        public async Task<IActionResult> DeleteNews(int id)
        {
            try
            {
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
        public async Task<IActionResult> DeleteAuthorConfirmed(int id)
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



    }


}
