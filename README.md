# Bookmark Manager Backend

A .NET 8 Bookmark Manager backend solution with Clean Architecture, CQRS pattern using MediatR, and Entity Framework Core.

## 🏗️ Architecture

This solution follows Clean Architecture principles with the following layers:

- **BookmarkManager.Api** - Web API layer with RESTful endpoints
- **BookmarkManager.Application** - Application layer with CQRS (Commands/Queries), DTOs, and business logic
- **BookmarkManager.Domain** - Domain layer with entities and interfaces
- **BookmarkManager.Infrastructure** - Infrastructure layer with EF Core, repositories, and external services
- **BookmarkManager.Migration** - Database migration scripts (private)

## 🚀 Quick Start

### Prerequisites

- .NET 8 SDK
- SQL Server (LocalDB, Express, or full version)
- Visual Studio 2022 or VS Code

### Setup Instructions

1. **Clone the repository**
   ```bash
   git clone <your-repo-url>
   cd BookmarkManager.BE
   ```

2. **Configure Database Connection (User Secrets)**
   
   **For API Project:**
   ```bash
   cd BookmarkManager.Api
   dotnet user-secrets init
   dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Server=YOUR_SERVER;Database=YOUR_DATABASE;Trusted_Connection=true;TrustServerCertificate=true;MultipleActiveResultSets=true"
   cd ..
   ```

   **For Migration Project:**
   ```bash
   cd BookmarkManager.Migration
   dotnet user-secrets init
   dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Server=YOUR_SERVER;Database=YOUR_DATABASE;Trusted_Connection=true;TrustServerCertificate=true;MultipleActiveResultSets=true"
   cd ..
   ```

3. **Run Database Migrations**
   ```bash
   cd BookmarkManager.Migration
   dotnet run
   cd ..
   ```

4. **Run the API**
   ```bash
   dotnet run --project BookmarkManager.Api
   ```

5. **Test the API**
   
   The API will be available at `https://localhost:7001` (or the port shown in the console)
   
   **Available Endpoints:**
   - `GET /api/collections` - Get all collections
   - `POST /api/collections` - Create a new collection
   - `GET /api/bookmarks/all` - Get all bookmarks
   - `POST /api/bookmarks` - Create a new bookmark

## 🔒 Security Considerations

### What's Protected

This repository is configured to protect sensitive information:

1. **Database Connection Strings** - Not committed to the repository; use user secrets
2. **Migration Scripts** - Excluded from public repository (contains database structure)
3. **Environment-specific Configurations** - Development/Production settings are ignored

### Files Excluded from Repository

- `appsettings.Development.json`
- `appsettings.Production.json`
- `appsettings.Local.json`
- `Migration/` directory (contains database scripts)
- `bin/` and `obj/` directories
- User-specific files (`.user`, `.suo`)
- `.secret.json` (user secrets)

### For Contributors

When setting up the project:

1. **Never commit** your local secrets or environment-specific config files
2. **Use user secrets** for all sensitive values
3. **Keep migration scripts private** - they contain sensitive database structure information

## 📁 Project Structure

```
BookmarkManager.BE/
├── BookmarkManager.Api/                 # Web API layer
│   ├── Controllers/                     # API controllers
│   ├── Configuration/                   # Startup configuration
│   └── appsettings.json                 # Default config (empty values)
├── BookmarkManager.Application/         # Application layer
│   ├── Commands/                        # CQRS Commands
│   ├── Queries/                         # CQRS Queries
│   ├── Dto/                            # Data Transfer Objects
│   └── Extensions/                      # Service extensions
├── BookmarkManager.Domain/              # Domain layer
│   ├── Entities/                        # Domain entities
│   └── Interfaces/                      # Repository interfaces
├── BookmarkManager.Infrastructure/      # Infrastructure layer
│   ├── Database/                        # EF Core configuration
│   │   ├── EntityTypeConfiguration/     # Entity configurations
│   │   └── Repository/                  # Repository implementations
│   └── ServiceCollectionExtension.cs    # DI configuration
├── BookmarkManager.Migration/           # Database migrations (PRIVATE)
│   ├── Migration/                       # SQL scripts (excluded from repo)
│   └── appsettings.json                 # Default config (empty values)
└── README.md                           # This file
```

## 🛠️ Development

### Adding New Features

1. **Domain Layer** - Add entities and interfaces
2. **Infrastructure Layer** - Implement repositories and configurations
3. **Application Layer** - Add commands/queries and DTOs
4. **API Layer** - Add controllers and endpoints

### Database Changes

1. **Create Migration Scripts** - Add SQL scripts to `BookmarkManager.Migration/Migration/YYYY/M/`
2. **Update Entity Configurations** - Modify configurations in Infrastructure layer
3. **Run Migrations** - Execute migration project to apply changes

### Testing

```bash
# Build the solution
dotnet build

# Run tests (when added)
dotnet test
```

## 📝 API Documentation

### Collections

**Get All Collections**
```http
GET /api/collections
```

**Create Collection**
```http
POST /api/collections
Content-Type: application/json

{
  "name": "My Collection",
  "icon": "📚",
  "isFav": false
}
```

### Bookmarks

**Get All Bookmarks**
```http
GET /api/bookmarks/all
```

**Create Bookmark**
```http
POST /api/bookmarks
Content-Type: application/json

{
  "collectionId": 1,
  "name": "Google",
  "url": "https://www.google.com",
  "icon": "🔍",
  "isFav": false
}
```

## 🤝 Contributing

1. Fork the repository
2. Create a feature branch
3. Make your changes
4. Ensure all tests pass
5. Submit a pull request

## 📄 License

This project is licensed under the MIT License - see the LICENSE file for details.

## ⚠️ Important Notes

- **Migration scripts are private** and not included in the public repository
- **Connection strings must be configured locally** using user secrets
- **Never commit sensitive information** like database credentials
- **Use environment variables** for production deployments

## 🆘 Troubleshooting

### Common Issues

1. **Connection String Errors**
   - Ensure user secrets are set up and have correct connection string
   - Verify SQL Server is running and accessible

2. **Migration Errors**
   - Check that migration scripts are in the correct directory
   - Ensure database exists and is accessible

3. **Build Errors**
   - Run `dotnet restore` to restore packages
   - Ensure .NET 8 SDK is installed

### Getting Help

If you encounter issues:
1. Check the console output for error messages
2. Verify your connection strings are correct
3. Ensure all prerequisites are installed
4. Create an issue in the repository 