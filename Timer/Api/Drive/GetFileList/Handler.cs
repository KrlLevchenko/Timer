using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Timer.App;
using DirectoryNotFoundException = Timer.ExceptionHandling.Exceptions.DirectoryNotFoundException;

namespace Timer.Api.Drive.GetFileList
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

            return Task.FromResult(new Response
            {
                Files = Directory.GetFiles(directory)
                    .Select(Path.GetFileName)
                    .ToArray()
            });
        }
    }
}