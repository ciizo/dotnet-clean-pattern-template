using Ciizo.CleanPattern.Domain.Core.Models;
using Ciizo.CleanPattern.Domain.Core.Repository;
using MediatR;

namespace Ciizo.CleanPattern.Domain.Business.UserCqrs.DeleteUser
{
    internal class DeleteUserHandler : IRequestHandler<DeleteUserCommand, EmptyResult>
    {
        private readonly IRepository<Core.Entities.User, IApplicationDbContext> _repository;

        public DeleteUserHandler(IRepository<Core.Entities.User, IApplicationDbContext> repository)
        {
            _repository = repository;
        }

        public async Task<EmptyResult> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            if (request.Id == default)
            {
                return new Error("Id is required.");
            }

            _repository.DeleteById(request.Id);
            await _repository.SaveChangesAsync(cancellationToken);

            return new();
        }
    }
}
