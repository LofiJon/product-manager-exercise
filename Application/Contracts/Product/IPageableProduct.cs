using Application.Dtos;
using Application.Requests;

namespace Application.Contracts.Product;

public interface IPageableProduct
{
    Task<ResultQueryPageableDto<ProductDto>> Execute(PageableRequest pageableRequest);

}