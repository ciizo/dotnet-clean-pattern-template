using Banking.Domain.Service.Models;
using FluentValidation;

namespace Banking.Domain.Service.Validators
{
    public class UserCreateDtoValidator : AbstractValidator<UserCreateDto>
    {
        public UserCreateDtoValidator()
        {
            RuleFor(x => x.Email).EmailAddress().NotNull();
            RuleFor(x => x.Name).NotEmpty();
        }
    }
}