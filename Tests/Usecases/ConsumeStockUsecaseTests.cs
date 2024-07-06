using Application.Dtos;
using Application.Requests;
using Application.Services;
using Application.Usecases.Product;
using Core.Entities;
using Core.Repositories;
using Moq;
using Xunit;

namespace Tests.Usecases;

public class ConsumeStockUsecaseTests
{
    [Fact]
    public async Task Execute_Should_ConsumeStock_When_ValidRequest()
    {
        // Arrange
        var mockRepository = new Mock<IProductRepository>();
        var mockMapper = new Mock<IMappingService<ProductDto, Product>>();

        var usecase = new ConsumeStockUsecase(mockRepository.Object, mockMapper.Object);

        var request = new ConsumeRequest
        {
            ProductId = Guid.NewGuid(),
            Quantity = 10
        };

        var product = new Product
        {
            Id = request.ProductId,
            StockQuantity = 10,
            Price = 5m
        };

        mockRepository.Setup(repo => repo.GetById(request.ProductId)).ReturnsAsync(product);
        mockRepository.Setup(repo => repo.Update(It.IsAny<Product>())).ReturnsAsync(product);
        mockMapper.Setup(mapper => mapper.ToDto(It.IsAny<Product>())).Returns(new ProductDto
        {
            Id = product.Id,
            Name = product.Name,
            PartNumber = product.PartNumber,
            StockQuantity = product.StockQuantity,
            Price = product.Price,
            ConsumedQuantity = product.ConsumedQuantity,
            ConsumedPrice = product.ConsumedPrice
        });

        // Act
        var result = await usecase.Execute(request);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(10, result.StockQuantity);
        mockRepository.Verify(repo => repo.GetById(request.ProductId), Times.Once);
        mockRepository.Verify(repo => repo.Update(It.IsAny<Product>()), Times.Once);
        mockMapper.Verify(mapper => mapper.ToDto(It.IsAny<Product>()), Times.Once);
    }
}