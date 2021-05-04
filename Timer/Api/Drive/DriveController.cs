using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Timer.Api.Drive
{
    [Route("api/[controller]")]
    public class DriveController
    {
        private readonly IMediator _mediator;

        public DriveController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        [HttpPost("{folderId}/{fileId}")]
        public Task CreateFile(CreateFile.Request request, CancellationToken ct) =>
            _mediator.Send(request, ct);
        
        [HttpGet("{folderId}/{fileId}")]
        public Task<GetFile.Response> GetFile(GetFile.Request request, CancellationToken ct) =>
            _mediator.Send(request, ct);

        [HttpGet("{folderId}")]
        public Task<GetFileList.Response> GetFolders(GetFileList.Request request, CancellationToken ct) =>
            _mediator.Send(request, ct);
        
        [HttpGet("")]
        public Task<GetFolders.Response> GetFolders(GetFolders.Request request, CancellationToken ct) =>
            _mediator.Send(request, ct);

    }
}