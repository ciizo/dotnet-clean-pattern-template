using Ciizo.Restful.Onion.Domain.Business.Common.Models;
using Ciizo.Restful.Onion.Domain.Business.User.Models;

namespace Ciizo.Restful.Onion.Domain.Business.User
{
    public interface IUserService
    {
        Task<UserDto> CreateUserAsync(UserCreateDto dto, CancellationToken cancellationToken);

        Task<UserDto> GetUserAsync(Guid id, CancellationToken cancellationToken);

        Task<SearchResult<UserDto>> SearchUsersAsync(UserSearchCriteria criteria, int page, int pageSize, CancellationToken cancellationToken);
    }
}