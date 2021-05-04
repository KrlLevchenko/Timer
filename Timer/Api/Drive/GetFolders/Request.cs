using MediatR;
using Timer.Drive;

namespace Timer.Api.Drive.GetFolders
{
    public class Request: IRequest<Response>, IDriveRequest
    {
    }
}