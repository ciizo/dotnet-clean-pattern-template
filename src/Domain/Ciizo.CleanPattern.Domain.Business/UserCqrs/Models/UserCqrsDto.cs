namespace Ciizo.CleanPattern.Domain.Business.UserCqrs.Models
{
    public record UserCqrsDto(
        Guid Id,
        string Email,
        string Name
        )
    {
        public static UserCqrsDto FromEntity(Core.Entities.User entity)
        {
            return new UserCqrsDto(entity.Id, entity.Email, entity.Name);
        }

        public static Core.Entities.User ToEntity(UserCqrsDto dto)
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