using Cultivo.Domain.Constants;
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
        private readonly IUserService _userService;

        public AuthController(IAuthService authService, IUserService userService)
        {
            _authService = authService;
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] Login login)
        {
            if (!ModelState.IsValid || (login == null) ||
               (login.Email == null || login.Email == "") ||
               (login.Password == null || login.Email == ""))
                return BadRequest(Exceptions.MESSAGE_INCOMPLETE_INFORMATION);

            var userExists = await _userService.GetOneByCriteria(model => (model.Email == login.Email) && (model.Password == login.Password));
            
            if (userExists == null)
                return BadRequest(Exceptions.MESSAGE_USER_NOT_EXIST);

            var token = _authService.GenerateToken(login);

            return Ok(new
            {
                Token = token,
                User = login
            });
        }

    }
}
