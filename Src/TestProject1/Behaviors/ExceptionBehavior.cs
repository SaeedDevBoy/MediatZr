

using MediatZR.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Behaviors;

public class ExceptionBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
{
    public static List<string> Errors { get; } = new();

    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, Func<Task<TResponse>> next)
    {
        try
        {
            return await next();
        }
        catch (Exception ex)
        {
            Errors.Add(ex.Message);
            throw; // دوباره پرتاب می‌کنیم تا سیستم بفهمه
        }
    }
}