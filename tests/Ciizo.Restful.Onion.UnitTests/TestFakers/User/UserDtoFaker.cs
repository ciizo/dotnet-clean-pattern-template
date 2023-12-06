using Ciizo.Restful.Onion.Domain.Business.User.Models;

namespace Ciizo.Restful.Onion.UnitTests.TestFakers.User
{
    public class UserDtoFaker : Faker<UserDto>
    {
        public UserDtoFaker()
        {
            CustomInstantiator(faker =>
            {
                return new UserDto
                (
                   Id: faker.Random.Guid(),
                   Email: faker.Internet.ExampleEmail(),
                   Name: faker.Name.FullName()
                );
            });
        }
    }
}