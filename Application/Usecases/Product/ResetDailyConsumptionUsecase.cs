using Application.Contracts.Product;
using Core.Exceptions;
using Core.Repositories;

namespace Application.Usecases.Product;

public class ResetDailyConsumptionUsecase : IResetDailyConsumption
{
    private readonly IProductRepository _productRepository;

    public ResetDailyConsumptionUsecase(IProductRepository productRepository)
    {
        _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
    }

    public async Task Execute(Guid Id)
    {
        var product = await _productRepository.GetById(Id);
        if (product == null)
        {
            throw new BadRequestException("Product not found.");
        }

        product.ResetDailyConsumption();
        await _productRepository.Update(product);
    }
}