using Banking.Domain.Service.Models;
using Ciizo.Restful.Onion.Domain.Business.Exceptions;
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
        public async Task CreateUserAsync_ValidRequestData_ReturnCreatedUserDto()
        {
            SetUp();
            UserCreateDtoFaker userCreateDtoFaker = new();
            UserCreateDto userCreateDto = userCreateDtoFaker.Generate();
            _userRepositoryMock.Setup(x => x.SaveChangesAsync(CancellationToken.None)).Returns(Task.CompletedTask);

            var result = await _userService.CreateUserAsync(userCreateDto, CancellationToken.None);

            var expected = new UserDto(result.Id, userCreateDto.Email, userCreateDto.Name);
            result.Should().BeEquivalentTo(expected);
        }


        [Fact]
        public async Task GetUserAsync_InvalidId_ThrowArgumentException()
        {
            SetUp();
            Guid invalidId = default;

            var act = async () => await _userService.GetUserAsync(invalidId, CancellationToken.None);

            await act.Should().ThrowAsync<ArgumentException>()
                .WithMessage("Id is required.");
        }

        [Fact]
        public async Task GetUserAsync_IdNotFound_ThrowDataNotFoundException()
        {
            SetUp();
            Guid notExistedId = Guid.NewGuid();
            _userRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<Guid>(), CancellationToken.None))
                .ReturnsAsync((Onion.Domain.Core.Entities.User)null!);

            var act = async () => await _userService.GetUserAsync(notExistedId, CancellationToken.None);

            await act.Should().ThrowAsync<DataNotFoundException>()
                .WithMessage("User not found.");
        }

        [Fact]
        public async Task GetUserAsync_ValidId_ReturnUserDto()
        {
            SetUp();
            UserFaker userFaker = new();
            Onion.Domain.Core.Entities.User entity = userFaker.Generate();
            Guid id = entity.Id;
            _userRepositoryMock.Setup(x => x.GetByIdAsync(id, CancellationToken.None)).ReturnsAsync(entity);

            var result = await _userService.GetUserAsync(id, CancellationToken.None);

            var expected = new UserDto(id, entity.Email, entity.Name);
            result.Should().BeEquivalentTo(expected);
        }
    }
}