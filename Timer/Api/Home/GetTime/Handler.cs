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
        private readonly ValuesContainer _valuesContainer;

        public Handler(AppContainer appContainer, ValuesContainer valuesContainer)
        {
            _appContainer = appContainer;
            _valuesContainer = valuesContainer;
        }
        
        public Task<Response> Handle(Request request, CancellationToken cancellationToken)
        {
            return Task.FromResult(new Response
            {
                Now = DateTime.UtcNow,
                ContainerId = _appContainer.Id,
                SecretValue = _valuesContainer.SecretValue,
                NotSecretValue = _valuesContainer.NotSecretValue
            });
        }
    }
}