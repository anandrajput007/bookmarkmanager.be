# Quick Setup Guide

## 🚀 Fast Setup (Windows)

### Option 1: Automated Setup (Recommended)
```powershell
# Run the PowerShell setup script
.\setup.ps1 -Server "YOUR_SERVER" -Database "YOUR_DATABASE"
```

### Option 2: Manual Setup
```batch
# Run the batch file
setup.bat
```

### Option 3: Manual Steps
1. **Copy template files:**
   ```bash
   copy BookmarkManager.Api\appsettings.template.json BookmarkManager.Api\appsettings.json
   copy BookmarkManager.Migration\appsettings.template.json BookmarkManager.Migration\appsettings.json
   ```

2. **Edit connection strings** in both `appsettings.json` files

3. **Build and run:**
   ```bash
   dotnet build
   cd BookmarkManager.Migration && dotnet run
   cd .. && dotnet run --project BookmarkManager.Api
   ```

## 🔒 Security Features

✅ **Protected from public repository:**
- Database connection strings
- Migration scripts (database structure)
- Environment-specific configurations
- User-specific files

✅ **Template files provided:**
- `appsettings.template.json` files show the structure
- No sensitive information in templates

✅ **Git configuration:**
- `.gitignore` excludes sensitive files
- `.gitattributes` ensures proper file handling

## 📋 What's Included

- **Clean Architecture** with 5 layers
- **CQRS pattern** with MediatR
- **Entity Framework Core** with SQL Server
- **RESTful API** with proper endpoints
- **Database migrations** (private)
- **Setup scripts** for easy configuration
- **Comprehensive documentation**

## 🎯 Next Steps

1. **Configure your database connection**
2. **Run the setup script**
3. **Test the API endpoints**
4. **Start developing!**

## 📚 Full Documentation

See `README.md` for complete documentation including:
- Architecture overview
- API documentation
- Development guidelines
- Troubleshooting 