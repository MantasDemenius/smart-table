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
