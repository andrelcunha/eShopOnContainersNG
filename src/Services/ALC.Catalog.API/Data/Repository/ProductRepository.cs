using ALC.Catalog.API.Data;
using ALC.Catalog.API.Models;
using ALC.Core.Data;
using Microsoft.EntityFrameworkCore;

namespace ALC.Catalog.API.Repository
{
    public class ProductRepository: IProductRepository
    {
        private readonly CatalogContext _context;

        public IUnitOfWork UnitOfWork => _context;

        public ProductRepository(CatalogContext catalog)
        {
            _context = catalog;
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            return await _context.Products.AsNoTracking().ToListAsync();
        }

        public async Task<Product?> GetProduct(int id)
        {
            return await _context.Products.FindAsync(id);
        }

        public async Task AddProduct(Product product)
        {
            product.CreatedAt = DateTime.UtcNow;
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateProduct(Product product)
        {
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteProduct(int id)
        {
            var product =  await _context.Products.FindAsync(id);
            if(product is not null)
            {
                product.Active = false;
                _context.Products.Update(product);
                await _context.SaveChangesAsync();
            }
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
