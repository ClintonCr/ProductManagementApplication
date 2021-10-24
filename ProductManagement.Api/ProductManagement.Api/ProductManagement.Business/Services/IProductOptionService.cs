using ProductManagement.Contract.Models;
using System;
using System.Threading.Tasks;

namespace ProductManagement.Business.Services
{
    public interface IProductOptionService
    {
        Task<ProductOptions> GetProductOptionByProductId(Guid productOptionid);
        Task<ProductOption> GetProductOptionById(Guid productId, Guid productOptionid);
        Task UpdateProductOption(Guid productId, Guid productOptionid, ProductOption productOption);
        Task<Guid> CreateProductOption(Guid productId, ProductOption productOption);
        Task DeleteProductOption(Guid productId, Guid productOptionid);
    }
}
