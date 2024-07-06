using Application.Contracts.Product;
using Core.Repositories;

namespace Application.Usecases.Product;

public class DeleteProductUsecase : IDeleteProduct
{
    private readonly IProductRepository _productRepository;

    public DeleteProductUsecase(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task Execute(Guid id)
    {
        await this._productRepository.Remove(id);
    }
}