using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Timer.App;

namespace Timer.Api.Drive.CreateFile
{
    public class Handler: AsyncRequestHandler<Request>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly StorageOptions _storageOptions;

        public Handler(IHttpContextAccessor httpContextAccessor, StorageOptions storageOptions)
        {
            _httpContextAccessor = httpContextAccessor;
            _storageOptions = storageOptions;
        }
        
        protected override async Task Handle(Request request, CancellationToken ct)
        {
            if (_httpContextAccessor.HttpContext == null)
            {
                throw new Exception("Unexpected httpContext is empty");
            }

            using (var fileStream = File.Create(Path.Combine(_storageOptions.StorageFolder, request.FileId)))
            {
                await _httpContextAccessor.HttpContext.Request.Body.CopyToAsync(fileStream, ct);
            }
        }
    }
}