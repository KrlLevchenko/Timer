using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace Timer.Api.Home.GetPrime
{
    public class Handler: IRequestHandler<Request, Response>
    {
        public Task<Response> Handle(Request request, CancellationToken cancellationToken)
        {
            var sw = Stopwatch.StartNew();
            var result = Enumerable.Range(1, request.Number)
                .Where(IsPrime)
                .Last();
            return Task.FromResult(new Response
            {
                MaxPrime = result,
                Duration = sw.ElapsedMilliseconds
            });
        }

        private static bool IsPrime(int number)
        {
            for (var i = 2; i < number; i++)
            {
                if (number % i == 0)
                    return false;
            }

            return true;
        }
    }
}