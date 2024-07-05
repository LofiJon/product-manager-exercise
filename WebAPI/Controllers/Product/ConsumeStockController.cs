using Application.Contracts.Product;
using Application.Dtos;
using Application.Requests;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Helpers;

namespace WebAPI.Controllers.Product;

[ApiController]
[Route(PathMapping.Api + PathMapping.Version + PathMapping.Product)]
[Produces("application/json")]
public class ConsumeStockController : ControllerBase
{
    private readonly IConsumeStock _consumeStock;

    public ConsumeStockController(IConsumeStock consumeStock)
    {
        _consumeStock = consumeStock;
    }
    
    /// <summary>
    /// Update product consume
    /// </summary>
    [HttpPut("consume-stock")]
    public async Task<ActionResult<ProductDto>> Handle(ConsumeRequest request)
    {
        var result = await _consumeStock.Execute(request);
        return Ok(result);
    }
}