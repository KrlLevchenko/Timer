using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace Timer.Api.Home.GetTime
{
    public class Handler: IRequestHandler<Request, Response>
    {
        public Task<Response> Handle(Request request, CancellationToken cancellationToken) =>
            Task.FromResult(new Response
            {
                Now = DateTime.UtcNow
            });
    }
}