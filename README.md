# DiningWheels

DiningWheels is an early-stage food-ordering platform built with .NET 8 and Blazor WebAssembly.
The project focuses on clean backend structure, testable application logic, and a Docker-based local setup.

## Tech Stack

- .NET 8
- ASP.NET Core + Blazor WebAssembly (hosted)
- Entity Framework Core + PostgreSQL (Npgsql)
- MediatR + CQRS style commands/queries
- FluentValidation
- xUnit + EF Core InMemory + Moq
- Docker (PostgreSQL + pgAdmin)

## Architecture

The solution is split into dedicated layers/projects:

- `DiningWheels.Domain` - entities and enums
- `DiningWheels.Application` - commands, queries, handlers, validators, pipeline behaviors
- `DiningWheels.Persistance` - `DbContext`, EF configuration, PostgreSQL integration
- `DiningWheels.Infrastructure` - SMTP email service and infrastructure DI
- `DiningWheels.Server` - API + Blazor host
- `DiningWheels.Client` - Blazor WebAssembly frontend
- `DiningWheels.Tests` - unit/integration-style tests

## Current Scope

Implemented:

- User creation flow (`CreateUserCommand`)
- Restaurant creation flow (`CreateRestaurantCommand`)
- Role transition to `Owner` during restaurant creation
- Restaurant listing query (`GetRestaurantsQuery`)
- Request validation via FluentValidation + MediatR pipeline
- SMTP email notification after restaurant registration
- Soft-delete and global query filters for active entities

## Local Development

### 1. Start PostgreSQL (Docker)

```bash
docker compose -f docker/docker-compose.yml up -d
```

Services:

- PostgreSQL: `localhost:5432`
- pgAdmin: `localhost:5050`

### 2. Configure app secrets (development)

`Server` uses User Secrets for SMTP settings. You can use any SMTP provider; commands below show a Mailtrap example:

```bash
dotnet user-secrets set "Smtp:Host" "sandbox.smtp.mailtrap.io" --project DiningWheels/Server/DiningWheels.Server.csproj
dotnet user-secrets set "Smtp:Port" "587" --project DiningWheels/Server/DiningWheels.Server.csproj
dotnet user-secrets set "Smtp:Username" "<your-username>" --project DiningWheels/Server/DiningWheels.Server.csproj
dotnet user-secrets set "Smtp:Password" "<your-password>" --project DiningWheels/Server/DiningWheels.Server.csproj
dotnet user-secrets set "Smtp:From" "noreply@diningwheels.com" --project DiningWheels/Server/DiningWheels.Server.csproj
```

### 3. Run the application

```bash
dotnet run --project DiningWheels/Server/DiningWheels.Server.csproj
```

Default URLs are defined in `DiningWheels/Server/Properties/launchSettings.json`.

## API Endpoints (current)

- `POST /api/users`
- `POST /api/restaurants`
- `GET /api/restaurants`

Example payloads:

```json
{
  "email": "owner@example.com",
  "fullName": "John Doe",
  "password": "Abcd1234"
}
```

```json
{
  "name": "Pasta House",
  "address": "Main Street 1",
  "latitude": 52.2297,
  "longitude": 21.0122,
  "ownerEmail": "owner@example.com"
}
```

## Running Tests

```bash
dotnet test DiningWheels/DiningWheels.sln
```

## Notes

- Project status: early stage, under active development.
- SMTP provider is fully configurable; I use Mailtrap for development testing.
