﻿using Banking.Domain.Service.Models;

namespace Ciizo.Restful.Onion.UnitTests.TestFakers.User
{
    public class UserCreateDtoFaker : Faker<UserCreateDto>
    {
        public UserCreateDtoFaker()
        {
            CustomInstantiator(faker =>
            {
                return new UserCreateDto
                (
                   Email: faker.Internet.ExampleEmail(),
                   Name: faker.Name.FullName()
                );
            });
        }
    }
}