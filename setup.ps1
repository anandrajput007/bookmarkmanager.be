#!/usr/bin/env pwsh
<#
.SYNOPSIS
    Setup script for Bookmark Manager Backend

.DESCRIPTION
    This script helps set up the Bookmark Manager Backend project by:
    1. Creating appsettings.json files from templates
    2. Prompting for database connection details
    3. Building the solution
    4. Running initial setup

.PARAMETER Server
    SQL Server instance name (e.g., "localhost" or "AR_SYSTEM\SQLEXPRESS")

.PARAMETER Database
    Database name (default: "BMM")

.PARAMETER UseWindowsAuth
    Use Windows Authentication (default: true)

.PARAMETER SkipMigrations
    Skip running database migrations

.EXAMPLE
    .\setup.ps1 -Server "localhost" -Database "BookmarkManager"

.EXAMPLE
    .\setup.ps1 -Server "AR_SYSTEM\SQLEXPRESS" -Database "BMM" -UseWindowsAuth
#>

param(
    [Parameter(Mandatory = $false)]
    [string]$Server = "localhost",
    
    [Parameter(Mandatory = $false)]
    [string]$Database = "BMM",
    
    [Parameter(Mandatory = $false)]
    [bool]$UseWindowsAuth = $true,
    
    [Parameter(Mandatory = $false)]
    [bool]$SkipMigrations = $false
)

# Set error action preference
$ErrorActionPreference = "Stop"

Write-Host "🚀 Setting up Bookmark Manager Backend..." -ForegroundColor Green

# Function to create connection string
function Create-ConnectionString {
    param(
        [string]$Server,
        [string]$Database,
        [bool]$UseWindowsAuth
    )
    
    if ($UseWindowsAuth) {
        return "Server=$Server;Database=$Database;Trusted_Connection=true;TrustServerCertificate=true;MultipleActiveResultSets=true"
    } else {
        Write-Host "Enter SQL Server username:" -ForegroundColor Yellow
        $username = Read-Host
        Write-Host "Enter SQL Server password:" -ForegroundColor Yellow
        $password = Read-Host -AsSecureString
        $BSTR = [System.Runtime.InteropServices.Marshal]::SecureStringToBSTR($password)
        $plainPassword = [System.Runtime.InteropServices.Marshal]::PtrToStringAuto($BSTR)
        return "Server=$Server;Database=$Database;User Id=$username;Password=$plainPassword;TrustServerCertificate=true;MultipleActiveResultSets=true"
    }
}

# Function to create appsettings.json from template
function Create-AppSettings {
    param(
        [string]$TemplatePath,
        [string]$TargetPath,
        [string]$ConnectionString
    )
    
    if (Test-Path $TargetPath) {
        Write-Host "⚠️  $TargetPath already exists. Skipping..." -ForegroundColor Yellow
        return
    }
    
    if (-not (Test-Path $TemplatePath)) {
        Write-Host "❌ Template file not found: $TemplatePath" -ForegroundColor Red
        Write-Host "   Please ensure the template file exists before running setup." -ForegroundColor Yellow
        return
    }
    
    try {
        $content = Get-Content $TemplatePath -Raw -ErrorAction Stop
        $content = $content -replace "YOUR_SERVER", $Server
        $content = $content -replace "YOUR_DATABASE", $Database
        $content = $content -replace "Server=YOUR_SERVER;Database=YOUR_DATABASE;Trusted_Connection=true;TrustServerCertificate=true;MultipleActiveResultSets=true", $ConnectionString
        
        Set-Content -Path $TargetPath -Value $content -Encoding UTF8 -ErrorAction Stop
        Write-Host "✅ Created $TargetPath" -ForegroundColor Green
    } catch {
        Write-Host ("❌ Failed to create " + $TargetPath + ": " + $_.Exception.Message) -ForegroundColor Red
    }
}

try {
    # Create connection string
    $connectionString = Create-ConnectionString -Server $Server -Database $Database -UseWindowsAuth $UseWindowsAuth
    
    Write-Host "📝 Creating configuration files..." -ForegroundColor Cyan
    
    # Create appsettings.json for API project
    Create-AppSettings `
        -TemplatePath "BookmarkManager.Api\appsettings.template.json" `
        -TargetPath "BookmarkManager.Api\appsettings.json" `
        -ConnectionString $connectionString
    
    # Create appsettings.json for Migration project
    Create-AppSettings `
        -TemplatePath "BookmarkManager.Migration\appsettings.template.json" `
        -TargetPath "BookmarkManager.Migration\appsettings.json" `
        -ConnectionString $connectionString
    
    Write-Host "🔨 Building solution..." -ForegroundColor Cyan
    dotnet build
    
    if ($LASTEXITCODE -ne 0) {
        Write-Host "❌ Build failed!" -ForegroundColor Red
        exit 1
    }
    
    Write-Host "✅ Build successful!" -ForegroundColor Green
    
    if (-not $SkipMigrations) {
        Write-Host "🗄️  Running database migrations..." -ForegroundColor Cyan
        Set-Location "BookmarkManager.Migration"
        dotnet run
        
        if ($LASTEXITCODE -ne 0) {
            Write-Host "❌ Migration failed!" -ForegroundColor Red
            exit 1
        }
        
        Set-Location ".."
        Write-Host "✅ Migrations completed!" -ForegroundColor Green
    }
    
    Write-Host ""
    Write-Host "🎉 Setup completed successfully!" -ForegroundColor Green
    Write-Host ""
    Write-Host "Next steps:" -ForegroundColor Yellow
    Write-Host "1. Run the API: dotnet run --project BookmarkManager.Api" -ForegroundColor White
    Write-Host "2. Test endpoints at: https://localhost:7001" -ForegroundColor White
    Write-Host "3. Check README.md for API documentation" -ForegroundColor White
    Write-Host ""
    Write-Host "Configuration:" -ForegroundColor Yellow
    Write-Host "- Server: $Server" -ForegroundColor White
    Write-Host "- Database: $Database" -ForegroundColor White
    Write-Host "- Authentication: $(if ($UseWindowsAuth) { 'Windows' } else { 'SQL Server' })" -ForegroundColor White
    
} catch {
    Write-Host "❌ Setup failed: $($_.Exception.Message)" -ForegroundColor Red
    exit 1
} 