using Application.Contracts.Product;
using Application.Dtos;
using Application.Requests;
using Application.Services;
using Infrastructure.Helpers;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Helpers;

namespace WebAPI.Controllers.Product;

[ApiController]
[Tags("Products")]
[Route(PathMapping.Api + PathMapping.Version + PathMapping.Product)]
[Produces("application/json")]
public class PageableProductController : ControllerBase
{
    private readonly IPageableProduct _pageable;
    private readonly IUriService _uriService;

    public PageableProductController(IPageableProduct pageable, IUriService uriService)
    {
        _pageable = pageable;
        _uriService = uriService;
    }

    /// <summary>
    /// Pageable product
    /// </summary>
    [HttpGet("pageable")]
    public async Task<IActionResult> Handle([FromQuery] PageableRequest request)
    {
        var validFilter = new PageableRequest(request.PageNumber, request.PageSize, request.Search);
        var result = await _pageable.Execute(validFilter);
        return Ok(PageableHelper.CreatePagedReponse<ProductDto>(result.Data, validFilter, result.TotalRecords, _uriService, Request.Path.Value));
    }
}