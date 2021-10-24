using ProductManagement.Contract.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductManagement.Data.Repositories.Implementation
{
    internal class ProductRepository : IProductRepository
    {
        private readonly List<Product> _products = new();
        private readonly List<ProductOption> _productOptions = new();

        public Task<Guid> CreateProduct(Product product)
        {
            _products.Add(product);

            return Task.FromResult(product.Id);
        }

        public Task<Guid> CreateProductOption(ProductOption productOption)
        {
            _productOptions.Add(productOption);

            return Task.FromResult(productOption.Id);
        }

        public Task DeleteProductById(Guid productId)
        {
            _products.Remove(_products.Where(product => product.Id == productId).FirstOrDefault());

            return Task.CompletedTask;
        }

        public Task DeleteProductOptionById(Guid productId, Guid productOptionId)
        {
            _productOptions
                .Remove(_productOptions
                .Where(option => option.Id == productOptionId && option.ProductId == productId)
                .FirstOrDefault());
            
            return Task.CompletedTask;
        }

        public Task DeleteProductOptionByProductId(Guid productId)
        {
            _productOptions.RemoveAll(option => option.ProductId == productId);

            return Task.CompletedTask;
        }

        public Task<Product> GetProductById(Guid productId)
        {
            return Task.FromResult(_products.Where(product => product.Id == productId).FirstOrDefault());
            
        }

        public Task<Products> GetProductByName(string name)
        {
            return Task.FromResult(new Products()
            {
                Items = _products.Where(product => product.Name == name).ToList()
            });
        }

        public Task<ProductOption> GetProductOptionById(Guid productId, Guid productOptionId)
        {
            return Task.FromResult(_productOptions
                .Where(option => option.Id == productOptionId && option.ProductId == productId)
                .FirstOrDefault());
        }

        public Task<ProductOptions> GetProductOptionsByProductId(Guid productId)
        {
            return Task.FromResult(new ProductOptions()
            {
                Items = _productOptions.Where(option => option.ProductId == productId).ToList()
            });
        }

        public Task<Products> GetProducts()
        {
            return Task.FromResult(new Products()
            {
                Items = _products
            });
        }

        public Task UpdateProduct(Guid productId, Product product)
        {
            Product oldProduct = _products.First(product => product.Id == productId);
            int index = _products.IndexOf(oldProduct);

            if (oldProduct != null)
                _products[index] = product;

            return Task.CompletedTask;
        }

        public Task UpdateProductOption(Guid productId, Guid productOptionId, ProductOption productOption)
        {
            ProductOption oldProductOption = _productOptions
                .First(option => option.Id == productOptionId && option.ProductId == productId);

            int index = _productOptions.IndexOf(oldProductOption);

            if (oldProductOption != null)
                _productOptions[index] = productOption;

            return Task.CompletedTask;
        }
    }
}
