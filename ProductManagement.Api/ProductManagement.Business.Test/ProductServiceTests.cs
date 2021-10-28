using AutoFixture;
using Moq;
using ProductManagement.Business.Services.Implementation;
using ProductManagement.Contract.Models;
using ProductManagement.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace ProductManagement.Business.Test
{
    public class ProductServiceTests
    {
        [Fact]
        public async Task ProductService_CreateProduct_Success()
        {
            // Arrange
            var fixture = new Fixture();

            Product product = fixture.Create<Product>();
            var mockProductRepository = new Mock<IProductRepository>();

            fixture.Register(() => mockProductRepository.Object);

            var target = fixture.Create<ProductService>();

            // Act
            await target.CreateProduct(product);

            // Assert
            mockProductRepository.Verify(repo => repo.CreateProduct(product), Times.Once);
            mockProductRepository.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ProductService_DeleteProductById_Success()
        {
            // Arrange
            var fixture = new Fixture();

            Product product = fixture.Create<Product>();
            var mockProductRepository = new Mock<IProductRepository>();
            mockProductRepository
                .Setup(r => r.GetProductById(product.Id))
                .Returns(Task.FromResult(product));

            fixture.Register(() => mockProductRepository.Object);

            var target = fixture.Create<ProductService>();

            // Act
            await target.DeleteProductById(product.Id);

            // Assert
            mockProductRepository.Verify(repo => repo.GetProductById(product.Id), Times.Once);
            mockProductRepository.Verify(repo => repo.DeleteProductById(product.Id), Times.Once);
            mockProductRepository.Verify(repo => repo.DeleteProductOptionByProductId(product.Id), Times.Once);
            mockProductRepository.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ProductService_DeleteProductById_ThrowException()
        {
            // Arrange
            var fixture = new Fixture();

            Guid productId = fixture.Create<Guid>();
            var mockProductRepository = new Mock<IProductRepository>();

            mockProductRepository
                .Setup(r => r.GetProductById(productId))
                .Returns(Task.FromResult<Product>(null));

            fixture.Register(() => mockProductRepository.Object);

            var target = fixture.Create<ProductService>();

            // Act + Assert
            var exception = await Assert.ThrowsAsync<Exception>(() => target.DeleteProductById(productId));
            Assert.Equal($"A Product with Id: {productId} does not exist.", exception.Message);
            mockProductRepository.Verify(repo => repo.GetProductById(productId), Times.Once);
            mockProductRepository.Verify(repo => repo.DeleteProductById(productId), Times.Never);
            mockProductRepository.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ProductService_GetProducts_Success()
        {
            // Arrange
            var fixture = new Fixture();

            var mockProductRepository = new Mock<IProductRepository>();

            fixture.Register(() => mockProductRepository.Object);

            var target = fixture.Create<ProductService>();

            // Act
            await target.GetProducts();

            // Assert
            mockProductRepository.Verify(repo => repo.GetProducts(), Times.Once);
            mockProductRepository.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ProductService_GetProductById_Success()
        {
            // Arrange
            var fixture = new Fixture();

            Guid productId = fixture.Create<Guid>();
            Product product = fixture
                .Build<Product>()
                .With(p => p.Id, productId)
                .Create();

            var mockProductRepository = new Mock<IProductRepository>();
            mockProductRepository
                .Setup(r => r.GetProductById(productId))
                .Returns(Task.FromResult(product));

            fixture.Register(() => mockProductRepository.Object);

            var target = fixture.Create<ProductService>();

            // Act
            await target.GetProductById(productId);

            // Assert
            mockProductRepository.Verify(repo => repo.GetProductById(productId), Times.Once);
            mockProductRepository.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ProductService_GetProductById_ThrowsException()
        {
            // Arrange
            var fixture = new Fixture();

            Guid productId = fixture.Create<Guid>();
            var mockProductRepository = new Mock<IProductRepository>();

            mockProductRepository
                .Setup(r => r.GetProductById(productId))
                .Returns(Task.FromResult<Product>(null));

            fixture.Register(() => mockProductRepository.Object);

            var target = fixture.Create<ProductService>();

            // Act + Assert
            var exception = await Assert.ThrowsAsync<Exception>(() => target.GetProductById(productId));
            Assert.Equal($"A Product with Id: {productId} does not exist.", exception.Message);
            mockProductRepository.Verify(repo => repo.GetProductById(productId), Times.Once);
            mockProductRepository.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ProductService_GetProductByName_Success()
        {
            // Arrange
            var fixture = new Fixture();

            string productName = fixture.Create<string>();
            Products products = fixture.Create<Products>();

            var mockProductRepository = new Mock<IProductRepository>();
            mockProductRepository
                .Setup(r => r.GetProductByName(productName))
                .Returns(Task.FromResult(products));

            fixture.Register(() => mockProductRepository.Object);

            var target = fixture.Create<ProductService>();

            // Act
            await target.GetProductByName(productName);

            // Assert
            mockProductRepository.Verify(repo => repo.GetProductByName(productName), Times.Once);
            mockProductRepository.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ProductService_GetProductByName_ThrowsException()
        {
            // Arrange
            var fixture = new Fixture();

            string productName = fixture.Create<string>();
            var mockProductRepository = new Mock<IProductRepository>();

            mockProductRepository
                .Setup(r => r.GetProductByName(productName))
                .Returns(Task.FromResult(new Products() 
                {
                    Items = new List<Product>()
                }));

            fixture.Register(() => mockProductRepository.Object);

            var target = fixture.Create<ProductService>();

            // Act + Assert
            var exception = await Assert.ThrowsAsync<Exception>(() => target.GetProductByName(productName));
            Assert.Equal($"No products exist with Name: {productName}.", exception.Message);
            mockProductRepository.Verify(repo => repo.GetProductByName(productName), Times.Once);
            mockProductRepository.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ProductService_UpdateProduct_Success()
        {
            // Arrange
            var fixture = new Fixture();

            Product product = fixture.Create<Product>();
            var mockProductRepository = new Mock<IProductRepository>();
            mockProductRepository
                .Setup(r => r.GetProductById(product.Id))
                .Returns(Task.FromResult(product));

            fixture.Register(() => mockProductRepository.Object);

            var target = fixture.Create<ProductService>();

            // Act
            await target.UpdateProduct(product.Id, product);

            // Assert
            mockProductRepository.Verify(repo => repo.GetProductById(product.Id), Times.Once);
            mockProductRepository.Verify(repo => repo.UpdateProduct(product.Id, product), Times.Once);
            mockProductRepository.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ProductService_UpdateProduct_ThrowNotExistException()
        {
            // Arrange
            var fixture = new Fixture();

            Product product = fixture.Create<Product>();
            var mockProductRepository = new Mock<IProductRepository>();

            mockProductRepository
                .Setup(r => r.GetProductById(product.Id))
                .Returns(Task.FromResult<Product>(null));

            fixture.Register(() => mockProductRepository.Object);

            var target = fixture.Create<ProductService>();

            // Act + Assert
            var exception = await Assert.ThrowsAsync<Exception>(() => target.UpdateProduct(product.Id, product));
            Assert.Equal($"A Product with Id: {product.Id} does not exist.", exception.Message);
            mockProductRepository.Verify(repo => repo.GetProductById(product.Id), Times.Once);
            mockProductRepository.Verify(repo => repo.UpdateProduct(product.Id, product), Times.Never);
            mockProductRepository.VerifyNoOtherCalls();
        }

        [Theory]
        [InlineData("8560cc0e-6729-4777-be95-6b6aed58f3d5", "8560cc0e-6729-4777-be95-6b6aed58f3d6",
            "ProductId does not match request body ProductId")]
        public async Task ProductService_UpdateProduct_ThrowMisMatchException(
            Guid entityId,
            Guid payloadId,
            string exceptionMessage)
        {
            // Arrange
            var fixture = new Fixture();

            Product product = fixture
                .Build<Product>()
                .With(p => p.Id, payloadId)
                .Create();

            var mockProductRepository = new Mock<IProductRepository>();
            mockProductRepository
                .Setup(r => r.GetProductById(entityId))
                .Returns(Task.FromResult(product));

            fixture.Register(() => mockProductRepository.Object);

            var target = fixture.Create<ProductService>();

            // Act + Assert
            var exception = await Assert.ThrowsAsync<Exception>(() => target.UpdateProduct(entityId, product));
            Assert.Equal(exceptionMessage, exception.Message);
            mockProductRepository.Verify(repo => repo.GetProductById(entityId), Times.Never);
            mockProductRepository.Verify(repo => repo.UpdateProduct(entityId, product), Times.Never);
            mockProductRepository.VerifyNoOtherCalls();
        }
    }
}
