using Application.Dtos;
using Application.Requests;

namespace Application.Contracts.Product;

public interface IUpdateProduct
{
    Task<ProductDto> Execute(Guid id, ProductRequest userRequest);

}