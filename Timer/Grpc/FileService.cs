using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using Timer.App;
using Timer.Proto.Files;

namespace Timer.Grpc
{
    public class FileService: Proto.Files.FileService.FileServiceBase
    {
        private readonly StorageOptions _storageOptions;

        public FileService(StorageOptions storageOptions)
        {
            _storageOptions = storageOptions;
        }
        
        public override Task<FileIds> GetFiles(Empty request, ServerCallContext context)
        {
            var ids =  Directory.GetFiles(_storageOptions.StorageFolder)
                .Select(Path.GetFileName)
                .ToArray();
            var fileIds = new FileIds();
            fileIds.Id.AddRange(ids);
            return Task.FromResult(fileIds);
        }
        
        
    }
}