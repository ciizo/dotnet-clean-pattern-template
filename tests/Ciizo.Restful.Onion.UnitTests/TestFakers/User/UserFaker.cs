namespace Ciizo.Restful.Onion.UnitTests.TestFakers.User
{
    public class UserFaker : Faker<Onion.Domain.Core.Entities.User>
    {
        public UserFaker()
        {
            RuleFor(x => x.Id, faker => faker.Random.Guid());
            RuleFor(x => x.Email, faker => faker.Internet.ExampleEmail());
            RuleFor(x => x.Name, faker => faker.Name.FullName());
        }
    }
}