using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace Timer.Api.Availability.Get
{
    public class Handler: IRequestHandler<Request, Response>
    {
        private readonly AppContainer _appContainer;

        public Handler(AppContainer appContainer)
        {
            _appContainer = appContainer;
        }
        
        public Task<Response> Handle(Request request, CancellationToken cancellationToken) =>
            Task.FromResult(new Response
            {
                IsAvailable = _appContainer.IsAvailable
            });
    }
}