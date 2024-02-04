using Ciizo.CleanPattern.Domain.Business.UserResultPattern.Models;
using FluentValidation;

namespace Ciizo.CleanPattern.Domain.Business.UserResultPattern.Validators
{
    public class UserSearchCriteriaValidator : AbstractValidator<UserRpSearchCriteria>
    {
        public UserSearchCriteriaValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
        }
    }
}