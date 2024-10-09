# Practice.Web

## Features

### Api
1. POST **/register** Register new user
2. POST **/logn** Login with exists user
3. POST **/change-password** Change the current user's password
4. POST **/logout** Logout by invalidating session
5. GET **/health** Simple HealthChek

See `localhost:5082/swagger/index.html` for testing.

#### How To Run

1. Open Terminal
2. Type And Enter ```cd path/to/sln/```
3. Type And Enter ```dotnet run --project ./src/Practice.Web.Api/Practice.Web.Api.csproj ```

### MVC

1. Allow User Login & Register
2. User Dashboard

See `localhost:5225` for testing.

#### How To Run

1. Open Terminal
2. Type And Enter ```cd path/to/sln/```
3. Type And Enter ```dotnet run --project ./src/Practice.Web.Mvc/Practice.Web.Mvc.csproj ```

### Blazor

1. Allow User Login & Register
2. User Dashboard

See `localhost:5147` for testing.

#### How To Run

1. Open Terminal
2. Type And Enter ```cd path/to/sln/```
3. Type And Enter ```dotnet run --project ./src/Practice.Web.Api/Practice.Web.Api.csproj ```
3. Type And Enter ```dotnet run --project ./src/Practice.Web.BlazorApp/Practice.Web.BlazorApp.csproj ```
