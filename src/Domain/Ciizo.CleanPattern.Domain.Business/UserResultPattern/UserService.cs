using Ciizo.CleanPattern.Domain.Business.Common.Constants;
using Ciizo.CleanPattern.Domain.Business.Common.Models;
using Ciizo.CleanPattern.Domain.Business.Exceptions;
using Ciizo.CleanPattern.Domain.Business.UserResultPattern.Models;
using Ciizo.CleanPattern.Domain.Business.UserResultPattern.Validators;
using Ciizo.CleanPattern.Domain.Core.Models;
using Ciizo.CleanPattern.Domain.Core.Models.User;
using Ciizo.CleanPattern.Domain.Core.Repository;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Ciizo.CleanPattern.Domain.Business.UserResultPattern
{
    public class UserService : IUserService
    {
        private readonly IRepository<Core.Entities.User, IApplicationDbContext> _repository;

        public UserService(IRepository<Core.Entities.User, IApplicationDbContext> repository)
        {
            _repository = repository;
        }

        public async Task<Result<UserRpDto>> CreateUserAsync(UserRpDto dto, CancellationToken cancellationToken)
        {
            UserDtoValidator validator = new();
            var validation = await validator.ValidateAsync(dto, cancellationToken);
            if (!validation.IsValid)
                return new ValidationException(validation.Errors);

            var entity = UserRpDto.ToEntity(dto);
            entity.Created = DateTime.UtcNow;
            entity.CreatedBy = "Test";

            _repository.Insert(entity);
            await _repository.SaveChangesAsync(cancellationToken);

            return UserRpDto.FromEntity(entity);
        }

        public async Task<EmptyResult> DeleteUserAsync(Guid id, CancellationToken cancellationToken)
        {
            if (id == default)
            {
                return new Error("Id is required.");
            }

            _repository.DeleteById(id);
            await _repository.SaveChangesAsync(cancellationToken);

            return new();
        }

        public async Task<Result<UserRpDto>> GetUserAsync(Guid id, CancellationToken cancellationToken)
        {
            if (id == default)
            {
                return new Error("Id is required.");
            }

            var entity = await _repository.GetByIdAsync(id, cancellationToken);
            if (entity is null)
                return new DataNotFoundException(nameof(Core.Entities.User));

            return UserRpDto.FromEntity(entity);
        }

        public async Task<Result<SearchResult<UserRpDto>>> SearchUsersAsync(UserRpSearchCriteria criteria, int page, int pageSize, CancellationToken cancellationToken)
        {
            UserSearchCriteriaValidator validator = new();
            var validation = await validator.ValidateAsync(criteria, cancellationToken);
            if (!validation.IsValid)
                return new ValidationException(validation.Errors);

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
                .Select(user => UserRpDto.FromEntity(user))
                .ToListAsync(cancellationToken);

            var result = new SearchResult<UserRpDto>
            {
                Data = matchingUsers,
                TotalCount = totalCount,
            };

            return result;
        }

        public async Task<EmptyResult> UpdateUserAsync(Guid id, UserRpDto dto, CancellationToken cancellationToken)
        {
            if (id == default)
            {
                return new Error("Id is required.");
            }

            UserDtoValidator validator = new();
            await validator.ValidateAndThrowAsync(dto, cancellationToken);

            var entity = await _repository.GetByIdAsync(id, cancellationToken);
            if (entity is null)
                return UserErrors.UserNotFound;
            //return new DataNotFoundException(nameof(Core.Entities.User));

            entity.Email = dto.Email;
            entity.Name = dto.Name;
            entity.Updated = DateTime.UtcNow;
            entity.UpdatedBy = "Test";

            _repository.Update(entity);
            await _repository.SaveChangesAsync(cancellationToken);

            return new();
        }
    }
}