using System;
using System.Threading;
using System.Threading.Tasks;
using Grpc.Net.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Timer.Proto.Files;

namespace TimerClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables();
            var configuration = builder.Build();
            
            var cts = new CancellationTokenSource();
            await Host.CreateDefaultBuilder()
                .ConfigureServices((_, services) =>
                {
                    services.AddHttpClient<TimerApiClient>(httpClient =>
                        httpClient.BaseAddress = new Uri(configuration["DriveApiUrl"]));
                    services.AddHostedService<TimerClientService>();
                    services.AddScoped(_ =>
                    {
                        var channel = GrpcChannel.ForAddress("http://localhost:3000");
                        return new FileService.FileServiceClient(channel);
                    });
                })
                .RunConsoleAsync(cts.Token);
        }
    }
}