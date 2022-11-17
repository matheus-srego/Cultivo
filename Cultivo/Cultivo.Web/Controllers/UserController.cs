using Cultivo.Domain.Constants;
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
        public UserController()
        {
            _httpClient = new HttpClient();
        }

        [AllowAnonymous]
        public IActionResult Create()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Create(NewUserViewDTO dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(dto);
                }

                StringContent content = new StringContent(JsonConvert.SerializeObject(dto), Encoding.UTF8, Endpoints.HEADER_VALUE);

                var response = await _httpClient.PostAsync(Endpoints.API_URL + Endpoints.ENDPOINT_USER, content);

                if (response.IsSuccessStatusCode)
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    JsonConvert.DeserializeObject<UserViewModel>(apiResponse);
                }

                return RedirectToAction("Auth/Login");
            }
            catch
            {
                return View();
            }
        }

        [HttpGet]
        public async Task<IActionResult> Profile(int id)
        {
            string response;
            var accessToken = HttpContext.Session.GetString("JWToken");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            if(id == null)
            {
                var email = HttpContext.Session.GetString("Email");
                response = await _httpClient.GetStringAsync(Endpoints.API_URL + Endpoints.ENDPOINT_USER + "/" + email);
            }
            else
            {
                response = await _httpClient.GetStringAsync(Endpoints.API_URL + Endpoints.ENDPOINT_USER + "/" + id);
            }

            var user = JsonConvert.DeserializeObject<UserViewModel>(response);
            
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
        public async Task<IActionResult> Edit(UpdateUserViewDTO model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var accessToken = HttpContext.Session.GetString("JWToken");

            StringContent content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, Endpoints.HEADER_VALUE);

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            var response = await _httpClient.PutAsync(Endpoints.API_URL + Endpoints.ENDPOINT_USER, content);
            string apiResponse = await response.Content.ReadAsStringAsync();
            JsonConvert.DeserializeObject<UserViewModel>(apiResponse);

            return RedirectToAction("Profile", "User");
        }

        [HttpGet]
        public async Task<IActionResult> Delete()
        {
            var accessToken = HttpContext.Session.GetString("JWToken");
            var email = HttpContext.Session.GetString("Email");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            var response = await _httpClient.GetStringAsync(Endpoints.API_URL + Endpoints.ENDPOINT_USER + "/" + email);
            var user = JsonConvert.DeserializeObject<UserViewModel>(response);

            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(UserViewModel model)
        {
            var accessToken = HttpContext.Session.GetString("JWToken");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            
            var response = await _httpClient.DeleteAsync(Endpoints.API_URL + Endpoints.ENDPOINT_USER + "/" + model.Id);

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
    }
}
