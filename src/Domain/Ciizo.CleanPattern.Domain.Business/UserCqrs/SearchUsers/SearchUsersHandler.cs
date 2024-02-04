using Ciizo.CleanPattern.Domain.Business.Common.Constants;
using Ciizo.CleanPattern.Domain.Business.Common.Models;
using Ciizo.CleanPattern.Domain.Business.UserCqrs.Models;
using Ciizo.CleanPattern.Domain.Business.UserCqrs.Validators;
using Ciizo.CleanPattern.Domain.Core.Models;
using Ciizo.CleanPattern.Domain.Core.Repository;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Ciizo.CleanPattern.Domain.Business.UserCqrs.SearchUsers
{
    internal class SearchUsersHandler : IRequestHandler<SearchUsersQuery, Result<SearchResult<UserCqrsDto>>>
    {
        private readonly IRepository<Core.Entities.User, IApplicationDbContext> _repository;

        public SearchUsersHandler(IRepository<Core.Entities.User, IApplicationDbContext> repository)
        {
            _repository = repository;
        }

        public async Task<Result<SearchResult<UserCqrsDto>>> Handle(SearchUsersQuery request, CancellationToken cancellationToken)
        {
            UserSearchCriteriaValidator validator = new();
            var validation = await validator.ValidateAsync(request.Criteria, cancellationToken);
            if (!validation.IsValid)
                return new ValidationException(validation.Errors);

            var query = _repository.GetQueryable();
            query = query.Where(x => x.Name.Contains(request.Criteria.Name))
                        .OrderBy(x => x.Id);

            var totalCount = await query.CountAsync(cancellationToken);

            var totalPages = (int)Math.Ceiling((double)totalCount / request.PageSize);
            var page = Math.Max(PaginationRules.FirstPage, Math.Min(request.Page, totalPages));
            var pageSize = Math.Min(PaginationRules.MaxPageSize, request.PageSize);

            var matchingUsers = await query
                .Skip((page - PaginationRules.FirstPage) * pageSize)
                .Take(pageSize)
                .Select(user => UserCqrsDto.FromEntity(user))
                .ToListAsync(cancellationToken);

            var result = new SearchResult<UserCqrsDto>
            {
                Data = matchingUsers,
                TotalCount = totalCount,
            };

            return result;
        }
    }
}
