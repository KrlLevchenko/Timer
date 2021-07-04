using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Timer.App;

namespace Timer.Api.Drive.DeleteFile
{
    public class Handler: AsyncRequestHandler<Request>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly StorageOptions _storageOptions;

        public Handler(StorageOptions storageOptions)
        {
            _storageOptions = storageOptions;
        }
        
        protected override async Task Handle(Request request, CancellationToken ct)
        {
            File.Delete(Path.Combine(_storageOptions.StorageFolder, request.FileId));
        }
    }
}