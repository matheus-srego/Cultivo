using Cultivo.Domain.Constants;
using Cultivo.Web.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;
using System.Text;

namespace Cultivo.Web.Controllers
{
    public class AuthController : Controller
    {
        private readonly HttpClient _httpClient;

        public AuthController()
        {
            _httpClient = new HttpClient();
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if(!ModelState.IsValid)
            {
                return View(model);
            }
            
            StringContent content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, Endpoints.HEADER_VALUE);

            var response = await _httpClient.PostAsync(Endpoints.API_URL + Endpoints.ENDPOINT_AUTH, content);
            var responseContent = await response.Content.ReadAsStringAsync();
            var data = (JObject)JsonConvert.DeserializeObject(responseContent);

            if (!response.IsSuccessStatusCode)
            {
                ViewBag.MessageError = data["message"].ToString();
                return View(model);
            }

            var token = data["token"].ToString();
            
            HttpContext.Session.SetString("JWToken", token);

            return RedirectToAction("Index", "Home", new { area = "" });
        }
    }
}
