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
            var filePath = Path.Combine(_storageOptions.StorageFolder, request.FileId);
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException(request.FileId);
            }
            
            return Task.FromResult(new Response
            {
                Content = ReadAll(filePath)
            });
        }
        
        private static string ReadAll(string filePath)
        {
            using var streamReader = new StreamReader(filePath);
            return streamReader.ReadToEnd();
        }
    }
}