using MediatZR.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Behaviors;

public class TestLoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
{
    public static List<string> Logs { get; } = new();

    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, Func<Task<TResponse>> next)
    {
        Logs.Add("Before");
        var response = await next();
        Logs.Add("After");
        return response;
    }
}
