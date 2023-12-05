using Banking.Domain.Service.Models;
using Banking.Domain.Service.Validators;
using Ciizo.Restful.Onion.Domain.Core.Repository;
using FluentValidation;

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
    }
}