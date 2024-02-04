using Ciizo.CleanPattern.Domain.Business.Common.Constants;
using Ciizo.CleanPattern.Domain.Business.UserResultPattern;
using Ciizo.CleanPattern.Domain.Business.UserResultPattern.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Ciizo.CleanPattern.Api.Controllers
{
    //[Authorize]
    [Route("api/users-result-pattern")]
    [ApiController]
    public class UserResultPatternController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserResultPatternController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("")]
        public async Task<IActionResult> CreateUserAsync([Required] UserRpDto dto, CancellationToken cancellationToken)
        {
            var result = await _userService.CreateUserAsync(dto, cancellationToken);

            return result.Match<IActionResult>(
                m => CreatedAtAction(nameof(GetUserAsync), new { id = m.Id }, m),
                error => BadRequest(error));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserAsync([Required] Guid id, CancellationToken cancellationToken)
        {
            var result = await _userService.GetUserAsync(id, cancellationToken);

            return result.Match<IActionResult>(
                m => Ok(m),
                error => BadRequest(error));
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchUsersAsync(
            [FromQuery] UserRpSearchCriteria criteria,
            [Required] int page = PaginationRules.FirstPage,
            [Required] int pageSize = PaginationRules.MinPageSize,
            CancellationToken cancellationToken = default)
        {
            if (criteria == null)
            {
                return BadRequest("Search criteria cannot be null.");
            }

            var result = await _userService.SearchUsersAsync(criteria, page, pageSize, cancellationToken);

            return result.Match<IActionResult>(Ok, BadRequest);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUserAsync([Required] Guid id, [Required] UserRpDto dto, CancellationToken cancellationToken)
        {
            var result = await _userService.UpdateUserAsync(id, dto, cancellationToken);

            return result.Match<IActionResult>(NoContent, BadRequest);
        }

        //[RequireClaim(ClaimTypes.UserType, nameof(UserTypes.Admin))]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserAsync([Required] Guid id, CancellationToken cancellationToken)
        {
            var result = await _userService.DeleteUserAsync(id, cancellationToken);

            return result.Match<IActionResult>(NoContent, BadRequest);
        }
    }
}