# smart-table

## Prerequisites

1. .NET Core SDK 3.1
1. Entity Framework 3.1.3 (global install with `dotnet tool install --global dotnet-ef --version 3.1.3`)
1. Postgres 12

## Running

Updating packages "dotnet restore"
Running locally "dotnet run"

## Migrations

Run migrations using "dotnet ef database update"
Code first approach. Modify entities and database context, then run "dotnet ef migrations add MigrationName"

## appsettings.json
Change connection  string to suit your system, mainly password and database
Add appsettings.json file to your tree with this structure
{
  "ConnectionStrings": {
    "TestBD": "USER ID=postgres;Password=leaveEmptyIfYouHaveNoPass;Host=127.0.0.1;Port=5432;Database=databasename;Pooling=true;"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft": "Warning",
      "Microsoft.Hosting.Lifetime": "Information"
    }
  },
  "AllowedHosts": "*"
}
