using Ciizo.CleanPattern.Domain.Business.Common.Models;
using Ciizo.CleanPattern.Domain.Business.User.Models;

namespace Ciizo.CleanPattern.Domain.Business.User
{
    public interface IUserService
    {
        Task<UserDto> CreateUserAsync(UserDto dto, CancellationToken cancellationToken);

        Task<UserDto> GetUserAsync(Guid id, CancellationToken cancellationToken);

        Task<SearchResult<UserDto>> SearchUsersAsync(UserSearchCriteria criteria, int page, int pageSize, CancellationToken cancellationToken);

        Task UpdateUserAsync(Guid id, UserDto dto, CancellationToken cancellationToken);

        Task DeleteUserAsync(Guid id, CancellationToken cancellationToken);
    }
}