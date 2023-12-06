# dotnet-restful-onion

Onion architecture .Net API 

## Quick Start
1. Clone this project  
2. Start API using Docker  
3. Generate JWT  
4. Let's play

#### Generate JWT
Have 2 options
- use [online web generator](https://dinochiesa.github.io/jwt/).  
- use `dotnet user-jwts`, see [this link](https://learn.microsoft.com/en-us/aspnet/core/security/authentication/jwt-authn?view=aspnetcore-8.0&tabs=windows).

JWT parameters
- key is `ciizojwtsigningkey12345678900000`, endcode the key using UTF-8.  
- payload  
  ```
  {
  "iss": "ciizo",
  "sub": "your-name",
  "aud": "ciizo",
  "iat": 1701851815,
  "exp": 1701856950,
  "UserType": "Admin"
  }
  ```  

#### Database
disable `UseInMemoryDatabase` config for using real database.   

## Decisions and Trade-offs
- Using RDBMS because user systems usually have structured schema, need data integrity, and possibly require joins, but scaling is more challenging compared to NoSQL  
- Not using MQ because don't know the server loads and events yet  
- Using .Net because it's cross-platform and has many helper libraries for creating general applications. Also, it's my expertise  
- Using repository pattern, not CQRS because it's general and don't know the server loads, and the features are still small  
- Using Onion architecture because it's good for general application, not overkill, and easy to apply software principles  


## Stuff will do later
- Store secrets in Key Vault  
- Seriog for logging
- Observability 
- Parallel testing  
- Rate limiting  
- API versioning
- Health check endpoint
- Generic test case that checks all properties of response DTO are assigned  
- Improving SearchUserAsync performance using Elasticsearch/Indexing/Full-text search  
- Add unit test for DTO mappers, Middlewares, Attributes, ...  
- Refactoring for reusing model faker classes  
- Improving integration tests
- Interceptor for AuditableEntity
- Cache (if need)
- CI/CD  
