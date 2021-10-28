using ProductManagement.Contract.Models;
using System;
using System.Threading.Tasks;

namespace ProductManagement.Business.Services
{
    public interface IProductService
    {
        Task<Products> GetProducts();
        Task<Products> GetProductByName(string name);
        Task<Product> GetProductById(Guid productId);
        Task UpdateProduct(Guid productId, Product product);
        Task<Guid> CreateProduct(Product product);
        Task DeleteProductById(Guid productId);
    }
}
