using MediatR;
using Timer.Drive;

namespace Timer.Api.Drive.GetFileList
{
    public class Request: IRequest<Response>, IDriveRequest
    {
    }
}