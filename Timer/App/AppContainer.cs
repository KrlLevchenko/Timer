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
            Alive = true;
            Created = DateTime.UtcNow;
        }

        public bool Alive { get; set; }
        public Guid Id { get; }
        public DateTime Created { get;  }
    }

    public class ReadinessCheck : IHealthCheck
    {
        private readonly AppContainer _container;

        public ReadinessCheck(AppContainer container)
        {
            _container = container;
        }
        
        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = new())
        {
            var lifetime = DateTime.UtcNow - _container.Created;
            var ready = lifetime > TimeSpan.FromMinutes(1);
            return Task.FromResult(ready ? HealthCheckResult.Healthy() : HealthCheckResult.Unhealthy());
        }

    }


    public class LivenessCheck : IHealthCheck
    {
        private readonly AppContainer _container;

        public LivenessCheck(AppContainer container)
        {
            _container = container;
        }
        
        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = new())
        {
            return Task.FromResult(_container.Alive ? HealthCheckResult.Healthy() : HealthCheckResult.Unhealthy());
        }
    }
}