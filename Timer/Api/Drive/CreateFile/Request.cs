using MediatR;
using Microsoft.AspNetCore.Mvc;
using Timer.Drive;

namespace Timer.Api.Drive.CreateFile
{
    public class Request: IRequest, IDriveRequest
    {
        [FromRoute]
        public string FolderId { get; set; }
        
        [FromRoute]
        public string FileId { get; set; }
    }
}