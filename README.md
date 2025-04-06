# Garneau Template

## Stack

- .NET 8
- SQL database

## Dependencies

- FastEndpoint
- Entity Framework Core 8

## Installation

- Install latest SDK of .NET Core 8 [here] (https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
- Install .NET EF CORE CLI tool [here] (https://learn.microsoft.com/en-us/ef/core/cli/dotnet)
- Restore nuget package
- Install [Sql Server](https://www.microsoft.com/en-ca/sql-server/sql-server-downloads)
- Install [SSMS](https://learn.microsoft.com/en-us/sql/ssms/download-sql-server-management-studio-ssms?view=sql-server-ver16)
- Create dev user with rights to create tables :
  - In `Security` folder, right click on `Logins` and select `New login`
  - Select SQL Server authentication
  - Login name : `dev`
  - Password : `dev`
  - Click OK
- Install nvm
- Install node 21.7.3
    ```bash
    $ nvm install 21.7.3
    $ nvm use 21.7.3
    ```
- Run this command to generate JWT token secret key
    ```bash
    $ node -e "console.log(require('crypto').randomBytes(32).toString('hex'))"
    ```
- Copy the printed value to appsettings.Development.json JwtToken:SecretKey

### Run front-end Vue app
```bash
$ cd .\src\Web\vue-app
# Installs dependencies
$ npm install yarn
$ yarn install

# Compiles and minifies for production
$ yarn build --watch
```

### Run back-end .NET Core 8 app
```bash
$ cd .\src\Web
$ npm install gulp
# This will run both dotnet watch run command and gulp command (for login page css)
$ npm run dev
# You can also run them separately
$ dotnet watch run
$ gulp
```

### Seed
- Default user is `admin@gmail.com` with password `Qwerty123!`

### Migrations

From the Infrastructure assembly:

```bash
$ cd .\src\Persistence

$ dotnet ef migrations add {MigrationName} --startup-project ..\Web\

$ dotnet ef database update --startup-project ../Web/
```

## Instant
An instant represents a moment in time and is always in UTC. Hence, `InstantHelper.GetUtcNow()` will return UTC current date and time. 
However, if you parse a string to an Instant (using `ParseFromString` for example) the Instant will contain the same date as the string but it will be saved in UTC. 
For example, 
- If you parse the string `2023-10-12` to an Instant, the Instant object will contain `2023-10-12 00:00:00` and not `2023-10-11 20:00:00` (which is UTC equivalent of `2023-10-12 00:00:00`)
- But, `2023-10-11 20:00:00-04` will be saved in database
- But, when getting object from database after saving it, the Instant will be `2023-10-12 00:00:00` and not `2023-10-11 20:00:00`
- It is parsed automatically when getting object from database

In conclusion, you don't need to worry about the timezone when parsing a string to an Instant or when getting an Instant from database. The timezone will be your local timezone.
Also, you don't need (and should not) compare your object's Instant with `InstantHelper.GetUtcNow()` because the Instant will be in your local timezone and not in UTC.

#### Comparing object's Instant to current date
Although, your object's Instant will be in your local timezone, it will be saved in UTC. 

Here's a list of conversion methods offered with Instant and what they do to an Instant that contains `2023-10-10 00:00:00`
1. `ToDateTimeUtc()` : will return DateTime with `2023-10-10 00:00:00`
2. `ToDateTimeOffset()` : will return DateTime and offset with `2023-10-10 00:00:00 +00:00`

So to compare your object's instant to current date time, I suggest doing `myObject.ItsInstant.ToDateTimeUtc() < DateTime.Now`