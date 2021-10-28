using ProductManagement.Contract.Models;
using ProductManagement.Data.Repositories;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ProductManagement.Business.Services.Implementation
{
    internal class ProductOptionService : IProductOptionService
    {
        private readonly IProductRepository _productRepository;

        public ProductOptionService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Guid> CreateProductOption(Guid productId, ProductOption contract)
        {
            Product product = await _productRepository.GetProductById(productId).ConfigureAwait(false);

            if (product == null)
                throw new Exception($"A Product with Id: {productId} does not exist.");

            Guid productOptionId = Guid.NewGuid();
            contract.Id = productOptionId;

            return await _productRepository.CreateProductOption(contract);
        }

        public async Task DeleteProductOption(Guid productId, Guid productOptionId)
        {
            ProductOption productOption = 
                await _productRepository.GetProductOptionById(productId, productOptionId).ConfigureAwait(false);

            if (productOption == null)
                throw new Exception($"A ProductOption with Id: {productOptionId} and ProductId: {productId} does not exist.");

            await _productRepository.DeleteProductOptionById(productId, productOptionId);
        }

        public async Task<ProductOption> GetProductOptionById(Guid productId, Guid productOptionId)
        {
            ProductOption productOption = 
                await _productRepository.GetProductOptionById(productId, productOptionId).ConfigureAwait(false);

            if (productOption == null)
                throw new Exception($"A ProductOption with Id: {productOptionId} and ProductId: {productId} does not exist.");

            return productOption;
        }

        public async Task<ProductOptions> GetProductOptionByProductId(Guid productId)
        {
            ProductOptions productOptions = 
                await _productRepository.GetProductOptionsByProductId(productId).ConfigureAwait(false);

            if (productOptions == null || !productOptions.Items.Any())
                throw new Exception($"There are no Product Options with ProductId: {productId}.");

            return productOptions;
        }

        public async Task UpdateProductOption(Guid productId, Guid productOptionId, ProductOption contract)
        {
            if (productOptionId != contract.Id)
                throw new Exception("ProductOptionId does not match request body OptionId");

            if (productId != contract.ProductId)
                throw new Exception("ProductId does not match request body ProductId");

            ProductOption productOption = 
                await _productRepository.GetProductOptionById(productId, productOptionId).ConfigureAwait(false);

            if (productOption == null)
                throw new Exception($"A ProductOption with Id: {productOptionId} and ProductId: {productId} does not exist.");

            await _productRepository.UpdateProductOption(productId, productOptionId, contract);
        }
    }
}
