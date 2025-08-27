
using MediatZR.Extensions;
using MediatZR.Mediator;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Commands;
using Test.Data;
using Test.Handlers;

namespace Test;

public class InMemoryTest
{
    private readonly Mediator mediator;
    private readonly EShopDbContext context;
    
    // EFCore InMemory
    //services.AddDbContext<EShopDbContext>(options =>
    //        options.UseInMemoryDatabase("TestDb"));
    public InMemoryTest()
    {
        var services = new ServiceCollection();
        // InMemory DB
        services.AddDbContext<EShopDbContext>(options =>
            options.UseInMemoryDatabase("TestDb"));
        services.AddScoped<Mediator>();
        services.AddCqrsHandlers(typeof(CreateProductCommandHandler).Assembly);
        var provider = services.BuildServiceProvider();
        mediator = provider.GetRequiredService<Mediator>();
        context = provider.GetRequiredService<EShopDbContext>();

        //services.AddDbContext<EShopDbContext>(options =>
        //    options.UseInMemoryDatabase("TestDb"));
        //serviceProvider = services.BuildServiceProvider();
    }
    [Fact]
    public async Task Should_Save_Product_InMemory() 
    {
        // Arrange
        var command = new CreateProductCommand("Test Product", 100);

        // Act
        var productId = await mediator.Send(command);

        // Assert
        var product = await context.Products.FindAsync(productId);
        Assert.NotNull(product);
        Assert.Equal("Test Product", product.Name);
        Assert.Equal(100, product.Price);
    }

}
