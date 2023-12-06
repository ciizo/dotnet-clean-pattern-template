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

        public async Task<UserDto> CreateUserAsync(UserDto dto, CancellationToken cancellationToken)
        {
            UserDtoValidator validator = new();
            await validator.ValidateAndThrowAsync(dto, cancellationToken);

            var entity = UserDto.ToEntity(dto);
            entity.Created = DateTime.UtcNow;
            entity.CreatedBy = "Test";

            _repository.Insert(entity);
            await _repository.SaveChangesAsync(cancellationToken);

            return UserDto.FromEntity(entity);
        }

        public async Task DeleteUserAsync(Guid id, CancellationToken cancellationToken)
        {
            if (id == default)
            {
                throw new ArgumentException("Id is required.");
            }

            _repository.DeleteById(id);
            await _repository.SaveChangesAsync(cancellationToken);
        }

        public async Task<UserDto> GetUserAsync(Guid id, CancellationToken cancellationToken)
        {
            if (id == default)
            {
                throw new ArgumentException("Id is required.");
            }

            var entity = await _repository.GetByIdAsync(id, cancellationToken)
                ?? throw new DataNotFoundException(nameof(Core.Entities.User));

            return UserDto.FromEntity(entity);
        }

        public async Task<SearchResult<UserDto>> SearchUsersAsync(UserSearchCriteria criteria, int page, int pageSize, CancellationToken cancellationToken)
        {
            UserSearchCriteriaValidator validator = new();
            await validator.ValidateAndThrowAsync(criteria, cancellationToken);

            var query = _repository.GetQueryable();
            query = query.Where(x => x.Name.Contains(criteria.Name))
                        .OrderBy(x => x.Id);

            var totalCount = await query.CountAsync(cancellationToken);

            var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);
            page = Math.Max(PaginationRules.FirstPage, Math.Min(page, totalPages));
            pageSize = Math.Min(PaginationRules.MaxPageSize, pageSize);

            var matchingUsers = await query
                .Skip((page - PaginationRules.FirstPage) * pageSize)
                .Take(pageSize)
                .Select(user => UserDto.FromEntity(user))
                .ToListAsync(cancellationToken);

            var result = new SearchResult<UserDto>
            {
                Data = matchingUsers,
                TotalCount = totalCount,
            };

            return result;
        }

        public async Task UpdateUserAsync(Guid id, UserDto dto, CancellationToken cancellationToken)
        {
            if (id == default)
            {
                throw new ArgumentException("Id is required.");
            }

            UserDtoValidator validator = new();
            await validator.ValidateAndThrowAsync(dto, cancellationToken);

            var entity = await _repository.GetByIdAsync(id, cancellationToken)
                ?? throw new DataNotFoundException(nameof(Core.Entities.User));

            entity.Email = dto.Email;
            entity.Name = dto.Name;
            entity.Updated = DateTime.UtcNow;
            entity.UpdatedBy = "Test";

            _repository.Update(entity);
            await _repository.SaveChangesAsync(cancellationToken);
        }
    }
}