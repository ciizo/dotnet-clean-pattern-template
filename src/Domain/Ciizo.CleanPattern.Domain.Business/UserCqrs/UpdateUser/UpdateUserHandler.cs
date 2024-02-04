using Ciizo.CleanPattern.Domain.Business.UserCqrs.Validators;
using Ciizo.CleanPattern.Domain.Core.Models;
using Ciizo.CleanPattern.Domain.Core.Models.User;
using Ciizo.CleanPattern.Domain.Core.Repository;
using FluentValidation;
using MediatR;

namespace Ciizo.CleanPattern.Domain.Business.UserCqrs.UpdateUser
{
    internal class UpdateUserHandler : IRequestHandler<UpdateUserCommand, EmptyResult>
    {
        private readonly IRepository<Core.Entities.User, IApplicationDbContext> _repository;

        public UpdateUserHandler(IRepository<Core.Entities.User, IApplicationDbContext> repository)
        {
            _repository = repository;
        }

        public async Task<EmptyResult> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            if (request.Id == default)
            {
                return new Error("Id is required.");
            }

            UserDtoValidator validator = new();
            var validation = await validator.ValidateAsync(request.User, cancellationToken);
            if (!validation.IsValid)
                return new ValidationException(validation.Errors);

            var entity = await _repository.GetByIdAsync(request.Id, cancellationToken);
            if (entity is null)
                return UserErrors.UserNotFound;
            //return new DataNotFoundException(nameof(Core.Entities.User));

            entity.Email = request.User.Email;
            entity.Name = request.User.Name;
            entity.Updated = DateTime.UtcNow;
            entity.UpdatedBy = "Test";

            _repository.Update(entity);
            await _repository.SaveChangesAsync(cancellationToken);

            return new();
        }
    }
}
