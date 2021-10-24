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
    public class ProductOptionServiceTests
    {
        [Fact]
        public async Task ProductOptionService_CreateProductOption_Success()
        {
            // Arrange
            var fixture = new Fixture();

            Product product = fixture.Create<Product>();
            ProductOption productOption = fixture
                .Build<ProductOption>()
                .With(option => option.ProductId, product.Id)
                .Create();

            var mockProductRepository = new Mock<IProductRepository>();
            mockProductRepository
                .Setup(r => r.GetProductById(product.Id))
                .Returns(Task.FromResult(product));

            fixture.Register(() => mockProductRepository.Object);

            var target = fixture.Create<ProductOptionService>();

            // Act
            await target.CreateProductOption(product.Id, productOption);

            // Assert
            mockProductRepository.Verify(repo => repo.GetProductById(product.Id), Times.Once);
            mockProductRepository.Verify(repo => repo.CreateProductOption(productOption), Times.Once);
            mockProductRepository.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ProductOptionService_DeleteProductOption_Success()
        {
            // Arrange
            var fixture = new Fixture();

            Product product = fixture.Create<Product>();
            ProductOption productOption = fixture
                .Build<ProductOption>()
                .With(option => option.ProductId, product.Id)
                .Create();

            var mockProductRepository = new Mock<IProductRepository>();
            mockProductRepository
                .Setup(r => r.GetProductOptionById(product.Id, productOption.Id))
                .Returns(Task.FromResult(productOption));

            fixture.Register(() => mockProductRepository.Object);

            var target = fixture.Create<ProductOptionService>();

            // Act
            await target.DeleteProductOption(product.Id, productOption.Id);

            // Assert
            mockProductRepository.Verify(repo => repo.GetProductOptionById(product.Id, productOption.Id), Times.Once);
            mockProductRepository.Verify(repo => repo.DeleteProductOptionById(product.Id, productOption.Id), Times.Once);
            mockProductRepository.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ProductOptionService_DeleteProductOption_ThrowException()
        {
            // Arrange
            var fixture = new Fixture();

            Guid productId = fixture.Create<Guid>();
            Guid productOptionId = fixture.Create<Guid>();
            var mockProductRepository = new Mock<IProductRepository>();

            mockProductRepository
                .Setup(r => r.GetProductOptionById(productId, productOptionId))
                .Returns(Task.FromResult<ProductOption>(null));

            fixture.Register(() => mockProductRepository.Object);

            var target = fixture.Create<ProductOptionService>();

            // Act + Assert
            var exception = await Assert.ThrowsAsync<Exception>(() => target.DeleteProductOption(productId, productOptionId));
            Assert.Equal(
                $"A ProductOption with Id: {productOptionId} and ProductId: {productId} does not exist.", 
                exception.Message);
            mockProductRepository.Verify(repo => repo.GetProductOptionById(productId, productOptionId), Times.Once);
            mockProductRepository.Verify(repo => repo.DeleteProductOptionById(productId, productOptionId), Times.Never);
            mockProductRepository.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ProductOptionService_GetProductOptionById_Success()
        {
            // Arrange
            var fixture = new Fixture();

            Product product = fixture.Create<Product>();
            ProductOption productOption = fixture
                .Build<ProductOption>()
                .With(option => option.ProductId, product.Id)
                .Create();

            var mockProductRepository = new Mock<IProductRepository>();
            mockProductRepository
                .Setup(r => r.GetProductOptionById(product.Id, productOption.Id))
                .Returns(Task.FromResult(productOption));

            fixture.Register(() => mockProductRepository.Object);

            var target = fixture.Create<ProductOptionService>();

            // Act
            await target.GetProductOptionById(product.Id, productOption.Id);

            // Assert
            mockProductRepository.Verify(repo => repo.GetProductOptionById(product.Id, productOption.Id), Times.Once);
            mockProductRepository.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ProductOptionService_GetProductOptionById_ThrowsException()
        {
            // Arrange
            var fixture = new Fixture();

            Guid productId = fixture.Create<Guid>();
            Guid productOptionId = fixture.Create<Guid>();
            var mockProductRepository = new Mock<IProductRepository>();

            mockProductRepository
                .Setup(r => r.GetProductOptionById(productId, productOptionId))
                .Returns(Task.FromResult<ProductOption>(null));

            fixture.Register(() => mockProductRepository.Object);

            var target = fixture.Create<ProductOptionService>();

            // Act + Assert
            var exception = await Assert.ThrowsAsync<Exception>(() => target.GetProductOptionById(productId, productOptionId));
            Assert.Equal(
                $"A ProductOption with Id: {productOptionId} and ProductId: {productId} does not exist.",
                exception.Message);
            mockProductRepository.Verify(repo => repo.GetProductOptionById(productId, productOptionId), Times.Once);
            mockProductRepository.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ProductOptionService_GetProductOptionByProductId_Success()
        {
            // Arrange
            var fixture = new Fixture();

            ProductOptions productOptions = fixture.Create<ProductOptions>();

            var mockProductRepository = new Mock<IProductRepository>();
            mockProductRepository
                .Setup(r => r.GetProductOptionsByProductId(It.IsAny<Guid>()))
                .Returns(Task.FromResult(productOptions));

            fixture.Register(() => mockProductRepository.Object);

            var target = fixture.Create<ProductOptionService>();

            // Act
            await target.GetProductOptionByProductId(It.IsAny<Guid>());

            // Assert
            mockProductRepository.Verify(repo => repo.GetProductOptionsByProductId(It.IsAny<Guid>()), Times.Once);
            mockProductRepository.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ProductOptionService_GetProductOptionByProductId_ThrowsException()
        {
            // Arrange
            var fixture = new Fixture();

            Guid productId = fixture.Create<Guid>();
            var mockProductRepository = new Mock<IProductRepository>();

            mockProductRepository
                .Setup(r => r.GetProductOptionsByProductId(productId))
                .Returns(Task.FromResult(new ProductOptions() 
                {
                    Items = new List<ProductOption>()
                }));

            fixture.Register(() => mockProductRepository.Object);

            var target = fixture.Create<ProductOptionService>();

            // Act + Assert
            var exception = await Assert.ThrowsAsync<Exception>(() => target.GetProductOptionByProductId(productId));
            Assert.Equal(
                $"There are no Product Options with ProductId: {productId}.",
                exception.Message);
            mockProductRepository.Verify(repo => repo.GetProductOptionsByProductId(productId), Times.Once);
            mockProductRepository.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ProductOptionService_UpdateProductOption_Success()
        {
            // Arrange
            var fixture = new Fixture();

            Product product = fixture.Create<Product>();
            ProductOption productOption = fixture
                .Build<ProductOption>()
                .With(option => option.ProductId, product.Id)
                .Create();

            var mockProductRepository = new Mock<IProductRepository>();
            mockProductRepository
                .Setup(r => r.GetProductOptionById(product.Id, productOption.Id))
                .Returns(Task.FromResult(productOption));

            fixture.Register(() => mockProductRepository.Object);

            var target = fixture.Create<ProductOptionService>();

            // Act
            await target.UpdateProductOption(product.Id, productOption.Id, productOption);

            // Assert
            mockProductRepository.Verify(repo => repo.GetProductOptionById(product.Id, productOption.Id), Times.Once);
            mockProductRepository.Verify(
                repo => repo.UpdateProductOption(product.Id, productOption.Id, productOption), Times.Once);
            mockProductRepository.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ProductOptionService_UpdateProductOption_ThrowNotExistException()
        {
            // Arrange
            var fixture = new Fixture();

            ProductOption productOption = fixture.Create<ProductOption>();

            var mockProductRepository = new Mock<IProductRepository>();
            mockProductRepository
                .Setup(r => r.GetProductOptionById(productOption.ProductId, productOption.Id))
                .Returns(Task.FromResult<ProductOption>(null));

            fixture.Register(() => mockProductRepository.Object);

            var target = fixture.Create<ProductOptionService>();

            // Act + Assert
            var exception = await Assert.ThrowsAsync<Exception>(
                () => target.UpdateProductOption(productOption.ProductId, productOption.Id, productOption));

            Assert.Equal(
                $"A ProductOption with Id: {productOption.Id} and ProductId: {productOption.ProductId} does not exist.", 
                exception.Message);

            mockProductRepository.Verify(
                repo => repo.GetProductOptionById(productOption.ProductId, productOption.Id), Times.Once);
            mockProductRepository.Verify(
                repo => repo.UpdateProductOption(productOption.ProductId, productOption.Id, productOption), Times.Never);
            mockProductRepository.VerifyNoOtherCalls();
        }

        [Theory]
        [InlineData(
            "8560cc0e-6729-4777-be95-6b6aed58f3d5", "8560cc0e-6729-4777-be95-6b6aed58f3d6",
            "8560cc0e-6729-4777-be95-6b6aed58f3d6", "8560cc0e-6729-4777-be95-6b6aed58f3d6",
            "ProductId does not match request body ProductId")]
        [InlineData(
            "8560cc0e-6729-4777-be95-6b6aed58f3d6", "8560cc0e-6729-4777-be95-6b6aed58f3d6",
            "8560cc0e-6729-4777-be95-6b6aed58f3d5", "8560cc0e-6729-4777-be95-6b6aed58f3d6",
            "ProductOptionId does not match request body OptionId")]
        public async Task ProductOptionService_UpdateProductOption_ThrowMisMatchException(
            Guid productId,
            Guid payloadProductId,
            Guid productOptionId,
            Guid payloadProductOptionId,
            string exceptionMessage)
        {
            // Arrange
            var fixture = new Fixture();

            ProductOption productOption = fixture
                .Build<ProductOption>()
                .With(p => p.Id, payloadProductOptionId)
                .With(p => p.ProductId, payloadProductId)
                .Create();

            var mockProductRepository = new Mock<IProductRepository>();
            mockProductRepository
                .Setup(r => r.GetProductOptionById(productId, productOptionId))
                .Returns(Task.FromResult(productOption));

            fixture.Register(() => mockProductRepository.Object);

            var target = fixture.Create<ProductOptionService>();

            // Act + Assert
            var exception = await Assert.ThrowsAsync<Exception>(
                () => target.UpdateProductOption(productId, productOptionId, productOption));

            Assert.Equal(exceptionMessage, exception.Message);

            mockProductRepository.Verify(repo => repo.GetProductOptionById(productId, productOptionId), Times.Never);
            mockProductRepository
                .Verify(repo => repo.UpdateProductOption(productId, productOptionId, productOption), Times.Never);
            mockProductRepository.VerifyNoOtherCalls();
        }
    }
}
