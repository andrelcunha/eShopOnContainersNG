using ALC.Catalog.API.Models;
using ALC.Core.Data;

namespace ALC.Catalog.API.Repository
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<IEnumerable<Product>> GetProducts();
        Task<Product?> GetProduct(Guid id);
        Task AddProduct(Product product);
        Task UpdateProduct(Product product);
        Task DeleteProduct(Guid id);
    }
}
