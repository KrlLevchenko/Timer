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
        }

        public bool Alive { get; set; }
        public Guid Id { get; }
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