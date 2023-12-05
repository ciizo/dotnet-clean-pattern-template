using Ciizo.Restful.Onion.Domain.Business.Common.Constants;
using Ciizo.Restful.Onion.Domain.Business.User;
using Ciizo.Restful.Onion.Domain.Business.User.Models;
using Microsoft.AspNetCore.Mvc;

namespace Ciizo.Restful.Onion.Api.Controllers
{
    [Route("api/users")]
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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserAsync(Guid id, CancellationToken cancellationToken)
        {
            var result = await _userService.GetUserAsync(id, cancellationToken);

            return Ok(result);
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchUsersAsync(
            [FromQuery] UserSearchCriteria criteria,
            int page = PaginationRules.FirstPage,
            int pageSize = PaginationRules.MinPageSize,
            CancellationToken cancellationToken = default)
        {
            if (criteria == null)
            {
                return BadRequest("Search criteria cannot be null.");
            }

            var result = await _userService.SearchUsersAsync(criteria, page, pageSize, cancellationToken);

            return Ok(result);
        }
    }
}