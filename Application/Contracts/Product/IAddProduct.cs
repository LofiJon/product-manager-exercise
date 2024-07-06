using Application.Dtos;
using Application.Requests;

namespace Application.Contracts.Product;

public interface IAddProduct
{
    Task<ProductDto> Execute(ProductRequest request);
}