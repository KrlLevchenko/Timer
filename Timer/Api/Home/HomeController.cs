using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Timer.Api.Home
{
    public class HomeController: Controller
    {
        private readonly IMediator _mediator;

        public HomeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Route("")]
        public Task<GetTime.Response> GetTime(GetTime.Request request, CancellationToken ct) =>
            _mediator.Send(request, ct);
        
        [Route("api/prime/{number:int}")]
        public Task<GetPrime.Response> GetPrime(GetPrime.Request request, CancellationToken ct) =>
            _mediator.Send(request, ct);
    }
}