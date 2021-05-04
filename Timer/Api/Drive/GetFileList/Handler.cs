using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Timer.App;

namespace Timer.Api.Drive.GetFileList
{
    public class Handler: IRequestHandler<Request, Response>
    {
        private readonly StorageOptions _storageOptions;

        public Handler(StorageOptions storageOptions)
        {
            _storageOptions = storageOptions;
        }
        
        public Task<Response> Handle(Request request, CancellationToken cancellationToken) =>
            Task.FromResult(new Response
            {
                Files = Directory.GetFiles(_storageOptions.StorageFolder)
                    .Select(Path.GetFileName)
                    .ToArray()
            });
    }
}