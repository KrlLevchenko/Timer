using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Timer.Proto.Files;

namespace TimerClient
{
    public class TimerClientService: IHostedService
    {
        private readonly IHostApplicationLifetime _applicationLifetime;
        private readonly TimerApiClient _timerApiClient;
        private readonly FileService.FileServiceClient _fileServiceClient;

        public TimerClientService(IHostApplicationLifetime applicationLifetime, 
            TimerApiClient timerApiClient,
            FileService.FileServiceClient fileServiceClient)
        {
            _applicationLifetime = applicationLifetime;
            _timerApiClient = timerApiClient;
            _fileServiceClient = fileServiceClient;
        }
        
        public Task StartAsync(CancellationToken cancellationToken)
        {
            _applicationLifetime.ApplicationStarted.Register(() =>
            {
                var x = _fileServiceClient.GetFiles(new Empty());
                var fileName = DateTime.UtcNow.ToString(@"YYYYMMddHHmmss.j\son");
                try
                {
                    
                    _timerApiClient.CreateFile(fileName, "fdsff!!!!").GetAwaiter().GetResult();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    Environment.ExitCode = 1;
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