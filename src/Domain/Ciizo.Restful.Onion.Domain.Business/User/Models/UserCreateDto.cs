namespace Ciizo.Restful.Onion.Domain.Business.User.Models
{
    public record UserCreateDto(
        string Email,
        string Name
        )
    {
        public static Core.Entities.User ToEntity(UserCreateDto dto)
        {
            return new Core.Entities.User()
            {
                Email = dto.Email,
                Name = dto.Name,
            };
        }
    }
}