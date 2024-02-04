using Ciizo.CleanPattern.Domain.Business.Common.Models;
using Ciizo.CleanPattern.Domain.Business.UserResultPattern.Models;
using Ciizo.CleanPattern.Domain.Core.Models;

namespace Ciizo.CleanPattern.Domain.Business.UserResultPattern
{
    public interface IUserService
    {
        Task<Result<UserRpDto>> CreateUserAsync(UserRpDto dto, CancellationToken cancellationToken);

        Task<Result<UserRpDto>> GetUserAsync(Guid id, CancellationToken cancellationToken);

        Task<Result<SearchResult<UserRpDto>>> SearchUsersAsync(UserRpSearchCriteria criteria, int page, int pageSize, CancellationToken cancellationToken);

        Task<EmptyResult> UpdateUserAsync(Guid id, UserRpDto dto, CancellationToken cancellationToken);

        Task<EmptyResult> DeleteUserAsync(Guid id, CancellationToken cancellationToken);
    }
}