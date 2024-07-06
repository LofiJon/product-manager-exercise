using Application.Contracts.Product;
using Application.Dtos;
using Application.Requests;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Helpers;

namespace WebAPI.Controllers.Product;

[ApiController]
[Tags("Products")]
[Route(PathMapping.Api + PathMapping.Version + PathMapping.Product)]
[Produces("application/json")]
public class UpdateProductController : ControllerBase
{
    private readonly IUpdateProduct _updateProduct;

    public UpdateProductController(IUpdateProduct updateProduct)
    {
        _updateProduct = updateProduct;
    }
    
    /// <summary>
    /// Update product
    /// </summary>
    [HttpPut("{id}")]
    public async Task<ActionResult<ProductDto>> Handle(Guid id, ProductRequest request)
    {
        var result = await _updateProduct.Execute(id, request);
        return Ok(result);
    }
}