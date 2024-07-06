using Application.Usecases.Product;
using Core.Entities;
using Core.Repositories;
using Moq;
using Xunit;

namespace Tests.Usecases;

public class ResetDailyConsumptionUsecaseTests
{
    [Fact]
    public async Task Execute_Should_ResetDailyConsumption_When_ValidId()
    {
        // Arrange
        var mockRepository = new Mock<IProductRepository>();
        var usecase = new ResetDailyConsumptionUsecase(mockRepository.Object);

        var productId = Guid.NewGuid();

        var product = new Product
        {
            Id = productId,
            ConsumedQuantity = 10,
            ConsumedPrice = 50.00m
        };

        mockRepository.Setup(repo => repo.GetById(productId)).ReturnsAsync(product);
        mockRepository.Setup(repo => repo.Update(It.IsAny<Product>())).ReturnsAsync(product);

        // Act
        await usecase.Execute(productId);

        // Assert
        mockRepository.Verify(repo => repo.GetById(productId), Times.Once);
        mockRepository.Verify(repo => repo.Update(It.Is<Product>(p => p.ConsumedQuantity == 0 && p.ConsumedPrice == 0)), Times.Once);
    }
}