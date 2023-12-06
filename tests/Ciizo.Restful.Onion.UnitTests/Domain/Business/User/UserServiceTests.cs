using Ciizo.Restful.Onion.Domain.Business.Common.Models;
using Ciizo.Restful.Onion.Domain.Business.Exceptions;
using Ciizo.Restful.Onion.Domain.Business.User;
using Ciizo.Restful.Onion.Domain.Business.User.Models;
using Ciizo.Restful.Onion.Domain.Core.Repository;
using Ciizo.Restful.Onion.UnitTests.TestFakers.User;
using MockQueryable.Moq;

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
            UserDtoFaker userDtoFaker = new();
            UserDto userDto = userDtoFaker.Generate();
            _userRepositoryMock.Setup(x => x.SaveChangesAsync(CancellationToken.None)).Returns(Task.CompletedTask);

            var result = await _userService.CreateUserAsync(userDto, CancellationToken.None);

            var expected = new UserDto(result.Id, userDto.Email, userDto.Name);
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

        [Fact]
        public async Task SearchUsersAsync_ValidId_ReturnUserDto()
        {
            SetUp();
            UserFaker userFaker = new();
            var userEntities = userFaker.Generate(5);
            var searchCriteria = new UserSearchCriteria { Name = userEntities[2].Name };
            var usersQueryable = userEntities.AsQueryable().BuildMock();
            _userRepositoryMock.Setup(x => x.GetQueryable()).Returns(usersQueryable);

            var result = await _userService.SearchUsersAsync(searchCriteria, 1, 3, CancellationToken.None);

            var expected = new SearchResult<UserDto>
            {
                Data = new List<UserDto>() { UserDto.FromEntity(userEntities[2]) },
                TotalCount = 1,
            };
            result.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public async Task DeleteUserAsync_InvalidId_ThrowArgumentException()
        {
            SetUp();
            Guid invalidId = default;

            var act = async () => await _userService.DeleteUserAsync(invalidId, CancellationToken.None);

            await act.Should().ThrowAsync<ArgumentException>()
                .WithMessage("Id is required.");
        }

        [Fact]
        public async Task DeleteUserAsync_ValidId_ReturnUserDto()
        {
            SetUp();
            Guid id = Guid.NewGuid();

            var act = async () => await _userService.DeleteUserAsync(id, CancellationToken.None);

            await act.Should().NotThrowAsync();
            await act.Should().CompleteWithinAsync(TimeSpan.FromSeconds(15));
        }

        [Fact]
        public async Task UpdateUserAsync_InvalidId_ThrowArgumentException()
        {
            SetUp();
            Guid invalidId = default;

            var act = async () => await _userService.UpdateUserAsync(invalidId, null!, CancellationToken.None);

            await act.Should().ThrowAsync<ArgumentException>()
                .WithMessage("Id is required.");
        }

        [Fact]
        public async Task UpdateUserAsync_IdNotFound_ThrowDataNotFoundException()
        {
            SetUp();
            Guid notExistedId = Guid.NewGuid();
            UserFaker userFaker = new();
            Onion.Domain.Core.Entities.User entity = userFaker.Generate();
            _userRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<Guid>(), CancellationToken.None))
                .ReturnsAsync((Onion.Domain.Core.Entities.User)null!);

            var act = async () => await _userService.UpdateUserAsync(notExistedId, UserDto.FromEntity(entity), CancellationToken.None);

            await act.Should().ThrowAsync<DataNotFoundException>()
                .WithMessage("User not found.");
        }

        [Fact]
        public async Task UpdateUserAsync_ValidRequestData_CompleteUpdateUser()
        {
            SetUp();
            UserFaker userFaker = new();
            Onion.Domain.Core.Entities.User entity = userFaker.Generate();
            Guid id = entity.Id;
            var dtoToUpdate = UserDto.FromEntity(entity) with
            {
                Email = "test.edit@example.com",
                Name = "test-edit",
            };

            _userRepositoryMock.Setup(x => x.GetByIdAsync(id, CancellationToken.None)).ReturnsAsync(entity);

            var act = async () => await _userService.UpdateUserAsync(id, dtoToUpdate, CancellationToken.None);

            await act.Should().NotThrowAsync();
            await act.Should().CompleteWithinAsync(TimeSpan.FromSeconds(15));
        }
    }
}