﻿using Ciizo.Restful.Onion.Domain.Business.User.Models;
using FluentValidation;

namespace Ciizo.Restful.Onion.Domain.Business.User.Validators
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