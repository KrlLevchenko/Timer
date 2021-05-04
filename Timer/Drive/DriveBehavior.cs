using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Timer.App;
using Timer.ExceptionHandling.Exceptions;

namespace Timer.Drive
{
    
    public class DriveBehavior<TResponse> : IPipelineBehavior<IDriveRequest, TResponse>
    {
        private readonly IServiceProvider _serviceProvider;
        public DriveBehavior(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        public async Task<TResponse> Handle(IDriveRequest request, CancellationToken cancellationToken,
            RequestHandlerDelegate<TResponse> next)
        {
            var storageOptions = _serviceProvider.GetRequiredService<StorageOptions>();
            Directory.CreateDirectory(storageOptions.StorageFolder);
            return await next();
        }
    }
}