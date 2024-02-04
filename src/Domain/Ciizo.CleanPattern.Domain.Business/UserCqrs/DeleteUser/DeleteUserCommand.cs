using Ciizo.CleanPattern.Domain.Core.Models;
using MediatR;

namespace Ciizo.CleanPattern.Domain.Business.UserCqrs.DeleteUser
{
    public class DeleteUserCommand : IRequest<EmptyResult>
    {
        public Guid Id { get; init; }
    }
}
