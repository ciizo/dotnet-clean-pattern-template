using Ciizo.CleanPattern.Domain.Business.UserResultPattern.Models;
using FluentValidation;

namespace Ciizo.CleanPattern.Domain.Business.UserResultPattern.Validators
{
    public class UserDtoValidator : AbstractValidator<UserRpDto>
    {
        public UserDtoValidator()
        {
            RuleFor(x => x.Email).EmailAddress().NotNull();
            RuleFor(x => x.Name).NotEmpty();
        }
    }
}