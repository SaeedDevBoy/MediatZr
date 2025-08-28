
using MediatZR.Abstractions;
using MediatZR.Extensions;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Commands;
using Test.Handlers;

namespace Test;

public class RegistrationTests
{
    [Fact]
    public void AddCqrsHandlers_Should_Register_All_Handlers()
    {
        // Arrange
        var services = new ServiceCollection();
        services.AddCqrsHandlers(typeof(TestCommandHandler).Assembly);

        var provider = services.BuildServiceProvider();

        // Act
        var handler = provider.GetService<IRequestHandler<TestCommand, string>>();

        // Assert
        Assert.NotNull(handler);
        Assert.IsType<TestCommandHandler>(handler);
    }
}
