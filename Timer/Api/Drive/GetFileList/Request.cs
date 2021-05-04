using MediatR;
using Microsoft.AspNetCore.Mvc;
using Timer.Drive;

namespace Timer.Api.Drive.GetFileList
{
    public class Request: IRequest<Response>, IDriveRequest
    {
        [FromRoute]
        public string FolderId { get; set; }
    }
}