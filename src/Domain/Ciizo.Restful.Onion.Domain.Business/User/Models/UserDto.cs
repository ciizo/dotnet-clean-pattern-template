namespace Ciizo.Restful.Onion.Domain.Business.User.Models
{
    public record UserDto(
        Guid Id,
        string Email,
        string Name
        )
    {
        public static UserDto FromEntity(Core.Entities.User entity)
        {
            return new UserDto(entity.Id, entity.Email, entity.Name);
        }

        public static Core.Entities.User ToEntity(UserDto dto)
        {
            return new Core.Entities.User()
            {
                Id = dto.Id,
                Email = dto.Email,
                Name = dto.Name,
            };
        }
    }
}