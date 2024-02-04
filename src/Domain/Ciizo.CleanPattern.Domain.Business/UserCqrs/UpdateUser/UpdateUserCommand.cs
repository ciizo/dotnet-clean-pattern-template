using Ciizo.CleanPattern.Domain.Business.UserCqrs.Models;
using Ciizo.CleanPattern.Domain.Core.Models;
using MediatR;

namespace Ciizo.CleanPattern.Domain.Business.UserCqrs.UpdateUser
{
    public class UpdateUserCommand : IRequest<EmptyResult>
    {
        public Guid Id { get; init; }
        public required UserCqrsDto User { get; init; }
    }
}
