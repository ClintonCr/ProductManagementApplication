using ProductManagement.Contract.Models;
using ProductManagement.Data.Repositories;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ProductManagement.Business.Services.Implementation
{
    internal class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public Task<Guid> CreateProduct(Product contract)
        {
            Guid productId = Guid.NewGuid();
            contract.Id = productId;

            return _productRepository.CreateProduct(contract);
        }

        public async Task DeleteProductById(Guid productId)
        {
            Product product = await _productRepository.GetProductById(productId).ConfigureAwait(false);

            if (product == null)
                throw new Exception($"A Product with Id: {productId} does not exist.");

             await _productRepository.DeleteProductOptionByProductId(productId);
             await _productRepository.DeleteProductById(productId);
        }

        public Task<Products> GetProducts() => _productRepository.GetProducts();

        public async Task<Product> GetProductById(Guid productId)
        {
            Product product = await _productRepository.GetProductById(productId).ConfigureAwait(false);

            if (product == null)
                throw new Exception($"A Product with Id: {productId} does not exist.");
                
            return product;
        }

        public async Task<Products> GetProductByName(string name)
        {
            Products products = await _productRepository.GetProductByName(name).ConfigureAwait(false);

            if (products == null || !products.Items.Any())
                throw new Exception($"No products exist with Name: {name}.");
                
            return products;
        }

        public async Task UpdateProduct(Guid productId, Product contract)
        {
            if (productId != contract.Id)
                throw new Exception("ProductId does not match request body ProductId");

            Product product = await _productRepository.GetProductById(productId).ConfigureAwait(false);

            if (product == null)
                throw new Exception($"A Product with Id: {productId} does not exist.");

            await _productRepository.UpdateProduct(productId, contract);
        }
    }
}
