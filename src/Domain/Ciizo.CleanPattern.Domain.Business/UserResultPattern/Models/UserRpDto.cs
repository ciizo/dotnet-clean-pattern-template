namespace Ciizo.CleanPattern.Domain.Business.UserResultPattern.Models
{
    public record UserRpDto(
        Guid Id,
        string Email,
        string Name
        )
    {
        public static UserRpDto FromEntity(Core.Entities.User entity)
        {
            return new UserRpDto(entity.Id, entity.Email, entity.Name);
        }

        public static Core.Entities.User ToEntity(UserRpDto dto)
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