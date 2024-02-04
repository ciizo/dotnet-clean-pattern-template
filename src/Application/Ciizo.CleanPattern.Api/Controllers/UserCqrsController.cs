using Asp.Versioning;
using Ciizo.CleanPattern.Domain.Business.Common.Constants;
using Ciizo.CleanPattern.Domain.Business.UserCqrs.CreateUser;
using Ciizo.CleanPattern.Domain.Business.UserCqrs.DeleteUser;
using Ciizo.CleanPattern.Domain.Business.UserCqrs.GetUser;
using Ciizo.CleanPattern.Domain.Business.UserCqrs.Models;
using Ciizo.CleanPattern.Domain.Business.UserCqrs.SearchUsers;
using Ciizo.CleanPattern.Domain.Business.UserCqrs.UpdateUser;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Ciizo.CleanPattern.Api.Controllers
{
    [ApiVersion(1)]
    //[Authorize]
    [Route("api/v{v:apiVersion}/users-cqrs")]
    [ApiController]
    public class UserCqrsController : ControllerBase
    {
        private readonly ISender _sender;

        public UserCqrsController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost("")]
        public async Task<IActionResult> CreateUserAsync([Required] UserCqrsDto dto, CancellationToken cancellationToken)
        {
            var createUserCmd = new CreateUserCommand { User = dto };
            var result = await _sender.Send(createUserCmd, cancellationToken);

            return result.Match<IActionResult>(
                m => CreatedAtAction(nameof(GetUserAsync), new { id = m.Id }, m),
                error => BadRequest(error));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserAsync([Required] Guid id, CancellationToken cancellationToken)
        {
            var getUserQry = new GetUserQuery { Id = id };
            var result = await _sender.Send(getUserQry, cancellationToken);

            return result.Match<IActionResult>(
                m => Ok(m),
                error => BadRequest(error));
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchUsersAsync(
            [FromQuery] UserCqrsSearchCriteria criteria,
            [Required] int page = PaginationRules.FirstPage,
            [Required] int pageSize = PaginationRules.MinPageSize,
            CancellationToken cancellationToken = default)
        {
            if (criteria == null)
            {
                return BadRequest("Search criteria cannot be null.");
            }

            var searchUsersQry = new SearchUsersQuery { Criteria = criteria, Page = page, PageSize = pageSize };
            var result = await _sender.Send(searchUsersQry, cancellationToken);

            return result.Match<IActionResult>(Ok, BadRequest);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUserAsync([Required] Guid id, [Required] UserCqrsDto dto, CancellationToken cancellationToken)
        {
            var updateUserCmd = new UpdateUserCommand { Id = id, User = dto };
            var result = await _sender.Send(updateUserCmd, cancellationToken);

            return result.Match<IActionResult>(NoContent, BadRequest);
        }

        //[RequireClaim(ClaimTypes.UserType, nameof(UserTypes.Admin))]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserAsync([Required] Guid id, CancellationToken cancellationToken)
        {
            var deleteUserCmd = new DeleteUserCommand { Id = id };
            var result = await _sender.Send(deleteUserCmd, cancellationToken);

            return result.Match<IActionResult>(NoContent, BadRequest);
        }
    }
}