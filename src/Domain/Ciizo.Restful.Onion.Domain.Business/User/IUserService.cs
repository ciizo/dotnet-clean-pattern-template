using Banking.Domain.Service.Models;

namespace Ciizo.Restful.Onion.Domain.Business.User
{
    public interface IUserService
    {
        Task<UserDto> CreateUserAsync(UserCreateDto dto, CancellationToken cancellationToken);

        Task<UserDto> GetUserAsync(Guid id, CancellationToken cancellationToken);
    }
}