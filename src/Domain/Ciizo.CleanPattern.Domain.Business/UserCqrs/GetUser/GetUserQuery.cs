using Ciizo.CleanPattern.Domain.Business.UserCqrs.Models;
using Ciizo.CleanPattern.Domain.Core.Models;
using MediatR;

namespace Ciizo.CleanPattern.Domain.Business.UserCqrs.GetUser
{
    public class GetUserQuery : IRequest<Result<UserCqrsDto>>
    {
        public Guid Id { get; init; }
    }
}
