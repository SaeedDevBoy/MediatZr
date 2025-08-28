
using MediatZR.Abstractions;
using MediatZR.Mediator;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Behaviors;
using Test.Commands;
using Test.Handlers;


namespace Test;

public class ExceptionBehaviorTests
{
    [Fact]
    public async Task Mediator_Should_Execute_ExceptionBehavior_On_Failure()
    {
        // Arrange
        var exceptionBehavior = new ExceptionBehavior<TestCommand, string>();

        var services = new ServiceCollection();
        services.AddScoped<Mediator>();
        services.AddScoped<IRequestHandler<TestCommand, string>, FailingHandler>();
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ExceptionBehavior<,>));

        var provider = services.BuildServiceProvider();
        var mediator = provider.GetRequiredService<Mediator>();

        // Act & Assert
        await Assert.ThrowsAsync<InvalidOperationException>(() => mediator.Send(new TestCommand("Fail")));

        Assert.Contains("Handler failed!", ExceptionBehavior<TestCommand, string>.Errors);
    }
}