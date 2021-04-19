using System.Threading.Tasks;
using Pushinator.Web.Core.ExceptionHandling;
using Timer.ExceptionHandling.Exceptions;

namespace Timer.ExceptionHandling.ExceptionHandlers
{
    public class NotAvailableExceptionHandler: ExceptionHandlerBase<NotAvailableException>
    {
        protected override Task<ExceptionHandleResult> HandleInternal(NotAvailableException exception) => Task.FromResult(new ExceptionHandleResult(503, "Service not available now!"));
    }
}