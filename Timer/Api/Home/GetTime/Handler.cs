using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Timer.App;
using Timer.ExceptionHandling.Exceptions;

namespace Timer.Api.Home.GetTime
{
    public class Handler: IRequestHandler<Request, Response>
    {
        private readonly AppContainer _appContainer;

        public Handler(AppContainer appContainer)
        {
            _appContainer = appContainer;
        }
        
        public Task<Response> Handle(Request request, CancellationToken cancellationToken)
        {
            return Task.FromResult(new Response
            {
                Now = DateTime.UtcNow,
                ContainerId = _appContainer.Id
            });
        }
    }
}