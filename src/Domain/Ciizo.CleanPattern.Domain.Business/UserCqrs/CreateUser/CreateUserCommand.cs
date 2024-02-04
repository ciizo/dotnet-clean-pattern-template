using Ciizo.CleanPattern.Domain.Business.UserCqrs.Models;
using Ciizo.CleanPattern.Domain.Core.Models;
using MediatR;

namespace Ciizo.CleanPattern.Domain.Business.UserCqrs.CreateUser
{
    public class CreateUserCommand : IRequest<Result<UserCqrsDto>>
    {
        public required UserCqrsDto User { get; init; }
    }
}
