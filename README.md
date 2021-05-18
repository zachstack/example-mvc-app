# Example Mvc App

This is an example of a .NET Core 5.0 MVC App. Complete with a frontend MVC app and a backend RESTful API.

## Technologies
- [.NET Core 5.0 MVC]
- [SQL Server]
- [EF Core]
- [Dapper]
- [NUnit]


## Connecting to the database
Add a file to the `ExampleMvcApp` project named `appsettings.Development.json` with the following content with your own SQL connection string
```
{
    "ConnectionStrings": {
        "ExampleDb" : "{your SQL connection string here}"
    }
}
```

#### Test runner 
Similarly, for the project `ExampleMvcApp.Tests`  add a file named  `appsettings.json`
```
{
    "ConnectionStrings": {
        "ExampleDb" : "{your SQL connection string here}"
    }
}
```

## Running
>Note: Ensure the SQL Connection Strings are defined before running the application.

Open the solution in Visual Studio. Make sure `ExampleMvcApp` is the Startup Project, and start the project (F5).