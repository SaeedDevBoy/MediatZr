using MediatZR.Abstractions;

namespace Test.Commands;

public record TestCommand(string Message) : IRequest<string>;