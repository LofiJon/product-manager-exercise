using Application.Contracts.Product;
using Application.Dtos;
using Application.Requests;
using Application.Services;
using Core.Repositories;
using Core.Exceptions;

namespace Application.Usecases.Product;

public class AddStockUsecase : IAddStock
{
    private readonly IProductRepository _productRepository;
    private readonly IMappingService<ProductDto, Core.Entities.Product> _mappingService;

    public AddStockUsecase(IProductRepository productRepository, IMappingService<ProductDto, Core.Entities.Product> mappingService)
    {
        _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
        _mappingService = mappingService ?? throw new ArgumentNullException(nameof(mappingService));
    }
    public async Task<ProductDto> Execute(AddStockRequest request)
    {
        if (request == null) throw new ArgumentNullException(nameof(request));

        var product = await _productRepository.GetById(request.Id);
        if (product == null)
        {
            throw new BadRequestException("Product not found.");
        }
        product.AddStock(request.Quantity, request.Price);
        await _productRepository.Update(product);
        var productDto = _mappingService.ToDto(product);
        return productDto;
    }
}