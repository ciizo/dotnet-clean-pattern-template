using Ciizo.Restful.Onion.Domain.Core.Entities;

namespace Banking.Domain.Service.Models
{
    public record UserCreateDto(
        string Email,
        string Name
        )
    {
        public static User ToEntity(UserCreateDto dto)
        {
            return new User()
            {
                Email = dto.Email,
                Name = dto.Name,
            };
        }
    }
}