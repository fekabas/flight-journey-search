# flight-journey-search
## Dependencies
* .Net 8 SDK
* Postgres or SQL database

## Appsettings configuration
* Postgress or SQL connection string
* Postgres or SQL provider
* Api Key

## Migrations
Run the following commands to generate the initial migraiton and applying it.

### If using a postgres database
```
dotnet ef migrations add Initial --startup-project ..\WebAPI\WebAPI.csproj --context ModelsDbContextPostgreSQL --output-dir Migrations\ModelsDbPostgreSQL
```
```
dotnet ef database update --startup-project ..\WebAPI\WebAPI.csproj --context ModelsDbContextPostgreSQL
```

### If using an SQL database
```
dotnet ef migrations add Initial --startup-project ..\WebAPI\WebAPI.csproj --context ModelsDbSQLContext  --output-dir Migrations\ModelsDbSQL
```
```
dotnet ef database update --startup-project ..\WebAPI\WebAPI.csproj --context ModelsDbSQLContext
```

## Run the project
Go to the WebAPI directory and run the project.
```
cd WebAPI
```
```
dotnet run
```
By default, the API listens on port 8081. You can change this by updating the port in appsettings.
