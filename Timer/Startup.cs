using System.Collections.Generic;
using Grpc.Core;
using Grpc.Reflection;
using Grpc.Reflection.V1Alpha;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Pushinator.Web.AppStart;
using Timer.App;
using Timer.AppStart;
using Timer.Core;
using Timer.Drive;
using Timer.Proto.Files;

namespace Timer
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddLogging(configure => configure.AddConsole());
            services.AddSingleton<AppContainer>();
            
            services.AddSingleton(_ => new StorageOptions(Configuration["Storage"]));
            
            services.AddMediatR(typeof(Startup).Assembly);
            services.AddValidators(typeof(Startup));
            services.AddBehaviorsForRequest<IDriveRequest>(typeof(Startup));
            services.AddErrorHandling(typeof(Startup));

            services.AddControllers();
            services.AddHttpContextAccessor();

            services.AddHealthChecks()
                .AddCheck<LivenessCheck>("liveness", tags: new List<string> {"liveness"})
                .AddCheck<ReadinessCheck>("readiness", tags: new List<string> {"readiness"});

            services.AddSingleton(_ =>
                new ValuesContainer(Configuration["SecretValue"], Configuration["NotSecretValue"]));
           
            services.AddSingleton(sp =>
            {
                var reflectionServiceImpl = new ReflectionServiceImpl(FileService.Descriptor, ServerReflection.Descriptor);
                var server = new Server
                {
                    Services =
                    {
                        FileService.BindService(new Grpc.FileService(sp.GetService<StorageOptions>())),
                        ServerReflection.BindService(reflectionServiceImpl)
                    },
                    Ports = {new ServerPort("localhost", 3000, ServerCredentials.Insecure)},
                    
                };
                return server;
                
            });
            
            services.AddHostedService<GrpcHostedService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment()) app.UseDeveloperExceptionPage();

            app.UseRouting();
            app.UseExceptionHandlingMiddleware();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHealthChecks("/health/liveness", new HealthCheckOptions
                {
                    Predicate = _ => _.Tags.Contains("liveness")
                });
                endpoints.MapHealthChecks("/health/readiness", new HealthCheckOptions
                {
                    Predicate = _ => _.Tags.Contains("readiness")
                });
                endpoints.MapControllerRoute(
                    "default",
                    "{controller=Home}/{action=Index}/{id?}");
            });

        }
    }
}