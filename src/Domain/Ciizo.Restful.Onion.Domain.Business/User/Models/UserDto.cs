using Ciizo.Restful.Onion.Domain.Core.Entities;

namespace Banking.Domain.Service.Models
{
    public record UserDto(
        Guid Id,
        string Email,
        string Name
        )
    {
        public static UserDto FromEntity(User entity)
        {
            return new UserDto(entity.Id, entity.Email, entity.Name);
        }

        public static User ToEntity(UserDto dto)
        {
            return new User()
            {
                Id = dto.Id,
                Email = dto.Email,
                Name = dto.Name,
            };
        }
    }
}