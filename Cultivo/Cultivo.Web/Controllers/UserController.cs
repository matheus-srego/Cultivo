using Cultivo.Domain.Constants;
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
    }
}
