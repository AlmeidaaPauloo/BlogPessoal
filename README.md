# Blog Pessoal – API

> **Study project – Generation Brasil bootcamp (2022).**
> Built while learning back-end development with C# and ASP.NET Core. It is kept here as a record of that work, not as a maintained product.

REST API for a simple blog: users, themes and posts, with JWT authentication and role-based authorization.
The front end lives in a separate repository: [blog-pessoal-frontend](https://github.com/AlmeidaaPauloo/blog-pessoal-frontend).

## Stack

- C# / ASP.NET Core (.NET 5)
- Entity Framework Core + SQL Server
- JWT Bearer authentication, roles `NORMAL` and `ADMINISTRATOR`
- Swagger (Swashbuckle)
- MSTest with EF Core InMemory for data and repository tests

## Structure

```
BlogPessoal/
  src/Controllers    Authentication, Users, Themes, Posts
  src/Data           DbContext
  src/Dtos           request objects
  src/Repositories   interfaces + implementations
  src/Services       token generation
  src/models         entities
BlogPessoalTESTE/    unit tests
```

## Endpoints

| Resource | Base route |
|---|---|
| Authentication | `api/Authentication` |
| Users | `api/Users` |
| Themes | `api/Themes` |
| Posts | `api/Post` |

Full details are available in Swagger when the API is running.

## Running locally

Requirements: .NET 5 SDK and a local SQL Server instance.

1. Set a JWT signing key (not stored in the repository):

   ```bash
   cd BlogPessoal
   dotnet user-secrets set "Settings:Secret" "<a long random string>"
   ```

   Or use environment variables: `Settings__Secret` and, for a non-local database, `ConnectionStrings__DefaultConnection`.

2. Run the API:

   ```bash
   dotnet run --project BlogPessoal
   ```

3. Run the tests:

   ```bash
   dotnet test
   ```

By default (`Enviroment:Start = DEV`) the API uses the local connection in `ConnectionStringsDev`. The database is created on first run with `EnsureCreated()`.

## Status

The original deploy (Heroku) is no longer online. .NET 5 is out of support; an update to a current LTS version would be the first step if this project is revisited.
