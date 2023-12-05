using Ciizo.Restful.Onion.Domain.Business.User.Models;
using FluentValidation;

namespace Ciizo.Restful.Onion.Domain.Business.User.Validators
{
    public class UserSearchCriteriaValidator : AbstractValidator<UserSearchCriteria>
    {
        public UserSearchCriteriaValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
        }
    }
}