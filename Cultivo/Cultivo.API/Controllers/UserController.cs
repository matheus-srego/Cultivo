using Cultivo.Domain.DTOs;
using Cultivo.Domain.Factories;
using Cultivo.Domain.Interfaces.Services;
using Cultivo.Domain.Models;
using Cultivo.Service.Validations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace Cultivo.API.Controllers
{
    [Route("api/users")]
    [ApiController]
    [Authorize]
    [Produces(MediaTypeNames.Application.Json)]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [ProducesResponseType(typeof(User), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] NewUserDTO newUser)
        {
            if(newUser == null)
                return NotFound();

            return  Ok(await _userService.CreateAsync<UserValidator>(UserFactory.Create(newUser)));
        }

        [ProducesResponseType(typeof(User), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateUserDTO updateUser)
        {
            var user = await _userService.GetByIdAsync(updateUser.Id);
            var userUpdated = UserFactory.Update(updateUser, user);
            
            return Ok(await _userService.UpdateAsync<UserValidator>(userUpdated));
        }

        [ProducesResponseType(typeof(User), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            return Ok(await _userService.DeleteAsync(id));
        }
    }
}
