using Application.Contracts.Product;
using Application.Dtos;
using Application.Requests;
using Application.Services;
using Core.Repositories;

namespace Application.Usecases.Product;

public class PageableProductUsecase : IPageableProduct
{
    private readonly IProductRepository _productRepository;
    private readonly IMappingService<ProductDto, Core.Entities.Product> _mapper;

    public PageableProductUsecase(IProductRepository productRepository, IMappingService<ProductDto, Core.Entities.Product> mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }
    async public Task<ResultQueryPageableDto<ProductDto>> Execute(PageableRequest pageableRequest)
    {
        var data = await _productRepository.Pageable(pageableRequest.PageNumber, pageableRequest.PageSize);
        var totalRecords = await _productRepository.Count();
        var dtos = _mapper.ToDto(data);
        return new ResultQueryPageableDto<ProductDto>(dtos, totalRecords);
    }
}