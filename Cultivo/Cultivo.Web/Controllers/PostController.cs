using Cultivo.Domain.Constants;
using Cultivo.Domain.Interfaces.Services;
using Cultivo.Domain.Models;
using Cultivo.Web.DTOs;
using Cultivo.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace Cultivo.Web.Controllers
{
    [Authorize]
    public class PostController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string _filePath;

        public PostController(IWebHostEnvironment environment)
        {
            _filePath = environment.WebRootPath;
            _httpClient = new HttpClient();
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(NewPostViewDTO model, IFormFile attachment)
        {
            UserViewModel user = new UserViewModel();
            if(model.UserId == null || model.UserId == 0)
            {
                var accessToken = HttpContext.Session.GetString("JWToken");
                var email = HttpContext.Session.GetString("Email");

                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                var responseUser = await _httpClient.GetStringAsync(Endpoints.API_URL + Endpoints.ENDPOINT_USER + "/" + email);
                user = JsonConvert.DeserializeObject<UserViewModel>(responseUser);
            }

            model.UserId = user.Id;

            if(attachment != null)
            {
                if (!ValidateImage(attachment))
                {
                    return View(model);
                }


                var file = SaveFile(attachment);

                model.image = file;
            }

            if (model == null)
            {
                return View(model);
            }

            StringContent content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, Endpoints.HEADER_VALUE);

            var response = await _httpClient.PostAsync(Endpoints.API_URL + Endpoints.ENDPOINT_POST, content);

            if (response.IsSuccessStatusCode)
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                JsonConvert.DeserializeObject<NewPostViewDTO>(apiResponse);
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var accessToken = HttpContext.Session.GetString("JWToken");

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            var response = await _httpClient.GetStringAsync(Endpoints.API_URL + Endpoints.ENDPOINT_POST + "/" + id);
            var post = JsonConvert.DeserializeObject<UpdatePostViewModel>(response);

            return View(post);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UpdatePostViewModel model, IFormFile attachment)
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

                model.image = file;
            }

            var accessToken = HttpContext.Session.GetString("JWToken");

            StringContent content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, Endpoints.HEADER_VALUE);

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            var response = await _httpClient.PutAsync(Endpoints.API_URL + Endpoints.ENDPOINT_POST, content);
            string apiResponse = await response.Content.ReadAsStringAsync();
            JsonConvert.DeserializeObject<PostViewModel>(apiResponse);

            return RedirectToAction("MyProfile", "User");
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            List<PostViewModel> posts = new List<PostViewModel>();
            var accessToken = HttpContext.Session.GetString("JWToken");
            
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var response = await _httpClient.GetAsync(Endpoints.API_URL + Endpoints.ENDPOINT_POST);
            string apiResponse = await response.Content.ReadAsStringAsync();
            posts = JsonConvert.DeserializeObject<List<PostViewModel>>(apiResponse);

            return View(posts);
        }


        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var accessToken = HttpContext.Session.GetString("JWToken");
            
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var response = await _httpClient.GetStringAsync(Endpoints.API_URL + Endpoints.ENDPOINT_POST + "/" + id);
            var post = JsonConvert.DeserializeObject<PostViewModel>(response);

            return View(post);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(PostViewModel model)
        {
            var accessToken = HttpContext.Session.GetString("JWToken");

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var response = await _httpClient.DeleteAsync(Endpoints.API_URL + Endpoints.ENDPOINT_POST + "/" + model.Id);

            return RedirectToAction("MyProfile", "User");
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

            using (var strem = System.IO.File.Create(filePath + "\\" + fileName))
            {
                attachment.CopyToAsync(strem);
            }

            return fileName;
        }
    }
}
