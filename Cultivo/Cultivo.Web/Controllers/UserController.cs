using Cultivo.Domain.Constants;
using Cultivo.Domain.Models;
using Cultivo.Web.DTOs;
using Cultivo.Web.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Drawing.Printing;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;

namespace Cultivo.Web.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string _filePath;

        public UserController(IWebHostEnvironment environment)
        {
            _filePath = environment.WebRootPath;
            _httpClient = new HttpClient();
        }

        [AllowAnonymous]
        public IActionResult Create()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Create(NewUserViewDTO dto, IFormFile attachment)
        {
            try
            {
                if (dto == null)
                {
                    return View(dto);
                }

                if(attachment != null)
                {
                    if (!ValidateImage(attachment))
                    {
                        return View(dto);
                    }

                    var file = SaveFile(attachment);

                    dto.PhotoURL = file;
                }

                StringContent content = new StringContent(JsonConvert.SerializeObject(dto), Encoding.UTF8, Endpoints.HEADER_VALUE);

                var response = await _httpClient.PostAsync(Endpoints.API_URL + Endpoints.ENDPOINT_USER, content);

                if (response.IsSuccessStatusCode)
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    JsonConvert.DeserializeObject<UserViewModel>(apiResponse);
                }

                return RedirectToAction("Login", "Auth");
            }
            catch
            {
                return View();
            }
        }

        [HttpGet]
        public async Task<IActionResult> Profile(string email)
        {
            var accessToken = HttpContext.Session.GetString("JWToken");

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var response = await _httpClient.GetStringAsync(Endpoints.API_URL + Endpoints.ENDPOINT_USER + "/" + email);
            var user = JsonConvert.DeserializeObject<UserWithPostsViewDTO>(response);

            if (user.PhotoURL != null || user.PhotoURL != "") 
                ViewBag.Photo = user.PhotoURL;

            return View(user);
        }

        [HttpGet]
        public async Task<IActionResult> MyProfile()
        {
            var accessToken = HttpContext.Session.GetString("JWToken");
            var email = HttpContext.Session.GetString("Email");
            
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            
            var response = await _httpClient.GetStringAsync(Endpoints.API_URL + Endpoints.ENDPOINT_USER + "/" + email);
            var user = JsonConvert.DeserializeObject<UserWithPostsViewDTO>(response);

            if (user.PhotoURL != null || user.PhotoURL != "")
                ViewBag.Photo = user.PhotoURL;

            return View(user);
        }

        [HttpGet]
        public async Task<IActionResult> Edit()
        {
            var accessToken = HttpContext.Session.GetString("JWToken");
            var email = HttpContext.Session.GetString("Email");

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            var response = await _httpClient.GetStringAsync(Endpoints.API_URL + Endpoints.ENDPOINT_USER + "/" + email);
            var user = JsonConvert.DeserializeObject<UpdateUserViewDTO>(response);

            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UpdateUserViewDTO model, IFormFile attachment)
        {
            if (model == null)
            {
                return View(model);
            }

            if(attachment != null)
            {
                if (!ValidateImage(attachment))
                {
                    return View(model);
                }

                var file = SaveFile(attachment);

                model.PhotoURL = file;
            }

            var accessToken = HttpContext.Session.GetString("JWToken");

            StringContent content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, Endpoints.HEADER_VALUE);

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            var response = await _httpClient.PutAsync(Endpoints.API_URL + Endpoints.ENDPOINT_USER, content);
            string apiResponse = await response.Content.ReadAsStringAsync();
            var user = JsonConvert.DeserializeObject<UserViewModel>(apiResponse);

            HttpContext.Session.SetString("Email", user.Email);

            return RedirectToAction("MyProfile", "User");
        }

        [HttpGet]
        public async Task<IActionResult> Delete()
        {
            var accessToken = HttpContext.Session.GetString("JWToken");
            var email = HttpContext.Session.GetString("Email");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            var response = await _httpClient.GetStringAsync(Endpoints.API_URL + Endpoints.ENDPOINT_USER + "/" + email);
            var user = JsonConvert.DeserializeObject<UserViewModel>(response);
            HttpContext.Session.SetInt32("userID", user.Id);

            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int user)
        {
            var id = HttpContext.Session.GetInt32("userID");
            var accessToken = HttpContext.Session.GetString("JWToken");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            
            var response = await _httpClient.DeleteAsync(Endpoints.API_URL + Endpoints.ENDPOINT_USER + "/" + id);

            HttpContext.Session.Clear();

            return RedirectToAction("Login", "Auth");
        }

        [HttpGet]
        public async Task<IActionResult> ListUsers()
        {
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Auth");
        }

        public bool ValidateImage(IFormFile attachment)
        {
            switch (attachment.ContentType)
            {
                case "image/jpeg":
                    return true;
                case "image/png":
                    return true;
                dafault:
                    return false;
                    break;
            }
            return false;
        }

        public string SaveFile(IFormFile attachment)
        {
            var fileName = attachment.FileName;

            var filePath = _filePath + "\\images";
            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }

            using(var strem = System.IO.File.Create(filePath + "\\" + fileName))
            {
                attachment.CopyToAsync(strem);
            }

            return fileName;
        }
    }
}
