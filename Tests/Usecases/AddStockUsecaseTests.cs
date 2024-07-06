using System.Threading.Tasks;
using Application.Contracts;
using Application.Dtos;
using Application.Requests;
using Application.Services;
using Application.Usecases;
using Application.Usecases.Product;
using Core.Entities;
using Core.Repositories;
using Moq;
using Xunit;

using System;
using System.Threading.Tasks;
using Application.Contracts;
using Application.Dtos;
using Application.Usecases;
using Core.Entities;
using Moq;
using Xunit;

public class AddStockUsecaseTests
{
    [Fact]
    public async Task Execute_Should_AddStock_When_ValidRequest()
    {
        // Arrange
        var mockRepository = new Mock<IProductRepository>();
        var mockMapper = new Mock<IMappingService<ProductDto, Product>>();

        var usecase = new AddStockUsecase(mockRepository.Object, mockMapper.Object);

        var request = new AddStockRequest
        {
            Id = Guid.NewGuid(),
            Quantity = 10
        };

        var product = new Product
        {
            Id = request.Id,
            Name = "Product A",
            PartNumber = "PN123456",
            StockQuantity = 50, // Initial stock quantity
            Price = 9.99m,
            ConsumedQuantity = 5,
            ConsumedPrice = 49.95m
        };

        var updatedProduct = new Product
        {
            Id = request.Id,
            Name = "Product A",
            PartNumber = "PN123456",
            StockQuantity = 60, // 50 initial + 10 added
            Price = 9.99m,
            ConsumedQuantity = 5,
            ConsumedPrice = 49.95m
        };

        mockRepository.Setup(repo => repo.GetById(request.Id)).ReturnsAsync(product);
        mockRepository.Setup(repo => repo.Update(It.IsAny<Product>())).ReturnsAsync(updatedProduct);
        mockMapper.Setup(mapper => mapper.ToDto(It.IsAny<Product>()));

    }
}
