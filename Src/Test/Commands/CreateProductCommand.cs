using MediatZR.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Commands;

public record CreateProductCommand(string Name, decimal Price) : IRequest<Guid>;
