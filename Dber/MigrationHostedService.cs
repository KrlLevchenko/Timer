using System.Threading;
using System.Threading.Tasks;
using FluentMigrator.Runner;
using Microsoft.Extensions.Hosting;

namespace Dber
{
    public class MigrationHostedService:IHostedService
    {
        private readonly IMigrationRunner _migrationRunner;
        private readonly IHostApplicationLifetime _applicationLifetime;

        public MigrationHostedService(IMigrationRunner migrationRunner, IHostApplicationLifetime applicationLifetime)
        {
            _migrationRunner = migrationRunner;
            _applicationLifetime = applicationLifetime;
        }
        
        public Task StartAsync(CancellationToken cancellationToken)
        {
            _applicationLifetime.ApplicationStarted.Register(() =>
            {
                _migrationRunner.MigrateUp();
                _applicationLifetime.StopApplication();
            });
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}