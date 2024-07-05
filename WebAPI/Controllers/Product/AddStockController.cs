using Application.Contracts.Product;
using Application.Dtos;
using Application.Requests;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Helpers;

namespace WebAPI.Controllers.Product;

[ApiController]
[Route(PathMapping.Api + PathMapping.Version + PathMapping.Product)]
[Produces("application/json")]
public class AddStockController : ControllerBase
{
    private readonly IAddStock _addStock;

    public AddStockController(IAddStock addStock)
    {
        _addStock = addStock;
    }
    
    /// <summary>
    /// Update product stock
    /// </summary>
    [HttpPut("add-stock-product")]
    public async Task<ActionResult<ProductDto>> Handle(AddStockRequest request)
    {
        var result = await _addStock.Execute(request);
        return Ok(result);
    }
}