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
public class AddProductController : ControllerBase
{
    private readonly IAddProduct _addProduct;

    public AddProductController(IAddProduct addProduct)
    {
        _addProduct = addProduct;
    }
    
    /// <summary>
    /// Add product
    /// </summary>
    [HttpPost()]
    public async Task<ActionResult<ProductDto>> Handle(ProductRequest request)
    {
        var result = await _addProduct.Execute(request);
        return Ok(result);
    }
}