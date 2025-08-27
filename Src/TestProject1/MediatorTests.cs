
using MediatZR.Abstractions;
using MediatZR.Mediator;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Commands;
using Test.Handlers;

namespace Test;



public class MediatorTests
{
    [Fact]
    public async Task Mediator_Should_Call_Handler_And_Return_Result()
    {
        // Arrange
        var services = new ServiceCollection();
        services.AddScoped<Mediator>();
        services.AddScoped<IRequestHandler<TestCommand, string>, TestCommandHandler>();

        var provider = services.BuildServiceProvider();
        var mediator = provider.GetRequiredService<Mediator>();

        // Act
        var result = await mediator.Send(new TestCommand("Hello"));

        // Assert
        Assert.Equal("Handled: Hello", result);
    }
    [Fact]
    public async Task Mediator_Should_Handle_Parallel_Requests_Correctly()
    {
        // Arrange
        var services = new ServiceCollection();
        services.AddScoped<Mediator>();
        services.AddScoped<IRequestHandler<TestCommand, string>, TestCommandHandler>();

        var provider = services.BuildServiceProvider();
        var mediator = provider.GetRequiredService<Mediator>();

        // Act
        var tasks = Enumerable.Range(1, 100)
            .Select(i => mediator.Send(new TestCommand($"Msg {i}")));

        var results = await Task.WhenAll(tasks);

        // Assert
        Assert.Equal(100, results.Length);
        Assert.All(results, r => Assert.StartsWith("Handled:", r));
    }
    [Fact]
    public async Task Mediator_Should_Handle_10000_Requests_Fast()
    {
        // Arrange
        var services = new ServiceCollection();
        services.AddScoped<Mediator>();
        services.AddScoped<IRequestHandler<TestCommand, string>, TestCommandHandler>();

        var provider = services.BuildServiceProvider();
        var mediator = provider.GetRequiredService<Mediator>();

        var sw = Stopwatch.StartNew();

        // Act
        for (int i = 0; i < 10_000; i++)
        {
            await mediator.Send(new TestCommand($"Msg {i}"));
        }

        sw.Stop();

        // Assert
        Assert.True(sw.ElapsedMilliseconds < 2000, $"Too slow! Took {sw.ElapsedMilliseconds} ms");
    }
}
