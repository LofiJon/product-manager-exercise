using Application.Dtos;
using Application.Requests;

namespace Application.Contracts.Product;

public interface IConsumeStock
{
    Task<ProductDto> Execute(ConsumeRequest request);
}