using Application.Contracts.Product;
using Application.Dtos;
using Application.Requests;
using Application.Services;
using Core.Repositories;

namespace Application.Usecases.Product;

public class UpdateProductUsecase: IUpdateProduct
{
    private readonly IProductRepository _productRepository;
    private readonly IMappingService<ProductDto, Core.Entities.Product> _mapper;

    public UpdateProductUsecase(IProductRepository productRepository, IMappingService<ProductDto, Core.Entities.Product> mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }

    public async Task<ProductDto> Execute(Guid id, ProductRequest request)
    {
        var product = await _productRepository.GetById(id);
        if (product == null)
        {
            throw new KeyNotFoundException("Product not found");
        }

        product.Name = request.Name;
        product.PartNumber = request.PartNumber;
        product.StockQuantity = request.StockQuantity;
        product.Price = request.Price;
        product.UpdatedAt = DateTime.UtcNow;

        var updatedProduct = await _productRepository.Update(product);

        return _mapper.ToDto(updatedProduct);
    }
}
