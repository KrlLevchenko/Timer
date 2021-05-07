using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Timer.Api.Home.GetPrime
{
    public class Request: IRequest<Response>
    {
        [FromRoute] public int Number { get; set; }
    }
}