using Application.Contracts.Product;
using Application.Dtos;
using Application.Requests;
using Application.Services;
using Core.Repositories;

namespace Application.Usecases.Product;

public class AddProductUsecase : IAddProduct
{
    private readonly IProductRepository _productRepository;
    private readonly IMappingService<ProductDto, Core.Entities.Product> _mapper;

    public AddProductUsecase(IProductRepository productRepository, IMappingService<ProductDto, Core.Entities.Product> mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }

    public async Task<ProductDto> Execute(ProductRequest request)
    {
        var product = new Core.Entities.Product
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            PartNumber = request.PartNumber,
            StockQuantity = request.StockQuantity,
            Price = request.Price,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        await _productRepository.Add(product);

        return _mapper.ToDto(product);
    }
}