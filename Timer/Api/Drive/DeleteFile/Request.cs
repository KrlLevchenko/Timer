using MediatR;
using Microsoft.AspNetCore.Mvc;
using Timer.Drive;

namespace Timer.Api.Drive.DeleteFile
{
    public class Request: IRequest, IDriveRequest
    {
        [FromRoute]
        public string FileId { get; set; }
    }
}