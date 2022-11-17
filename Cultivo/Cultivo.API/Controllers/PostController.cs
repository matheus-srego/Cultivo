using Cultivo.Domain.Interfaces.Services;
using Cultivo.Domain.Models;
using Cultivo.Service.Validations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace Cultivo.API.Controllers
{
    [Route("api/post")]
    [ApiController]
    [Authorize]
    [Produces(MediaTypeNames.Application.Json)]
    public class PostController : ControllerBase
    {
        private readonly IPostService _postService;

        public PostController(IPostService postService)
        {
            _postService = postService;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Post post)
        {
            return Ok(await _postService.CreateAsync<PostValidator>(post));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            return Ok(await _postService.GetByIdAsync(id));
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            return Ok(await _postService.ListUsers());
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            return Ok(await _postService.DeleteAsync(id));
        }
    }
}
