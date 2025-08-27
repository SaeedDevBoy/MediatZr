using System;
using System.Threading;
using System.Threading.Tasks;

namespace MediatZR.Abstractions;

// Abstractions/IPipelineBehavior.cs
public interface IPipelineBehavior<TRequest, TResponse>
{
    Task<TResponse> Handle(
        TRequest request,
        CancellationToken cancellationToken,
        Func<Task<TResponse>> next);
}
