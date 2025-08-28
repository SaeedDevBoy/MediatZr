

# MediatZR

[![NuGet Version](https://img.shields.io/nuget/v/MediatZR.svg?style=for-the-badge&logo=nuget)](https://www.nuget.org/packages/MediatZR/)
[![NuGet Downloads](https://img.shields.io/nuget/dt/MediatZR.svg?style=for-the-badge&color=blue)](https://www.nuget.org/packages/MediatZR/)


MediatZR is a lightweight **CQRS and Mediator** library for .NET, built with simplicity and flexibility in mind.  
It allows you to organize your application logic using **Commands, Queries, and Handlers**, with support for **Pipeline Behaviors** (logging, validation, etc.).



## 🚀 Installation

You can install the package via NuGet:


```bash
  dotnet add package MediatZR
```
Or via the NuGet Package Manager in Visual Studio:


```bash
  PM> Install-Package MediatZR

```
## 📦 Getting Started
1. Define a Request (Command or Query)

```csharp
using MediatZR.Abstractions;

public record UserCommand(string Name) : IRequest<bool>;
```
2. Implement a Handler
```csharp
using MediatZR.Abstractions;

public class UserCommandHandler : IRequestHandler<UserCommand, bool>
{
    public async Task<bool> Handle(UserCommand request, CancellationToken cancellationToken)
    {
        Console.WriteLine($"User created: {request.Name}");
        return true;
    }
}

```
3. Register your mediator library in your webapi project:
```csharp
builder.Services.AddScoped<Mediator>();
```
- If your CQRS implementations are only inside the Web API project:
```csharp
builder.Services.AddCqrsHandlers(typeof(Program).Assembly);
``` 
- If you have multiple class libraries (e.g., Application Layer + Web API):
```csharp
builder.Services.AddCqrsHandlers(typeof(Program).Assembly, typeof(UserCommandHandler).Assembly);
```
4. Use Mediator in Your Code
For example in your webapi you can use like this:
```csharp
// UserController.cs
using MediatZR.Mediator;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly Mediator _mediator;

    public UserController(Mediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("create")]
    public async Task<IActionResult> Create([FromBody] UserCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(result);
    }
}
```
## ⚡ Pipeline Behavior

Pipeline Behaviors let you run logic **before and after** a request handler executes (e.g., logging, validation, performance tracking).

Example: Logging Behavior 

- First, create your Behavior like below :

```csharp
public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    public async Task<TResponse> Handle(
        TRequest request,
        CancellationToken cancellationToken,
        RequestHandlerDelegate<TResponse> next)
    {
        Console.WriteLine($"[LOG] Handling {typeof(TRequest).Name}");
        var response = await next();
        Console.WriteLine($"[LOG] Finished {typeof(TRequest).Name}");
        return response;
    }
}
```
- Second, Register your behaviour in your serveces in program.cs :
```csharp
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
```

## License

This project is licensed under the [MIT License](./LICENSE).


## Author  

[![GitHub](https://img.shields.io/badge/-GitHub-181717?style=flat&logo=github&logoColor=white)](https://github.com/SaeedDevBoy)
[![LinkedIn](https://img.shields.io/badge/-LinkedIn-0A66C2?style=flat&logo=linkedin&logoColor=white)](https://www.linkedin.com/in/saeed-zarei-a2a7b5a1/)



