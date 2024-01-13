using Ciizo.CleanPattern.Domain.Business.User.Models;
using Ciizo.CleanPattern.Domain.Business.User.Validators;
using FluentValidation;
using FluentValidation.TestHelper;

namespace Ciizo.CleanPattern.UnitTests.Domain.Business.User.Validators
{
    public class UserSearchCriteriaValidatorTests
    {
        [Fact]
        public async Task ValidateAndThrowAsync_ValidRequestData_PassValidation()
        {
            UserSearchCriteriaValidator validator = new();
            UserSearchCriteria searchCriteria = new() { Name = "test" };

            var exception = await Record.ExceptionAsync(async () => await validator.ValidateAndThrowAsync(searchCriteria));

            Assert.Null(exception);
        }

        [Theory]
        [InlineData(" ")]
        [InlineData("")]
        [InlineData(null)]
        public async Task TestValidateAsync_InvalidNameCriteria_ThrowValidationException(string name)
        {
            UserSearchCriteriaValidator validator = new();
            UserSearchCriteria searchCriteria = new() { Name = name };

            var result = await validator.TestValidateAsync(searchCriteria);

            result.ShouldHaveValidationErrorFor(x => x.Name);
        }
    }
}