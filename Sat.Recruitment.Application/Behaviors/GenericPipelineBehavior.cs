using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Sat.Recruitment.Application.Behaviors
{
    public class GenericPipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {            
            var response = await next();         
            return response;
        }
    }
}
