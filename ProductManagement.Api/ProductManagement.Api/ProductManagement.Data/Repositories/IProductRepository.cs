using ProductManagement.Contract.Models;
using System;
using System.Threading.Tasks;

namespace ProductManagement.Data.Repositories
{
    public interface IProductRepository
    {
        Task<Products> GetProducts();
        Task<Products> GetProductByName(string name);
        Task<Product> GetProductById(Guid productId);
        Task UpdateProduct(Guid productId, Product product);
        Task<Guid> CreateProduct(Product product);
        Task DeleteProductById(Guid productId);
        Task<ProductOptions> GetProductOptionsByProductId(Guid productId);
        Task<ProductOption> GetProductOptionById(Guid productId, Guid productOptionId);
        Task UpdateProductOption(Guid productId, Guid productOptionId, ProductOption productOption);
        Task<Guid> CreateProductOption(ProductOption productOption);
        Task DeleteProductOptionById(Guid productId, Guid productOptionId);
        Task DeleteProductOptionByProductId(Guid productId);
    }
}
