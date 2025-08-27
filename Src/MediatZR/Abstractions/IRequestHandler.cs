using System.Threading;
using System.Threading.Tasks;

namespace MediatZR.Abstractions;

// Abstractions/IRequestHandler.cs
public interface IRequestHandler<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken);
}
