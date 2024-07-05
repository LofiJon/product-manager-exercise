using Core.Entities;
using Infrastructure.Database.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Seeders;

public class ProductsSeeders
{
    public static async Task Execute(ApplicationDbContext applicationDbContext)
    {
        if (!await applicationDbContext.Products.AnyAsync())
        {
            List<Product> products = new()
            {
                new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Product A",
                    PartNumber = "PN123456",
                    StockQuantity = 100,
                    Price = 9.99m,
                    ConsumedQuantity = 10,
                    ConsumedPrice = 5.99m,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Product B",
                    PartNumber = "PN654321",
                    StockQuantity = 200,
                    Price = 19.99m,
                    ConsumedQuantity = 20,
                    ConsumedPrice = 15.99m,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Product C",
                    PartNumber = "PN789012",
                    StockQuantity = 300,
                    Price = 29.99m,
                    ConsumedQuantity = 30,
                    ConsumedPrice = 25.99m,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                }
            };
            await applicationDbContext.Products.AddRangeAsync(products);
            await applicationDbContext.SaveChangesAsync();
        }
    }
}