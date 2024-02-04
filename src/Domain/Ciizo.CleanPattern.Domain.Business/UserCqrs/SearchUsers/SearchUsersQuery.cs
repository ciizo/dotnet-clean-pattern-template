using Ciizo.CleanPattern.Domain.Business.Common.Models;
using Ciizo.CleanPattern.Domain.Business.UserCqrs.Models;
using Ciizo.CleanPattern.Domain.Core.Models;
using MediatR;

namespace Ciizo.CleanPattern.Domain.Business.UserCqrs.SearchUsers
{
    public class SearchUsersQuery : IRequest<Result<SearchResult<UserCqrsDto>>>
    {
        public required UserCqrsSearchCriteria Criteria { get; init; }
        public int Page { get; init; }
        public int PageSize { get; init; }
    }
}
