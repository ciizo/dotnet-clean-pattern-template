using Ciizo.CleanPattern.Domain.Business.User.Models;
using FluentValidation;

namespace Ciizo.CleanPattern.Domain.Business.User.Validators
{
    public class UserDtoValidator : AbstractValidator<UserDto>
    {
        public UserDtoValidator()
        {
            RuleFor(x => x.Email).EmailAddress().NotNull();
            RuleFor(x => x.Name).NotEmpty();
        }
    }
}