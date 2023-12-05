using Ciizo.Restful.Onion.Domain.Business.Common.Constants;
using Ciizo.Restful.Onion.Domain.Business.Common.Models;
using Ciizo.Restful.Onion.Domain.Business.Exceptions;
using Ciizo.Restful.Onion.Domain.Business.User.Models;
using Ciizo.Restful.Onion.Domain.Business.User.Validators;
using Ciizo.Restful.Onion.Domain.Core.Repository;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Ciizo.Restful.Onion.Domain.Business.User
{
    public class UserService : IUserService
    {
        private readonly IRepository<Core.Entities.User, IApplicationDbContext> _repository;

        public UserService(IRepository<Core.Entities.User, IApplicationDbContext> repository)
        {
            _repository = repository;
        }

        public async Task<UserDto> CreateUserAsync(UserCreateDto dto, CancellationToken cancellationToken)
        {
            UserCreateDtoValidator validator = new();
            await validator.ValidateAndThrowAsync(dto, cancellationToken);

            var entity = UserCreateDto.ToEntity(dto);
            entity.Created = DateTime.UtcNow;
            entity.CreatedBy = "Test";

            _repository.Insert(entity);
            await _repository.SaveChangesAsync(cancellationToken);

            return UserDto.FromEntity(entity);
        }

        public async Task<UserDto> GetUserAsync(Guid id, CancellationToken cancellationToken)
        {
            if (id == default)
            {
                throw new ArgumentException("Id is required.");
            }

            var entity = await _repository.GetByIdAsync(id, cancellationToken);

            return entity is not null ?
                UserDto.FromEntity(entity)
                : throw new DataNotFoundException(nameof(Core.Entities.User));
        }

        public async Task<UserSearchResult<UserDto>> SearchUsersAsync(UserSearchCriteria criteria, int page, int pageSize, CancellationToken cancellationToken)
        {
            UserSearchCriteriaValidator validator = new();
            await validator.ValidateAndThrowAsync(criteria, cancellationToken);

            var query = _repository.GetQueryable();
            query = query.Where(x => x.Name.Contains(criteria.Name));

            var totalCount = await query.CountAsync(cancellationToken);

            var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);
            page = Math.Max(PaginationRules.FirstPage, Math.Min(page, totalPages));
            pageSize = Math.Min(PaginationRules.MaxPageSize, pageSize);

            var matchingUsers = await query
                .Skip((page - PaginationRules.FirstPage) * pageSize)
                .Take(pageSize)
                .Select(user => UserDto.FromEntity(user))
                .ToListAsync(cancellationToken);

            var result = new UserSearchResult<UserDto>
            {
                Data = matchingUsers,
                TotalCount = totalCount,
            };

            return result;
        }
    }
}