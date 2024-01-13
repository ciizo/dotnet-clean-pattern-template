using Ciizo.CleanPattern.Domain.Business.User.Models;
using Ciizo.CleanPattern.Domain.Business.User.Validators;
using Ciizo.CleanPattern.UnitTests.TestFakers.User;
using FluentValidation;
using FluentValidation.TestHelper;

namespace Ciizo.CleanPattern.UnitTests.Domain.Business.User.Validators
{
    public class UserDtoValidatorTests
    {
        [Fact]
        public async Task ValidateAndThrowAsync_ValidRequestData_PassValidation()
        {
            UserDtoValidator validator = new();
            UserDtoFaker userDtoFaker = new();
            UserDto dto = userDtoFaker.Generate();

            var exception = await Record.ExceptionAsync(async () => await validator.ValidateAndThrowAsync(dto));

            Assert.Null(exception);
        }

        [Theory]
        [InlineData("abcd")]
        [InlineData(" ")]
        [InlineData("")]
        [InlineData(null)]
        public async Task TestValidateAsync_InvalidEmail_ThrowValidationException(string email)
        {
            UserDtoValidator validator = new();
            UserDtoFaker userDtoFaker = new();
            UserDto dto = userDtoFaker.Generate() with { Email = email };

            var result = await validator.TestValidateAsync(dto);

            result.ShouldHaveValidationErrorFor(x => x.Email);
        }

        [Theory]
        [InlineData(" ")]
        [InlineData("")]
        [InlineData(null)]
        public async Task TestValidateAsync_InvalidName_ThrowValidationException(string name)
        {
            UserDtoValidator validator = new();
            UserDtoFaker userDtoFaker = new();
            UserDto dto = userDtoFaker.Generate() with { Name = name };

            var result = await validator.TestValidateAsync(dto);

            result.ShouldHaveValidationErrorFor(x => x.Name);
        }
    }
}