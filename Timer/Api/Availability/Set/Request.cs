using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Timer.Api.Availability.Set
{
    public class Request: IRequest
    {
        [FromQuery] public int Value { get; set; }
        
    }
}