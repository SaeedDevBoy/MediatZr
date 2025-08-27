

# MediatZR

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
-1. Define a Request (Command or Query)

```csharp
using CQRSLibrary.Abstractions;

public record UserCommand(string Name) : IRequest<bool>;
```
-2. Implement a Handler
```csharp
using CQRSLibrary.Abstractions;

public class UserCommandHandler : IRequestHandler<UserCommand, bool>
{
    public async Task<bool> Handle(UserCommand request, CancellationToken cancellationToken)
    {
        Console.WriteLine($"User created: {request.Name}");
        return true;
    }
}

```
-3 Register your mediator library in your webapi project:
```csharp
builder.Services.AddScoped<Mediator>();
```
- if your CQRS implementations was only in web api you can easily register it like this:
```csharp
builder.Services.AddCqrsHandlers(typeof(Program).Assembly);
``` 
- if your CQRS implementations was in more than one class library like WebApi and Application Layer and ... you can easily register only one class like handler query and others that inherited from IRequest or IRequestHandler like this:
```csharp
builder.Services.AddCqrsHandlers(typeof(Program).Assembly, typeof(UserCommandHandler).Assembly);
```
-4 Use Mediator in Your Code
For example in your webapi you can use like this:
```csharp
// UserController.cs
using CQRSLibrary.Mediator;
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
-4. Implement a Pipeline Behavior (Logging) : 
- if you want to have for example logging or doing something regular befor and after handle of yor CQRs you can use like this :
- first regiater your pipeline behavoiur in program.cs :
```csharp
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
```
- second use code like this :
```csharp
‍‍‍‍‍public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
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

## License

This project is licensed under the [MIT License](./LICENSE).


## Author

- [Saeed Zarei Github](https://www.github.com/octokatherine)
- [Saeed Zarei Linkedin](https://www.linkedin.com/in/saeed-zarei-a2a7b5a1/)

