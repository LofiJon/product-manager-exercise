using Application.Contracts.Product;
using Application.Dtos;
using Application.Requests;
using Application.Services;
using Core.Exceptions;
using Core.Repositories;

namespace Application.Usecases.Product;

public class ConsumeStockUsecase : IConsumeStock
{
    private readonly IProductRepository _productRepository;
    private readonly IMappingService<ProductDto, Core.Entities.Product> _mappingService;

    public ConsumeStockUsecase(IProductRepository productRepository, IMappingService<ProductDto, Core.Entities.Product> mappingService)
    {
        _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
        _mappingService = mappingService ?? throw new ArgumentNullException(nameof(mappingService));
    }

    public async Task<ProductDto> Execute(ConsumeRequest request)
    {
        if (request == null) throw new ArgumentNullException(nameof(request));

        var product = await _productRepository.GetById(request.ProductId);
        if (product == null)
        {
            throw new BadRequestException("Product not found.");
        }

        bool result = product.Consume(request.Quantity);
        if (!result)
        {
            throw new InvalidOperationException("Insufficient stock to consume.");
        }

        await _productRepository.Update(product);

        var productDto = _mappingService.ToDto(product);
        return productDto;
    }
}