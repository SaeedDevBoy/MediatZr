
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


public class PipelineBehaviorTests
{
    [Fact]
    public async Task Mediator_Should_Execute_Pipeline_Behavior()
    {
        // Arrange
        var loggingBehavior = new TestLoggingBehavior<TestCommand, string>();

        var services = new ServiceCollection();
        services.AddScoped<Mediator>();
        services.AddScoped<IRequestHandler<TestCommand, string>, TestCommandHandler>();
        //services.AddSingleton<IPipelineBehavior<TestCommand, string>>(loggingBehavior);
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(TestLoggingBehavior<,>));

        var provider = services.BuildServiceProvider();
        var mediator = provider.GetRequiredService<Mediator>();

        // Act
        await mediator.Send(new TestCommand("Hello"));

        // Assert
        Assert.Contains("Before", TestLoggingBehavior<TestCommand, string>.Logs);
        Assert.Contains("After", TestLoggingBehavior<TestCommand, string>.Logs);
    }
}
