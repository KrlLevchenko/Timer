using System.Threading.Tasks;
using Pushinator.Web.Core.ExceptionHandling;
using Timer.ExceptionHandling.Exceptions;

namespace Timer.ExceptionHandling.ExceptionHandlers
{
    public class ValidationFailedExceptionHandler: ExceptionHandlerBase<ValidationFailedException>
    {
        protected override Task<ExceptionHandleResult> HandleInternal(ValidationFailedException exception) => Task.FromResult(new ExceptionHandleResult(400, exception.Message));
    }
}