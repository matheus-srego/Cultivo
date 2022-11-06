using Cultivo.Domain.DTOs;
using Cultivo.Domain.Factories;
using Cultivo.Domain.Interfaces.Services;
using Cultivo.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cultivo.API.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] Login login)
        {
            var token = _authService.GenerateToken(login);

            return Ok(new
            {
                Token = token,
                User = login
            });
        }

    }
}
