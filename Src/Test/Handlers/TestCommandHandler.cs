
using MediatZR.Abstractions;
using Test.Commands;

namespace Test.Handlers;

public class TestCommandHandler : IRequestHandler<TestCommand, string>
{
    public Task<string> Handle(TestCommand request, CancellationToken cancellationToken)
    {
        return Task.FromResult($"Handled: {request.Message}");
    }
}