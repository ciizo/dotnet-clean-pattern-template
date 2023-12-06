using Ciizo.Restful.Onion.Domain.Business.User.Models;
using Ciizo.Restful.Onion.Domain.Core.Repository;
using Ciizo.Restful.Onion.IntegrationTests.TestFakers.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace Ciizo.Restful.Onion.IntegrationTests.Application.Controllers
{
    public class UserControllerTests : TestFixture
    {
        private const string BaseUrl = "api/users";

        [Fact]
        public async Task GetUserAsync_ValidRequest_ReturnUserDto()
        {
            using var scope = CreateScope();
            var client = GetHttpClient();
            UserFaker userFaker = new();
            var user = userFaker.Generate();
            var dbContext = scope.ServiceProvider.GetRequiredService<IApplicationDbContext>();
            await dbContext.Users.AddAsync(user);
            await dbContext.SaveChangesAsync();

            var response = await client.GetAsync(Path.Combine(BaseUrl, user.Id.ToString()));
            var contentString = await response.Content.ReadAsStringAsync();
            var userDtoResult = JsonConvert.DeserializeObject<UserDto>(contentString);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            userDtoResult!.Should().BeEquivalentTo(UserDto.FromEntity(user));
        }

        [Fact]
        public async Task UpdateUserAsync_ValidRequest_CompleteUpdateUser()
        {
            using var scope = CreateScope();
            var client = GetHttpClient();
            UserFaker userFaker = new();
            var user = userFaker.Generate();
            var dbContext = scope.ServiceProvider.GetRequiredService<IApplicationDbContext>();
            await dbContext.Users.AddAsync(user);
            await dbContext.SaveChangesAsync();
            var userToUpdate = UserDto.FromEntity(user) with
            {
                Email = "test.edit@example.com",
                Name = "test-edit"
            };

            var response = await client.PutAsync(Path.Combine(BaseUrl, user.Id.ToString()),
                new StringContent(JsonConvert.SerializeObject(userToUpdate), Encoding.UTF8, "application/json"));

            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
            using (var scopeAssert = CreateScope())
            {
                dbContext = scopeAssert.ServiceProvider.GetRequiredService<IApplicationDbContext>();
                var updatedUser = await dbContext.Users.FirstOrDefaultAsync();
                updatedUser!.Should().BeEquivalentTo(userToUpdate);
            }
        }
    }
}