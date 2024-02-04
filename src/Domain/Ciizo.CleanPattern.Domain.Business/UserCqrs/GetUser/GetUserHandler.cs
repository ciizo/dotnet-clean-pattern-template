using Ciizo.CleanPattern.Domain.Business.Exceptions;
using Ciizo.CleanPattern.Domain.Business.UserCqrs.Models;
using Ciizo.CleanPattern.Domain.Core.Models;
using Ciizo.CleanPattern.Domain.Core.Repository;
using MediatR;

namespace Ciizo.CleanPattern.Domain.Business.UserCqrs.GetUser
{
    internal class GetUserHandler : IRequestHandler<GetUserQuery, Result<UserCqrsDto>>
    {
        private readonly IRepository<Core.Entities.User, IApplicationDbContext> _repository;

        public GetUserHandler(IRepository<Core.Entities.User, IApplicationDbContext> repository)
        {
            _repository = repository;
        }

        public async Task<Result<UserCqrsDto>> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            if (request.Id == default)
            {
                return new Error("Id is required.");
            }

            var entity = await _repository.GetByIdAsync(request.Id, cancellationToken);
            if (entity is null)
                return new DataNotFoundException(nameof(Core.Entities.User));

            return UserCqrsDto.FromEntity(entity);
        }
    }
}
