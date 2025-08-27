using MediatZR.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Reflection;

namespace MediatZR.Extensions;
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCqrsHandlers(this IServiceCollection services, params Assembly[] assemblies)
    {
        var handlerInterfaceType = typeof(IRequestHandler<,>);

        var handlers = assemblies
            .SelectMany(a => a.GetTypes())
            .Where(t => !t.IsAbstract && !t.IsInterface)
            .SelectMany(t =>
                t.GetInterfaces()
                 .Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == handlerInterfaceType)
                 .Select(i => new { Service = i, Implementation = t })
            );

        foreach (var handler in handlers)
        {
            services.AddTransient(handler.Service, handler.Implementation);
        }

        return services;
    }
}