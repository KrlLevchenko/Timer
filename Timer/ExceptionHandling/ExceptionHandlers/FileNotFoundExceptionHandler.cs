using System.Threading.Tasks;
using Pushinator.Web.Core.ExceptionHandling;
using Timer.ExceptionHandling.Exceptions;

namespace Timer.ExceptionHandling.ExceptionHandlers
{
    public class FileNotFoundExceptionHandler: ExceptionHandlerBase<FileNotFoundException>
    {
        protected override Task<ExceptionHandleResult> HandleInternal(FileNotFoundException exception) => Task.FromResult(new ExceptionHandleResult(404, exception.Message));
    }
}