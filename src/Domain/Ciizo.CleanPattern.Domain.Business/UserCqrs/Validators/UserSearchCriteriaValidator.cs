using Ciizo.CleanPattern.Domain.Business.UserCqrs.Models;
using FluentValidation;

namespace Ciizo.CleanPattern.Domain.Business.UserCqrs.Validators
{
    public class UserSearchCriteriaValidator : AbstractValidator<UserCqrsSearchCriteria>
    {
        public UserSearchCriteriaValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
        }
    }
}