using System.IO;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Timer.App;
using DirectoryNotFoundException = Timer.ExceptionHandling.Exceptions.DirectoryNotFoundException;
using FileNotFoundException = Timer.ExceptionHandling.Exceptions.FileNotFoundException;

namespace Timer.Api.Drive.GetFile
{
    public class Handler: IRequestHandler<Request, Response>
    {
        private readonly StorageOptions _storageOptions;

        public Handler(StorageOptions storageOptions)
        {
            _storageOptions = storageOptions;
        }
        
        public Task<Response> Handle(Request request, CancellationToken cancellationToken)
        {
            var directory = Path.Combine(_storageOptions.StorageFolder, request.FolderId);
            if (!Directory.Exists(directory))
            {
                throw new DirectoryNotFoundException(request.FolderId);
            }

            var filePath = Path.Combine(directory, request.FileId);
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException(request.FolderId, request.FileId);
            }
            
            return Task.FromResult(new Response
            {
                Content = ReadAllBytes(filePath)
            });
        }
        
        private static byte[] ReadAllBytes(string filePath)
        {
            using var fileStream = File.OpenRead(filePath);
            using var memoryStream = new MemoryStream();
            fileStream.CopyTo(memoryStream);
            return memoryStream.ToArray();
        }
    }
}