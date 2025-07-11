# NewsPortal API
News portal with article and category management features.
## Technologies
- .NET 8.0
- ASP.NET Core
- Entity Framework Core
- MediatR
- FluentResults
- Docker & Docker Compose
## Project Structure
``` 
NewsPortal/
├── NewsPortal.Api/              # API layer (controllers, middleware)
├── NewsPortal.Application/      # Business logic (handlers, features)
│   ├── Features/
│   │   ├── ArticlesFeatures/    # Article-related features
│   │   └── CategoriesFeatures/  # Category-related features
│   └── Repositories/            # Repository interfaces
├── NewsPortal.Domain/           # Domain models and services
│   ├── Models/                  # Domain entities
│   └── Services/                # Domain services
├── NewsPortal.Infrastructure/   # Data layer (Entity Framework)
│   └── Context/                 # Database context
├── NewsPortal.Domain.Tests/     # Unit tests
├── NewsPortal.Application.Tests/# Unit tests
├── compose.yaml                 # Docker Compose configuration
├── Makefile                     # Task automation
└── README.md                    # Documentation
```
## Local Development
### Prerequisites
- .NET 8.0 SDK
### Setup Steps
1. **Clone the repository**
``` bash
   git clone <repository-url>
   cd NewsPortal
```
2. **Build the project**
``` bash
   make build
   # or
   dotnet build
```
3. **Run tests**
``` bash
   make test
   # or
   dotnet test
```
4. **Run the application**
``` bash
   make run
   # or
   dotnet run --project NewsPortal.Api
```
5. **The application will be available at:**
    - HTTP: `http://localhost:5076`
    - Swagger UI: `http://localhost:5076/swagger`
## Docker Deployment
### Prerequisites
- Docker
- Docker Compose
### Setup Steps
1. **Build Docker image**
``` bash
   make docker-build
   # or
   docker compose build
```
1. **Run the application**
``` bash
   make docker-run
   # or
   docker compose up -d
```
1. **The application will be available at:**
    - HTTP: `http://localhost:8080`
    - Swagger UI: `http://localhost:8080/swagger`
## API Endpoints
### Articles
| Method | Endpoint | Description |
| --- | --- | --- |
| GET | `/api/articles` | Get all articles |
| GET | `/api/articles/{id}` | Get article by ID |
| POST | `/api/articles` | Create new article |
| PUT | `/api/articles/{id}` | Update article |
| POST | `/api/articles/{id}/publish` | Publish article |
| GET | `/api/articles/stats` | Get articles statistics |
### Categories
| Method | Endpoint | Description |
| --- | --- | --- |
| GET | `/api/categories` | Get all categories |
| POST | `/api/categories` | Create new category |
## Sample curl Requests
### Get all articles
``` bash
curl -X GET "http://localhost:5076/api/articles" \
  -H "accept: application/json"
```
### Get articles by status
``` bash
curl -X GET "http://localhost:5076/api/articles?status=Published" \
  -H "accept: application/json"
```
### Create new article
``` bash
curl -X POST "http://localhost:5076/api/articles" \
  -H "accept: application/json" \
  -H "Content-Type: application/json" \
  -d '{
    "title": "New Article",
    "content": "Article content...",
    "author": "John Doe",
    "categoryId": "3fa85f64-5717-4562-b3fc-2c963f66afa6"
  }'
```
### Get article by ID
``` bash
curl -X GET "http://localhost:5076/api/articles/3fa85f64-5717-4562-b3fc-2c963f66afa6" \
  -H "accept: application/json"
```
### Update article
``` bash
curl -X PUT "http://localhost:5076/api/articles/3fa85f64-5717-4562-b3fc-2c963f66afa6" \
  -H "accept: application/json" \
  -H "Content-Type: application/json" \
  -d '{
    "title": "Updated Title",
    "content": "Updated content...",
    "author": "John Doe"
  }'
```
### Publish article
``` bash
curl -X POST "http://localhost:5076/api/articles/3fa85f64-5717-4562-b3fc-2c963f66afa6/publish" \
  -H "accept: application/json"
```
### Get article statistics
``` bash
curl -X GET "http://localhost:5076/api/articles/stats" \
  -H "accept: application/json"
```
### Get all categories
``` bash
curl -X GET "http://localhost:5076/api/categories" \
  -H "accept: application/json"
```
### Create new category
``` bash
curl -X POST "http://localhost:5076/api/categories" \
  -H "accept: application/json" \
  -H "Content-Type: application/json" \
  -d '{
    "name": "Category name"
  }'
```
## Available Makefile Commands
``` bash
make build          # Build the project
make test           # Run tests
make run            # Run the application
make docker-build   # Build Docker image
make docker-run     # Run with Docker
```
## Development Environment
### Swagger UI
After running the application, API documentation is available at:
- Local: `http://localhost:5076/swagger`
- Docker: `http://localhost:8080/swagger`

### Database
The application uses Entity Framework Core with InMemory Database.
### Logging
The application logs errors and operations. In development mode, logs are displayed in the console.
## Testing
``` bash
# Run all tests
make test

# Run tests with detailed output
dotnet test --verbosity detailed
```
