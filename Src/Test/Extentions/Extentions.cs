using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Commands;
using Test.Model;

namespace Test.Extentions;

public static class Extentions
{
    public static Product ToProduct(this CreateProductCommand productCommand)
    {
        var product = new Product
        {
            Name = productCommand.Name,
            Price = productCommand.Price
        };
        return product;
    }
}
