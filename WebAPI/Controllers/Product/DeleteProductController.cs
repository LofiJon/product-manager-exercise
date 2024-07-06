using Application.Contracts.Product;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Helpers;

namespace WebAPI.Controllers.Product;

[ApiController]
[Tags("Products")]
[Route(PathMapping.Api + PathMapping.Version + PathMapping.Product)]
[Produces("application/json")]
public class DeleteProductController : ControllerBase
{
    private readonly IDeleteProduct _delete;

    public DeleteProductController(IDeleteProduct deleteProduct)
    {
        _delete = deleteProduct;
    }
    
    /// <summary>
    /// Remove Product
    /// </summary>
    [HttpDelete("{id}")]
    public async Task Handle(Guid id)
    {
        await _delete.Execute(id);
    }
}