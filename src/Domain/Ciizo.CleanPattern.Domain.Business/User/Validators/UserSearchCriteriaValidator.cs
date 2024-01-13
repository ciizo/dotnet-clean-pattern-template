using Ciizo.CleanPattern.Domain.Business.User.Models;
using FluentValidation;

namespace Ciizo.CleanPattern.Domain.Business.User.Validators
{
    public class UserSearchCriteriaValidator : AbstractValidator<UserSearchCriteria>
    {
        public UserSearchCriteriaValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
        }
    }
}