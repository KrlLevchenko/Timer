using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

namespace TimerClient
{
    public class TimerClientService: IHostedService
    {
        private readonly IHostApplicationLifetime _applicationLifetime;
        private readonly ProgramExecutor _programExecutor;
        private readonly TimerApiClient _timerApiClient;

        public TimerClientService(IHostApplicationLifetime applicationLifetime, ProgramExecutor programExecutor, TimerApiClient timerApiClient)
        {
            _applicationLifetime = applicationLifetime;
            _programExecutor = programExecutor;
            _timerApiClient = timerApiClient;
        }
        
        public Task StartAsync(CancellationToken cancellationToken)
        {
            _applicationLifetime.ApplicationStarted.Register(() =>
            {
                var fileName = DateTime.UtcNow.ToString(@"YYYYMMddHHmmss.j\son");
                try
                {
                    _timerApiClient.CreateFile(fileName, "fdsff!!!!").GetAwaiter().GetResult();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    _applicationLifetime.StopApplication();
                }
              
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