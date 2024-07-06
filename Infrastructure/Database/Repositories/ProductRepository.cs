using Core.Entities;
using Core.Repositories;
using Dapper;
using Infrastructure.Database.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Infrastructure.Database.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Product> Add(Product entity)
        {
            var validation = await Validate(entity);
            EntityEntry<Product> result = null;

            if (validation)
            {
                result = await _context.AddAsync(entity);
                await _context.SaveChangesAsync();
            }

            return result.Entity;
        }

        private async Task<bool> Validate(Product product)
        {
            // Example validation logic: Check if a product with the same name or part number already exists
            var existingProduct = await _context.Products
                .FirstOrDefaultAsync(p => p.Name == product.Name || p.PartNumber == product.PartNumber);

            return existingProduct == null;
        }

        public async Task<int> Count()
        {
            return await _context.Products.CountAsync();
        }

        public async Task<IEnumerable<Product>> GetAll()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<Product> GetById(Guid id)
        {
            return await _context.Products.FindAsync(id);
        }

        public async Task<List<Product>> Pageable(int number, int size)
        {
            return await _context.Products
                .Where(p => p.DeletedAt == null)
                .OrderBy(p => p.CreatedAt)
                .Skip((number - 1) * size)
                .Take(size)
                .ToListAsync();
        }

        public async Task<bool> Remove(Guid id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null) return false;
            product.DeletedAt = DateTime.Now;
            _context.Update(product);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Product> Update(Product entity)
        {
            var product = await _context.Products.FindAsync(entity.Id);
            if (product == null) throw new KeyNotFoundException("Product not found");

            product.Name = entity.Name;
            product.PartNumber = entity.PartNumber;
            product.StockQuantity = entity.StockQuantity;
            product.Price = entity.Price;
            product.ConsumedQuantity = entity.ConsumedQuantity;
            product.ConsumedPrice = entity.ConsumedPrice;
            product.UpdatedAt = DateTime.UtcNow;

            _context.Products.Update(product);
            await _context.SaveChangesAsync();

            return product;
        }
    }
}
