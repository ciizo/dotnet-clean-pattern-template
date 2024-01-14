using Ciizo.CleanPattern.Api.Auth;
using Ciizo.CleanPattern.Domain.Business.Common.Constants;
using Ciizo.CleanPattern.Domain.Business.User;
using Ciizo.CleanPattern.Domain.Business.User.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Ciizo.CleanPattern.Api.Controllers
{
    [Authorize]
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
        public async Task<IActionResult> CreateUserAsync([Required] UserDto dto, CancellationToken cancellationToken)
        {
            var result = await _userService.CreateUserAsync(dto, cancellationToken);

            return Created(new Uri(Path.Combine(Request.GetEncodedUrl(), result.Id.ToString())), result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserAsync([Required] Guid id, CancellationToken cancellationToken)
        {
            var result = await _userService.GetUserAsync(id, cancellationToken);

            return Ok(result);
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchUsersAsync(
            [FromQuery] UserSearchCriteria criteria,
            [Required] int page = PaginationRules.FirstPage,
            [Required] int pageSize = PaginationRules.MinPageSize,
            CancellationToken cancellationToken = default)
        {
            if (criteria == null)
            {
                return BadRequest("Search criteria cannot be null.");
            }

            var result = await _userService.SearchUsersAsync(criteria, page, pageSize, cancellationToken);

            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUserAsync([Required] Guid id, [Required] UserDto dto, CancellationToken cancellationToken)
        {
            await _userService.UpdateUserAsync(id, dto, cancellationToken);

            return NoContent();
        }

        [RequireClaim(ClaimTypes.UserType, nameof(UserTypes.Admin))]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserAsync([Required] Guid id, CancellationToken cancellationToken)
        {
            await _userService.DeleteUserAsync(id, cancellationToken);

            return NoContent();
        }
    }
}