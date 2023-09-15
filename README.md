# Alteration Api

# Introduction 
The service is intended to provide the REST API for the Alteration website. 

# Getting Started
Make sure that the following is installed:
- [.NET Core 7 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/7.0)

# Environment Setup
- provide connection string to the SQL Server Database in either Environment Variables or appsettings.json file
- update database with migrations following ef core guidelines [Ef Core Migrations Guideline](https://learn.microsoft.com/en-us/ef/core/managing-schemas/migrations/?tabs=dotnet-core-cli):
    1. In VS Package Manager Console set the env variable with the command: $env:SqlServerOptions__ConnectionString = [your connection string]
    2. Set Default Project to Alteration.Application
    3. Execute command: update-database
- [optional] provide connection string to the Application Insights service