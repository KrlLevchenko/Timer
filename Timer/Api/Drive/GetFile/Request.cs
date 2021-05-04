using MediatR;
using Microsoft.AspNetCore.Mvc;
using Timer.Drive;

namespace Timer.Api.Drive.GetFile
{
    public class Request: IRequest<Response>, IDriveRequest
    {
        [FromRoute]
        public string FileId { get; set; }
    }
}