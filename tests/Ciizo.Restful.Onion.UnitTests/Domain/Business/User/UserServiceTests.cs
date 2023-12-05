using Banking.Domain.Service.Models;
using Ciizo.Restful.Onion.Domain.Business.User;
using Ciizo.Restful.Onion.Domain.Core.Repository;
using Ciizo.Restful.Onion.UnitTests.TestFakers.User;

namespace Ciizo.Restful.Onion.UnitTests.Domain.Business.User
{
    public class UserServiceTests
    {
        private IUserService _userService = null!;
        private Mock<IRepository<Onion.Domain.Core.Entities.User, IApplicationDbContext>> _userRepositoryMock = null!;

        private void SetUp()
        {
            _userRepositoryMock = new Mock<IRepository<Onion.Domain.Core.Entities.User, IApplicationDbContext>>();
            _userService = new UserService(_userRepositoryMock.Object);
        }

        [Fact]
        public async Task CreateUserAsync_InvalidRequestData_ReturnCreatedUserDto()
        {
            SetUp();
            UserCreateDtoFaker userCreateDtoFaker = new();
            UserCreateDto dto = userCreateDtoFaker.Generate();
            _userRepositoryMock.Setup(x => x.SaveChangesAsync(CancellationToken.None)).Returns(Task.CompletedTask);

            var result = await _userService.CreateUserAsync(dto, CancellationToken.None);

            var expected = new UserDto(result.Id, dto.Email, dto.Name);
            result.Should().BeEquivalentTo(expected);
        }
    }
}