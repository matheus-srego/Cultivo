using Cultivo.Domain.DTOs;
using Cultivo.Domain.Factories;
using Cultivo.Domain.Interfaces.Services;
using Cultivo.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cultivo.API.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        [ProducesResponseType(typeof(User), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] NewUserDTO newUser)
        {
            return Ok(await _userService.CreateAsync(UserFactory.Create(newUser)));
        }
    }
}
