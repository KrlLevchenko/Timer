using System.Threading.Tasks;
using Pushinator.Web.Core.ExceptionHandling;
using Timer.ExceptionHandling.Exceptions;

namespace Timer.ExceptionHandling.ExceptionHandlers
{
    public class EntityNotFoundExceptionHandler: ExceptionHandlerBase<EntityNotFoundException>
    {
        protected override Task<ExceptionHandleResult> HandleInternal(EntityNotFoundException exception) => Task.FromResult(new ExceptionHandleResult(404, exception.Message));
    }
}