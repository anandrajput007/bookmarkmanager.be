@echo off
echo 🚀 Setting up Bookmark Manager Backend...
echo.

REM Check if .NET is installed
dotnet --version >nul 2>&1
if %errorlevel% neq 0 (
    echo ❌ .NET SDK is not installed. Please install .NET 8 SDK first.
    pause
    exit /b 1
)

echo ✅ .NET SDK found
echo.

REM Create appsettings.json files if they don't exist
if not exist "BookmarkManager.Api\appsettings.template.json" (
    echo ❌ Template file not found: BookmarkManager.Api\appsettings.template.json
    echo Please ensure the template file exists before running setup.
    pause
    exit /b 1
)

if not exist "BookmarkManager.Migration\appsettings.template.json" (
    echo ❌ Template file not found: BookmarkManager.Migration\appsettings.template.json
    echo Please ensure the template file exists before running setup.
    pause
    exit /b 1
)

if not exist "BookmarkManager.Api\appsettings.json" (
    echo 📝 Creating API appsettings.json from template...
    copy "BookmarkManager.Api\appsettings.template.json" "BookmarkManager.Api\appsettings.json"
    if %errorlevel% neq 0 (
        echo ❌ Failed to create BookmarkManager.Api\appsettings.json
        pause
        exit /b 1
    )
    echo ⚠️  Please edit BookmarkManager.Api\appsettings.json with your database connection string
) else (
    echo ✅ BookmarkManager.Api\appsettings.json already exists
)

if not exist "BookmarkManager.Migration\appsettings.json" (
    echo 📝 Creating Migration appsettings.json from template...
    copy "BookmarkManager.Migration\appsettings.template.json" "BookmarkManager.Migration\appsettings.json"
    if %errorlevel% neq 0 (
        echo ❌ Failed to create BookmarkManager.Migration\appsettings.json
        pause
        exit /b 1
    )
    echo ⚠️  Please edit BookmarkManager.Migration\appsettings.json with your database connection string
) else (
    echo ✅ BookmarkManager.Migration\appsettings.json already exists
)

echo.
echo 🔨 Building solution...
dotnet build

if %errorlevel% neq 0 (
    echo ❌ Build failed!
    pause
    exit /b 1
)

echo ✅ Build successful!
echo.
echo 🎉 Setup completed!
echo.
echo Next steps:
echo 1. Edit the appsettings.json files with your database connection strings
echo 2. Run migrations: cd BookmarkManager.Migration ^&^& dotnet run
echo 3. Run the API: dotnet run --project BookmarkManager.Api
echo 4. Test endpoints at: https://localhost:7001
echo.
echo For more detailed setup, run: .\setup.ps1
echo.
pause 