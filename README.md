# Example Mvc App

This is an example of a .NET Core 5.0 MVC API. Complete with a frontend MVC app and a backend RESTful API.

## Technologies
- [.NET Core 5.0 MVC]
- [SQL Server]
- [EF Core]
- [Dapper]
- [NUnit]

## Running
Open the Project in Visual Studio, set ExampleMvcApp as the start up project, and pres

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
