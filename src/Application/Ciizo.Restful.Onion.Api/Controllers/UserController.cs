using Banking.Domain.Service.Models;
using Ciizo.Restful.Onion.Domain.Business.User;
using Microsoft.AspNetCore.Mvc;

namespace Ciizo.Restful.Onion.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("")]
        public async Task<IActionResult> CreateUserAsync(UserCreateDto dto, CancellationToken cancellationToken)
        {
            var result = await _userService.CreateUserAsync(dto, cancellationToken);

            return Ok(result);
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> GetUserAsync(Guid id, CancellationToken cancellationToken)
        {
            var result = await _userService.CreateUserAsync(dto, cancellationToken);

            return Ok(result);
        }

        [HttpPost("")]
        public async Task<IActionResult> CreateUserAsync(UserCreateDto dto, CancellationToken cancellationToken)
        {
            var result = await _userService.CreateUserAsync(dto, cancellationToken);

            return Ok(result);
        }
    }
}