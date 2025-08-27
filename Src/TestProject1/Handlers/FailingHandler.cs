
using MediatZR.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Commands;

namespace Test.Handlers;

public class FailingHandler : IRequestHandler<TestCommand, string>
{
    public Task<string> Handle(TestCommand request, CancellationToken cancellationToken)
    {
        throw new InvalidOperationException("Handler failed!");
    }
}
