using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Timer.Api.Availability
{
    [Route("api/[controller]")]
    public class AvailabilityController: Controller
    {
        private readonly IMediator _mediator;

        public AvailabilityController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("")]
        public Task Set(Set.Request request, CancellationToken ct) =>
            _mediator.Send(request, ct);
    }
}