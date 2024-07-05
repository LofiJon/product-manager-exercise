using Application.Dtos;
using Application.Requests;

namespace Application.Contracts.Product;

public interface IAddStock
{
    Task<ProductDto> Execute(AddStockRequest request);
}