using Ciizo.CleanPattern.Domain.Business.UserCqrs.Models;
using FluentValidation;

namespace Ciizo.CleanPattern.Domain.Business.UserCqrs.Validators
{
    public class UserDtoValidator : AbstractValidator<UserCqrsDto>
    {
        public UserDtoValidator()
        {
            RuleFor(x => x.Email).EmailAddress().NotNull();
            RuleFor(x => x.Name).NotEmpty();
        }
    }
}