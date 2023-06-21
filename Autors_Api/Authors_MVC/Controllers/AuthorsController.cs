using Authors_MVC.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Text;
using System.Reflection.Metadata;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Authors_MVC.Controllers
{
    public class AuthorsController : Controller
    {
        Uri BaseAddress = new Uri("https://localhost:7200/api");

        private readonly HttpClient _client;
        public AuthorsController()
        {
            _client = new HttpClient();
            _client.BaseAddress = BaseAddress;
        }
        [HttpGet]
        public IActionResult Index()
        {
            List<Author> authors = new List<Author>();
            HttpResponseMessage httpResponse = _client.GetAsync(_client.BaseAddress + "/authors/GetAllAuthors").Result;
            if (httpResponse.IsSuccessStatusCode)
            {
                try
                {
                    string data = httpResponse.Content.ReadAsStringAsync().Result;


                    authors = JsonConvert.DeserializeObject<List<Author>>(data);
                }
                catch (Exception ex )
                {

                    return View();
                }
            }
            

            return View(authors);
        }
        [HttpGet]
        public IActionResult AddAuthor()
            
        {

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddAuthor(Author author)
        {
            try
            {
                string data = JsonConvert.SerializeObject(author);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

                HttpResponseMessage httpResponse =  _client.PostAsync(_client.BaseAddress + "/authors/AddAuthor", content).Result;

                if (httpResponse.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {

                return View();
            }

            return View();
        }
        [HttpGet]
        public IActionResult EditAuthor(int id)
        {
            try
            {
                Author author = new Author();
                HttpResponseMessage httpResponse = _client.GetAsync(_client.BaseAddress + "/authors/GetAuthor/" + id).Result;
                if (httpResponse.IsSuccessStatusCode)
                {
                    string data = httpResponse.Content.ReadAsStringAsync().Result;
                    author = JsonConvert.DeserializeObject<Author>(data);
                    return View(author);
                }
                else
                {
                    TempData["errorMessage"] = "Failed to retrieve author details.";
                    return View();
                }
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = "An error occurred: " + ex.Message;
                return View();
            }
        }

        [HttpPost]
        public IActionResult EditAuthor(int id, Author author)
        {
            try
            {
                string data = JsonConvert.SerializeObject(author);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Put, $"{_client.BaseAddress}/authors/EditAuthor/{id}");
                request.Content = content;

                HttpResponseMessage httpResponse = _client.SendAsync(request).Result;

                if (httpResponse.IsSuccessStatusCode)
                {
                    TempData["successMessage"] = "Updated";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["errorMessage"] = "Failed to update author.";
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
        public IActionResult Details(int id)
        {
            try
            {
                Author author = new Author();
                HttpResponseMessage httpResponse = _client.GetAsync(_client.BaseAddress + "/authors/GetAuthor/" + id).Result;
                if (httpResponse.IsSuccessStatusCode)
                {
                    string data = httpResponse.Content.ReadAsStringAsync().Result;
                    author = JsonConvert.DeserializeObject<Author>(data);
                    return View(author);
                }
                else
                {
                    TempData["errorMessage"] = "Failed to retrieve author details.";
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
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            try
            {
                HttpResponseMessage httpResponse = await _client.GetAsync($"{_client.BaseAddress}/authors/GetAuthor/{id}");

                if (httpResponse.IsSuccessStatusCode)
                {
                    string data = await httpResponse.Content.ReadAsStringAsync();
                    Author author = JsonConvert.DeserializeObject<Author>(data);
                    return View(author);
                }
                else
                {
                    TempData["errorMessage"] = "Failed to retrieve author details.";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = "An error occurred: " + ex.Message;
                return RedirectToAction("Index");
            }
        }

        [HttpPost,ActionName("DeleteAuthor")]
        public async Task<IActionResult> DeleteAuthorConfirmed(int id)
        {
            try
            {
                HttpResponseMessage httpResponse = await _client.DeleteAsync($"{_client.BaseAddress}/authors/DeleteAuthor/{id}");

                if (httpResponse.IsSuccessStatusCode)
                {
                    TempData["successMessage"] = "Author deleted";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["errorMessage"] = "Failed to delete author.";
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
