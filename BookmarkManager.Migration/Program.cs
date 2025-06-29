using DbUp;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace BookmarkManager.Migration
{
    class Program
    {
        static int Main(string[] args)
        {
            try
            {
                Console.WriteLine("Starting BookmarkManager Database Migration...");
                
                // Build configuration - look in the migration project directory
                var projectDirectory = Path.GetDirectoryName(typeof(Program).Assembly.Location);
                if (string.IsNullOrEmpty(projectDirectory))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Error: Could not determine project directory");
                    Console.ResetColor();
                    return -1;
                }
                
                var configuration = new ConfigurationBuilder()
                    .SetBasePath(projectDirectory)
                    .AddJsonFile("appsettings.json", optional: false)
                    .AddEnvironmentVariables()
                    .Build();

                // Get connection string from configuration
                var connectionString = configuration.GetConnectionString("DefaultConnection");
                if (string.IsNullOrEmpty(connectionString))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Error: Connection string 'DefaultConnection' not found in appsettings.json");
                    Console.ResetColor();
                    return -1;
                }

                // Get migration scripts path
                var scriptPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Migration", "2025", "6");
                
                Console.WriteLine($"Using connection string: {connectionString}");
                Console.WriteLine($"Scripts path: {scriptPath}");

                // Configure DbUp
                var upgrader = DeployChanges.To
                    .SqlDatabase(connectionString)
                    .WithScriptsFromFileSystem(scriptPath)
                    .LogToConsole()
                    .WithTransaction()
                    .Build();

                // Check if database is up to date
                var isUpToDate = upgrader.IsUpgradeRequired();
                if (!isUpToDate)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Database is already up to date.");
                    Console.ResetColor();
                    return 0;
                }

                // Perform the upgrade
                Console.WriteLine("Performing database upgrade...");
                var result = upgrader.PerformUpgrade();

                if (!result.Successful)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Migration failed: {result.Error}");
                    Console.ResetColor();
                    return -1;
                }

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Database migration completed successfully!");
                Console.ResetColor();
                return 0;
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Unexpected error during migration: {ex.Message}");
                Console.WriteLine($"Stack trace: {ex.StackTrace}");
                Console.ResetColor();
                return -1;
            }
        }
    }
} 