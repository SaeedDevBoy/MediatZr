
using MediatZR.Abstractions;
using Test.Commands;
using Test.Data;
using Test.Extentions;

namespace Test.Handlers;

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Guid>
{
    private readonly EShopDbContext context;
    public CreateProductCommandHandler(EShopDbContext context)
    {
        this.context = context;
    }
    public async Task<Guid> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var product = request.ToProduct();
        context.Products.Add(product);
        await context.SaveChangesAsync(cancellationToken);
        return product.Id;
    }
}
