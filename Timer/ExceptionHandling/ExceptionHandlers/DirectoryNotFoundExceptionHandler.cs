using System.Threading.Tasks;
using Pushinator.Web.Core.ExceptionHandling;
using Timer.ExceptionHandling.Exceptions;

namespace Timer.ExceptionHandling.ExceptionHandlers
{
    public class DirectoryNotFoundExceptionHandler: ExceptionHandlerBase<DirectoryNotFoundException>
    {
        protected override Task<ExceptionHandleResult> HandleInternal(DirectoryNotFoundException exception) => Task.FromResult(new ExceptionHandleResult(404, exception.Message));
    }
}