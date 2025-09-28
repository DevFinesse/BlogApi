# BlogApi

A feature-rich ASP.NET Core Web API for a blogging platform using layered architecture (Presentation, Service, Repository, Entities, Contracts, Shared). The project follows Repository and Service patterns, AutoMapper, and EF Core with PostgreSQL. Docker and health checks are included.

URL: https://url-shortener.me/5K3A

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
- API Endpoints
  - Authentication Endpoints
  - Token Management
  - Root API
  - Posts Endpoints
  - Categories Endpoints
  - Comments Endpoints
  - Content Negotiation
  - Pagination
  - Authentication
  - Rate Limiting
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

## API Endpoints

### Authentication Endpoints (`/api/authentication`)

| Method | Endpoint | Description | Request Body | Response |
|--------|----------|-------------|--------------|----------|
| POST | `/api/authentication` | Register a new user | `UserRegistrationDto` | 201 Created / 400 Bad Request |
| POST | `/api/authentication/login` | Authenticate user and get JWT token | `UserAuthenticationDto` | 200 OK (TokenDto) / 401 Unauthorized |

### Token Management (`/api/token`)

| Method | Endpoint | Description | Request Body | Response |
|--------|----------|-------------|--------------|----------|
| POST | `/api/token/refresh` | Refresh JWT access token | `TokenDto` | 200 OK (TokenDto) / 400 Bad Request |

### Root API (`/api`)

| Method | Endpoint | Description | Headers | Response |
|--------|----------|-------------|---------|----------|
| GET | `/api` | Get API root with HATEOAS links | `Accept: application/vnd.isaacman.apiroot` | 200 OK (Link[]) / 204 No Content |

### Posts Endpoints (`/api/posts`)

| Method | Endpoint | Description | Query Parameters | Headers | Response |
|--------|----------|-------------|------------------|---------|----------|
| GET | `/api/posts` | Get all posts with pagination and HATEOAS | `PostParameter` | `Accept: application/vnd.isaacman.hateoas+json` | 200 OK (PostDto[]) |
| HEAD | `/api/posts` | Get posts headers only | `PostParameter` | - | 200 OK |
| GET | `/api/posts/{id:guid}` | Get post by ID | - | - | 200 OK (PostDto) / 404 Not Found |
| GET | `/api/posts/collection/({ids})` | Get multiple posts by IDs | - | - | 200 OK (PostDto[]) |
| GET | `/api/posts/{slug}` | Get post by slug | - | - | 200 OK (PostDto) / 404 Not Found |
| POST | `/api/posts` | Create new post | - | - | 201 Created / 400 Bad Request |
| POST | `/api/posts/collection` | Create multiple posts | - | - | 201 Created (PostCollectionDto) |
| DELETE | `/api/posts/{id:guid}` | Delete post | - | - | 204 No Content / 404 Not Found |
| PUT | `/api/posts/{id:guid}` | Update entire post | - | - | 204 No Content / 400 Bad Request |
| PATCH | `/api/posts/{id:guid}` | Partially update post | - | - | 204 No Content / 400 Bad Request |
| GET | `/api/posts/category` | Get posts by category | `categoryId` | - | 200 OK (PostDto[]) |
| OPTIONS | `/api/posts` | Get allowed HTTP methods | - | - | 200 OK |

### Categories Endpoints (`/api/categories`)

| Method | Endpoint | Description | Request Body | Response |
|--------|----------|-------------|--------------|----------|
| GET | `/api/categories` | Get all categories | - | 200 OK (CategoryDto[]) |
| GET | `/api/categories/{id:Guid}` | Get category by ID | - | 200 OK (CategoryDto) / 404 Not Found |
| POST | `/api/categories` | Create new category | `CategoryCreationDto` | 200 OK / 400 Bad Request |
| DELETE | `/api/categories/{id:guid}` | Delete category | - | 204 No Content / 404 Not Found |
| PUT | `/api/categories/{id:guid}` | Update category | `CategoryUpdateDto` | 204 No Content / 400 Bad Request |

### Comments Endpoints (`/api/posts/{postId}/comments`)

| Method | Endpoint | Description | Query Parameters | Request Body | Response |
|--------|----------|-------------|------------------|--------------|----------|
| GET | `/api/posts/{postId}/comments` | Get paginated comments for post | `CommentParameters` | - | 200 OK (CommentDto[]) |
| GET | `/api/posts/{postId}/comments/{id:Guid}` | Get specific comment | - | - | 200 OK (CommentDto) / 404 Not Found |
| POST | `/api/posts/{postId}/comments` | Create comment for post | - | `CommentCreationDto` | 200 OK (CommentDto) / 400 Bad Request |
| GET | `/api/posts/{postId}/comments/threaded` | Get threaded comments hierarchy | - | - | 200 OK (CommentDto[]) |
| GET | `/api/posts/{postId}/comments/{id:Guid}/replies` | Get comment replies | - | - | 200 OK (CommentDto[]) |
| DELETE | `/api/posts/{postId}/comments` | Delete comment | `id` | - | 204 No Content / 404 Not Found |
| PUT | `/api/posts/{postId}/comments/{id:guid}` | Update comment | - | `CommentUpdateDto` | 204 No Content / 400 Bad Request |

### Content Negotiation

The API supports multiple content types:

- **JSON**: `application/json` (default)
- **Custom JSON**: `application/vnd.isaacman.hateoas+json`, `application/vnd.isaacman+json`
- **XML**: `application/xml`
- **CSV**: `text/csv` (posts only)
- **HATEOAS Root**: `application/vnd.isaacman.apiroot`

### Pagination

Posts and comments endpoints support pagination with the following query parameters:

- `pageNumber` (int): Page number (default: 1)
- `pageSize` (int): Items per page (default: 10)
- `fields` (string): Comma-separated list of fields to return
- `orderBy` (string): Field to sort by

Response includes `X-Pagination` header with metadata.

### Authentication

The API uses JWT Bearer token authentication:

1. Register user via `/api/authentication`
2. Login via `/api/authentication/login` to get access and refresh tokens
3. Include `Authorization: Bearer {access_token}` header in protected requests
4. Refresh tokens via `/api/token/refresh` when access token expires

### Rate Limiting

- **Limit**: 15 requests per 30 minutes per IP address
- **Headers**: Rate limit information included in response headers

## Troubleshooting
- "Unable to cast object of type 'System.Net.Http.Headers.MediaTypeHeaderValue'...": ensure you use `Microsoft.Net.Http.Headers.MediaTypeHeaderValue` consistently when referencing media types in link utilities. Import correct namespace.
- `.vs` files causing git issues: add `.vs/` to `.gitignore` and remove from git index: `git rm -r --cached .vs`.
- Migrations assembly mismatch: set `MigrationsAssembly` in `AddDbContext` or use `--project/--startup-project` flags with `dotnet ef`.

## Contributing
Open issues or PRs. Keep changes small and targeted.

## License
MIT
