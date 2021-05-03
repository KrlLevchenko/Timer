using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Timer.App
{
    public class AppContainer
    {
        public AppContainer()
        {
            Id = Guid.NewGuid();
            AppState = AppState.Healthy;
        }

        public AppState AppState { get; set; }
        public Guid Id { get; }
    }

    public enum AppState
    {
        Healthy = 1,
        Degraded = 2,
        Unhealthy = 3
    }

    public class AppContainerHealthCheck : IHealthCheck
    {
        private readonly AppContainer _container;

        public AppContainerHealthCheck(AppContainer container)
        {
            _container = container;
        }
        
        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = new CancellationToken())
        {
            switch (_container.AppState)
            {
                case AppState.Healthy:
                    return Task.FromResult(HealthCheckResult.Healthy());
                case AppState.Degraded:
                    return Task.FromResult(HealthCheckResult.Degraded());
                case AppState.Unhealthy:
                    return Task.FromResult(HealthCheckResult.Unhealthy());
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}