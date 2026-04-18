# JWT Authentication API — ASP.NET Core

A REST API built with ASP.NET Core and .NET 8 that covers JWT authentication, CRUD operations, and database integration using Entity Framework Core.

Built as part of a 30-day .NET learning challenge to practice real backend development from scratch.

---

## What This Project Covers

- JWT token-based authentication (register, login, protected routes)
- REST API endpoints with full CRUD operations
- Entity Framework Core with SQL Server
- Clean project structure with Controllers, Services, and Models
- Middleware setup and dependency injection

---

## Project Structure

```
JWT_ApiProject/
├── Controllers/       # API endpoints and route handling
├── Models/            # Data models and request/response objects
├── Services/          # Business logic layer
├── Database/          # DbContext and database configuration
├── Migrations/        # EF Core database migrations
├── Program.cs         # App startup, middleware, and DI setup
└── appsettings.json   # Configuration and JWT settings
```

---

## Tech Stack

- .NET 8
- ASP.NET Core Web API
- Entity Framework Core
- SQL Server
- JWT Bearer Authentication

---

## Getting Started

### Prerequisites

- Visual Studio 2022 or later
- .NET 8 SDK
- SQL Server

### Setup

1. Clone the repository
```bash
git clone https://github.com/Ahsan-Malik-0/JWT_ApiProject.git
```

2. Open the solution in Visual Studio

3. Update the connection string in `appsettings.json` with your SQL Server details
```json
"ConnectionStrings": {
  "DefaultConnection": "Server=YOUR_SERVER;Database=YOUR_DB;Trusted_Connection=True;"
}
```

4. Update the JWT settings in `appsettings.json`
```json
"Jwt": {
  "Key": "YOUR_SECRET_KEY",
  "Issuer": "YOUR_ISSUER"
}
```

5. Run database migrations
```bash
dotnet ef database update
```

6. Run the project
```bash
dotnet run
```

7. Open Swagger UI at `https://localhost:{port}/swagger` to test the endpoints

---

## API Endpoints

### Auth
| Method | Endpoint | Description |
|--------|----------|-------------|
| POST | `/api/auth/register` | Register a new user |
| POST | `/api/auth/login` | Login and get JWT token |

### Protected Routes
| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/api/...` | Requires Bearer token in header |

> Add `Authorization: Bearer {your_token}` in the request header to access protected endpoints.

---

## Author

**Ahsan Malik**
CS Student at BIIT, Rawalpindi
Learning .NET Core in public — one day at a time.

[GitHub](https://github.com/Ahsan-Malik-0) · [LinkedIn](https://www.linkedin.com/in/)
