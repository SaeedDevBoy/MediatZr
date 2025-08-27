using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Model;

namespace Test.Data;

public class EShopDbContext : DbContext
{
    public EShopDbContext(DbContextOptions<EShopDbContext> options) : base(options) { }
    public DbSet<Product> Products { get; set; }
}