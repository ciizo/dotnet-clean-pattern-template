using Ciizo.CleanPattern.Domain.Business.UserCqrs.Models;
using Ciizo.CleanPattern.Domain.Business.UserCqrs.Validators;
using Ciizo.CleanPattern.Domain.Core.Models;
using Ciizo.CleanPattern.Domain.Core.Repository;
using FluentValidation;
using MediatR;

namespace Ciizo.CleanPattern.Domain.Business.UserCqrs.CreateUser
{
    internal class CreateUserHandler : IRequestHandler<CreateUserCommand, Result<UserCqrsDto>>
    {
        private readonly IRepository<Core.Entities.User, IApplicationDbContext> _repository;

        public CreateUserHandler(IRepository<Core.Entities.User, IApplicationDbContext> repository)
        {
            _repository = repository;
        }

        public async Task<Result<UserCqrsDto>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            UserDtoValidator validator = new();
            var validation = await validator.ValidateAsync(request.User, cancellationToken);
            if (!validation.IsValid)
                return new ValidationException(validation.Errors);

            var entity = UserCqrsDto.ToEntity(request.User);
            entity.Created = DateTime.UtcNow;
            entity.CreatedBy = "Test";

            _repository.Insert(entity);
            await _repository.SaveChangesAsync(cancellationToken);

            return UserCqrsDto.FromEntity(entity);
        }
    }
}
