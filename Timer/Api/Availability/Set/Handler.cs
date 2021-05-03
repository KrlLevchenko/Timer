using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Timer.App;

namespace Timer.Api.Availability.Set
{
    public class Handler: AsyncRequestHandler<Request>
    {
        private readonly AppContainer _appContainer;

        public Handler(AppContainer appContainer)
        {
            _appContainer = appContainer;
        }

        protected override Task Handle(Request request, CancellationToken cancellationToken)
        {
            _appContainer.Alive = request.Value;
            return Task.CompletedTask;
        }
    }
}