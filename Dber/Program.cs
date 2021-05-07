using System;
using System.Threading;
using System.Threading.Tasks;
using FluentMigrator.Runner;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Dber
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables();
            var configuration = builder.Build();
            
            await Host.CreateDefaultBuilder()
                .ConfigureServices((_, services) =>
                {
                    services.AddFluentMigratorCore()
                        .ConfigureRunner(rb => rb
                            // Add SQLite support to FluentMigrator
                            .AddMySql5()
                            // Set the connection string
                            .WithGlobalConnectionString(configuration.GetConnectionString("Db"))
                            // Define the assembly containing the migrations
                            .ScanIn(typeof(Program).Assembly).For.Migrations())
                        // Enable logging to console in the FluentMigrator way
                        .AddLogging(lb => lb.AddFluentMigratorConsole());
                    services.AddHostedService<MigrationHostedService>();
                })
                .RunConsoleAsync();
        }
    }
}