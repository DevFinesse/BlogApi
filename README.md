# BlogApi

A feature-rich ASP.NET Core Web API for a blogging platform using layered architecture (Presentation, Service, Repository, Entities, Contracts, Shared). The project follows Repository and Service patterns, AutoMapper, and EF Core with PostgreSQL. Docker and health checks are included.

## Table of Contents
- About
- Architecture
- Projects
- Prerequisites
- Local setup
  - Build
  - Database migrations
  - Running with Docker Compose
- Development workflow
- Key concepts
  - RepositoryManager & ServiceManager
  - Threaded comments
  - Slug generation
  - Data shaping & HATEOAS links
- Testing
- Troubleshooting
- Contributing
- License

## About
This repository implements a blog backend with support for posts, categories, comments (threaded replies), authentication, and pagination. It uses EF Core (PostgreSQL), AutoMapper for DTO mapping, and a clear separation of concerns.

## Architecture
- BlogApi (main host project)
- BlogApi.Presentation (controllers)
- Repository (EF Core DbContext, repositories, configurations, migrations)
- Service (business logic)
- Contracts (interfaces shared between layers)
- Entities (EF models)
- Shared (DTOs and shared utilities)
- LoggerService (logging abstraction)

## Projects
Brief mapping of important projects and folders:
- `BlogApi/` - application host and DI setup
- `BlogApi.Presentation/Controllers/` - API endpoints
- `Repository/` - `RepositoryContext`, repositories, and EF configurations
- `Service/` - business logic and managers
- `Service.Contracts/` - service interfaces
- `Contracts/` - repository interfaces
- `Entities/` - entity models
- `Shared/` - DTOs

## Prerequisites
- .NET SDK 8.0+
- Docker & Docker Compose (optional - for local DB)
- PostgreSQL (if not using Docker)
- dotnet-ef tools (for migrations)

Install dotnet ef (if missing):

```powershell
dotnet tool install --global dotnet-ef
```

## Local setup
1. Restore packages:
```powershell
dotnet restore
```

2. Build:
```powershell
dotnet build
```

3. Configure connection string:
Edit `appsettings.json` or set `ConnectionStrings:SqlConnection` environment variable. Example using Docker Compose uses `Host=db;Port=5432;Database=blog;Username=postgres;Password=password`.

### Database migrations
This repository historically used migrations in the `Repository` project or `BlogApi` depending on configuration. By default, migrations are configured where the `MigrationsAssembly` is set in `DbContext` registration.

To add a migration (adjust `--project` and `--startup-project` as needed):

```powershell
# Add migration that will be placed into the Repository project
dotnet ef migrations add InitialCreate --project Repository --startup-project BlogApi

# Apply migrations
dotnet ef database update --project Repository --startup-project BlogApi
```

If you prefer migrations in the host project (BlogApi):
```powershell
dotnet ef migrations add InitialCreate --project BlogApi --startup-project BlogApi
```

### Running with Docker Compose
Start containers (Postgres + API):

```powershell
docker-compose up --build
```

To reset the DB data (removes named volumes):

```powershell
docker-compose down -v
docker-compose up --build
```

## Development workflow
- Repositories implement data access and expose methods via `IRepositoryManager`.
- Services implement business logic and use `IServiceManager` to access services.
- Controllers depend on service interfaces and return DTOs.
- AutoMapper maps between entities and DTOs.
- Seed data is provided via EF Core configuration classes (e.g., `CategoryConfiguration`). Avoid duplicating seeds when using persistent volumes.

## Key concepts
### RepositoryManager & ServiceManager
`RepositoryManager` centralizes repository instances (lazy-loaded). `ServiceManager` centralizes services. Controllers ask `IServiceManager` for services; services use `IRepositoryManager` for data access.

### Threaded comments
The `Comment` entity supports `ParentCommentId`, `ParentComment`, and `Replies` to build a tree. Repository loads all comments for a post and assembles the hierarchy in-memory to support unlimited nesting.

### Slug generation
`SlugService` generates URL-friendly slugs and `GenerateUniqueSlug` ensures uniqueness by checking existing slugs via repository callbacks.

### Data shaping & HATEOAS links
Utility classes produce shaped responses and link generation for HATEOAS. See `PostLinks` for example.

## Troubleshooting
- "Unable to cast object of type 'System.Net.Http.Headers.MediaTypeHeaderValue'...": ensure you use `Microsoft.Net.Http.Headers.MediaTypeHeaderValue` consistently when referencing media types in link utilities. Import correct namespace.
- `.vs` files causing git issues: add `.vs/` to `.gitignore` and remove from git index: `git rm -r --cached .vs`.
- Migrations assembly mismatch: set `MigrationsAssembly` in `AddDbContext` or use `--project/--startup-project` flags with `dotnet ef`.

## Contributing
Open issues or PRs. Keep changes small and targeted.

## License
MIT
