using MediatZR.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MediatZR.Mediator;

public class Mediator
{
    private readonly IServiceProvider _serviceProvider;

    public Mediator(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default)
    {
        var handlerType = typeof(IRequestHandler<,>).MakeGenericType(request.GetType(), typeof(TResponse));
        dynamic handler = _serviceProvider.GetRequiredService(handlerType); // ✅ این متد حالا در دسترس است

        var pipelineBehaviors = _serviceProvider
            .GetServices(typeof(IPipelineBehavior<,>).MakeGenericType(request.GetType(), typeof(TResponse)))
            .Cast<dynamic>()
            .ToList();

        Func<Task<TResponse>> handlerDelegate = () => handler.Handle((dynamic)request, cancellationToken);

        foreach (var behavior in pipelineBehaviors.AsEnumerable().Reverse())
        {
            var next = handlerDelegate;
            handlerDelegate = () => behavior.Handle((dynamic)request, cancellationToken, next);
        }

        return await handlerDelegate();
    }
}